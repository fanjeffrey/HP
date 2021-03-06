﻿using HPCN.UnionOnline.Data;
using HPCN.UnionOnline.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public class EnrollingService : IEnrollingService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public EnrollingService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<EnrollmentService>();
        }

        public bool IsReadyForEnrolling(Enrollment enrollment)
        {
            if (enrollment == null)
            {
                throw new ArgumentNullException(nameof(enrollment));
            }

            return enrollment.Status == ActivityState.Active
                && enrollment.BeginTime < DateTime.Now
                && DateTime.Now < enrollment.EndTime;
        }

        public async Task<bool> ExceedsMaxCountOfEnrollees(Enrollment enrollment)
        {
            if (enrollment == null)
            {
                throw new ArgumentNullException(nameof(enrollment));
            }

            var countOfEnrollees = await _db.Enrollings.CountAsync(e => e.Enrollment.Id == enrollment.Id);

            return countOfEnrollees >= enrollment.MaxCountOfEnrolles;
        }

        public async Task<bool> IsAlreadyEnrolled(string employeeNo, Enrollment enrollment)
        {
            if (string.IsNullOrWhiteSpace(employeeNo))
            {
                throw new ArgumentNullException(nameof(enrollment));
            }

            if (enrollment == null)
            {
                throw new ArgumentNullException(nameof(enrollment));
            }

            return await (from e in _db.Enrollings
                          where e.Enrollment.Id == enrollment.Id
                                && e.EmployeeNo.ToLower() == employeeNo.ToLower()
                          select 1).AnyAsync();
        }

        public async Task<Enrolling> GetEnrollingIncludingEnrollmentAndFieldInputsAsync(Guid enrollingId)
        {
            return await (from e in _db.Enrollings
                          where e.Id == enrollingId
                          select e)
                          .Include(e => e.Enrollment)
                          .Include(e => e.FieldInputs)
                          .SingleOrDefaultAsync();
        }

        public async Task<List<Enrolling>> GetLatestEnrollingsAsync(Guid userId)
        {
            var employee = await _db.Employees.SingleOrDefaultAsync(e => e.UserId == userId);
            if (employee == null) return new List<Enrolling>();

            return await (from e in _db.Enrollings
                          where e.EmployeeNo.ToLower() == employee.No
                          orderby e.CreatedTime descending
                          select e)
                          .Include(e => e.Enrollment)
                          .Include(e => e.User).ThenInclude(u => u.Employee)
                          .Take(3)
                          .ToListAsync();
        }

        public async Task<List<Enrolling>> GetEnrollingsAsync(Guid userId)
        {
            var employee = await _db.Employees.SingleOrDefaultAsync(e => e.UserId == userId);
            if (employee == null) return new List<Enrolling>();

            return await (from e in _db.Enrollings
                          where e.EmployeeNo.ToLower() == employee.No
                          orderby e.CreatedTime descending
                          select e)
                          .Include(e => e.Enrollment)
                          .Include(e => e.User).ThenInclude(u => u.Employee)
                          .ToListAsync();
        }

        public async Task<Dictionary<Guid, int>> GetCountOfEnrollingsInEnrollments(IEnumerable<Guid> enrollmentIds)
        {
            var enrollings = await (from e in _db.Enrollments
                                    where enrollmentIds.Contains(e.Id)
                                    let count = _db.Enrollings.Count(el => el.Enrollment.Id == e.Id)
                                    select new { Id = e.Id, CountOfEnrollees = count })
                                    .ToDictionaryAsync(g => g.Id, g => g.CountOfEnrollees);

            return enrollings;
        }

        public async Task<Enrolling> CreateAsync(Guid enrollmentId,
            string employeeNo, IDictionary<string, string> fieldInputs, Guid userId, string createdBy)
        {
            var enrollment = await _db.Enrollments.SingleOrDefaultAsync(e => e.Id == enrollmentId);
            if (enrollment == null)
            {
                throw new Exception($"Failed to find the enrollment with Id: {enrollmentId}.");
            }

            if (enrollment.SelfEnrollmentOnly)
            {
                var employee = await _db.Employees.SingleOrDefaultAsync(e => e.UserId == userId);
                if (employee != null && employee.No != employeeNo)
                {
                    throw new Exception($"不允许代报名。");
                }
            }

            var now = DateTime.Now;
            if (await _db.Enrollings.AnyAsync(e => e.Enrollment.Id == enrollment.Id && e.EmployeeNo == employeeNo))
            {
                throw new Exception($"The employee (#{employeeNo}) already enrolled the enrollment: {enrollment.Name}.");
            }

            var user = await _db.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var enrolling = new Enrolling
            {
                Id = Guid.NewGuid(),
                EmployeeNo = employeeNo,
                Enrollment = enrollment,
                User = user
            };
            enrolling.UpdatedBy = enrolling.CreatedBy = createdBy;
            enrolling.UpdatedTime = enrolling.CreatedTime = now;
            enrolling.FieldInputs = new List<FieldInput>();

            // add field inputs
            if (fieldInputs != null)
            {
                foreach (var item in fieldInputs)
                {
                    var fieldId = Guid.Parse(item.Key.Replace("FieldInputs.", string.Empty));
                    var fieldValue = item.Value ?? string.Empty; // not null

                    enrolling.FieldInputs.Add(new FieldInput
                    {
                        Id = Guid.NewGuid(),
                        FieldEntryId = fieldId,
                        Input = fieldValue,
                        CreatedBy = createdBy,
                        UpdatedBy = createdBy,
                        CreatedTime = now,
                        UpdatedTime = now
                    });
                }
            }

            _db.Enrollings.Add(enrolling);
            await _db.SaveChangesAsync();

            return enrolling;
        }

        public async Task<Enrolling> UpdateAsync(Guid enrollingId,
            string employeeNo, Dictionary<string, string> fieldInputs, Guid userId, string updatedBy)
        {
            var enrolling = await _db.Enrollings
                .Include(e => e.Enrollment)
                .Include(e => e.FieldInputs)
                .SingleOrDefaultAsync(e => e.Id == enrollingId);

            if (enrolling == null)
            {
                throw new Exception($"Failed to find the enrolling with Id: {enrollingId}.");
            }

            if (enrolling.Enrollment.SelfEnrollmentOnly)
            {
                var employee = await _db.Employees.SingleOrDefaultAsync(e => e.UserId == userId);
                if (employee != null && employee.No != employeeNo)
                {
                    throw new Exception($"不允许代报名。");
                }
            }

            // update field inputs by removing and adding
            var now = DateTime.Now;
            enrolling.FieldInputs.Clear();
            if (fieldInputs != null)
            {
                foreach (var item in fieldInputs)
                {
                    var fieldId = Guid.Parse(item.Key.Replace("FieldInputs.", string.Empty));
                    var fieldValue = item.Value ?? string.Empty; // not null

                    enrolling.FieldInputs.Add(new FieldInput
                    {
                        Id = Guid.NewGuid(),
                        FieldEntryId = fieldId,
                        Input = fieldValue,
                        CreatedBy = updatedBy,
                        UpdatedBy = updatedBy,
                        CreatedTime = now,
                        UpdatedTime = now
                    });
                }
            }

            // update the enrolling
            enrolling.UpdatedBy = updatedBy;
            enrolling.UpdatedTime = now;

            await _db.SaveChangesAsync();

            return enrolling;
        }

        public async Task CancelAsync(Guid enrollingId)
        {
            var enrolling = await (from e in _db.Enrollings where e.Id == enrollingId select e).SingleOrDefaultAsync();

            if (enrolling != null)
            {
                _db.Enrollings.Remove(enrolling);
                await _db.SaveChangesAsync();
            }
        }
    }
}
