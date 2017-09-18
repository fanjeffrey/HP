using HPCN.UnionOnline.Data;
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

            return countOfEnrollees > enrollment.MaxCountOfEnrolles;
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
                                && e.Enrollee.EmployeeNo.ToLower() == employeeNo.ToLower()
                          select 1).AnyAsync();
        }

        public async Task<Enrolling> CreateAsync(Guid enrollmentId,
            string employeeNo, string emailAddress, string name, string phoneNumber,
            IDictionary<string, string> fieldInputs,
            Guid userId, string createdBy)
        {
            var enrollment = await _db.Enrollments.SingleOrDefaultAsync(e => e.Id == enrollmentId);
            if (enrollment == null)
            {
                throw new Exception($"Failed to find the enrollment with Id: {enrollmentId}.");
            }

            var now = DateTime.Now;
            var enrollee = await _db.Enrollees.SingleOrDefaultAsync(e => e.EmployeeNo.ToLower() == employeeNo.ToLower());
            if (enrollee == null)
            {
                enrollee = new Enrollee
                {
                    Id = Guid.NewGuid(),
                    EmployeeNo = employeeNo,
                    EmailAddress = emailAddress,
                    Name = name,
                    PhoneNumber = phoneNumber
                };
                enrollee.UpdatedBy = enrollee.CreatedBy = createdBy;
                enrollee.UpdatedTime = enrollee.CreatedTime = now;
            }

            if (await _db.Enrollings.AnyAsync(e => e.Enrollment.Id == enrollment.Id && e.Enrollee.Id == enrollee.Id))
            {
                throw new Exception($"The enrollee ({enrollee.Name}, {enrollee.EmployeeNo}) already enrolled the enrollment: {enrollment.Name}.");
            }

            var user = await _db.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var enrolling = new Enrolling
            {
                Id = Guid.NewGuid(),
                Enrollment = enrollment,
                Enrollee = enrollee,
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
    }
}
