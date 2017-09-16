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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public EnrollmentService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<EnrollmentService>();
        }

        #region enrollment operations

        public async Task<bool> ExistsEnrollmentAsync(Guid enrollmentId)
        {
            return await _db.Enrollments.AnyAsync(a => a.Id == enrollmentId);
        }

        public async Task<bool> ExistsEnrollmentAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name?.Trim()))
            {
                throw new ArgumentException(nameof(name));
            }

            return await _db.Enrollments.AnyAsync(a => a.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<bool> ExistsEnrollmentAsync(Guid enrollmentId, string name)
        {
            if (string.IsNullOrWhiteSpace(name?.Trim()))
            {
                throw new ArgumentException(nameof(name));
            }

            return await _db.Enrollments.AnyAsync(a => a.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) && a.Id != enrollmentId);
        }

        public async Task<int> CountEnrollmentsAsync(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _db.Activities.CountAsync();
            }

            return await _db.Enrollments
                .Where(a => a.Name.Contains(keyword) || a.Description.Contains(keyword))
                .CountAsync();
        }

        public async Task<Enrollment> GetEnrollmentAsync(Guid enrollmentId)
        {
            return await _db.Enrollments.SingleOrDefaultAsync(a => a.Id == enrollmentId);
        }

        public async Task<Enrollment> GetEnrollmentIncludingFieldsAsync(Guid enrollmentId)
        {
            return await _db.Enrollments.Where(e => e.Id == enrollmentId)
                                        .Include(e => e.ExtraFormFields)
                                        .SingleOrDefaultAsync();
        }

        public async Task<List<Enrollment>> GetActiveEnrollmentsAsync()
        {
            return await (from a in _db.Enrollments
                          where a.Status == ActivityState.Active
                          orderby a.EndTime
                          select a).ToListAsync();
        }

        public async Task<List<Enrollment>> SearchEnrollmentsAsync(string keyword, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 20 : pageSize;
            keyword = keyword?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _db.Enrollments.OrderBy(p => p.Name)
                                            .Skip((pageIndex - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();
            }

            return await _db.Enrollments.Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword))
                                        .OrderBy(p => p.Name)
                                        .Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();
        }

        public async Task<Enrollment> CreateEnrollmentAsync(string name, DateTime beginTime, DateTime endTime, string description, int maxCountOfEnrolles, bool selfEnrollmentOnly, string creator)
        {
            name = name?.Trim();

            var e = new Enrollment
            {
                Name = name,
                BeginTime = beginTime,
                EndTime = endTime,
                Description = description,
                MaxCountOfEnrolles = maxCountOfEnrolles,
                SelfEnrollmentOnly = selfEnrollmentOnly,
                Status = ActivityState.Pending
            };
            e.UpdatedTime = e.CreatedTime = DateTime.Now;
            e.UpdatedBy = e.CreatedBy = creator;

            _db.Enrollments.Add(e);
            await _db.SaveChangesAsync();

            return e;
        }

        public async Task<Enrollment> UpdateEnrollmentAsync(Guid id, string name, DateTime beginTime, DateTime endTime, int maxCountOfEnrollees, bool selfEnrollmentOnly, string description, string updatedBy)
        {
            var enrollment = _db.Enrollments.SingleOrDefault(e => e.Id == id);
            if (enrollment == null)
            {
                throw new Exception($"Failed to find the enrollment: {id}.");
            }

            enrollment.Name = name.Trim();
            enrollment.BeginTime = beginTime;
            enrollment.EndTime = endTime;
            enrollment.MaxCountOfEnrolles = maxCountOfEnrollees;
            enrollment.SelfEnrollmentOnly = selfEnrollmentOnly;
            enrollment.Description = description;
            enrollment.UpdatedBy = updatedBy;
            enrollment.UpdatedTime = DateTime.Now;

            await _db.SaveChangesAsync();

            return enrollment;
        }

        public async Task OpenEnrollmentAsync(Guid enrollmentId, string openedBy)
        {
            var enrollment = _db.Enrollments.SingleOrDefault(e => e.Id == enrollmentId);
            if (enrollment != null)
            {
                enrollment.Status = ActivityState.Active;
                enrollment.UpdatedBy = openedBy;
                enrollment.UpdatedTime = DateTime.Now;

                await _db.SaveChangesAsync();
            }
        }

        public async Task CloseEnrollmentAsync(Guid enrollmentId, string closedBy)
        {
            var enrollment = _db.Enrollments.SingleOrDefault(e => e.Id == enrollmentId);
            if (enrollment != null)
            {
                enrollment.Status = ActivityState.Closed;
                enrollment.UpdatedBy = closedBy;
                enrollment.UpdatedTime = DateTime.Now;

                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteEnrollmentAsync(Guid enrollmentId)
        {
            var enrollment = _db.Enrollments.SingleOrDefault(e => e.Id == enrollmentId);
            if (enrollment != null)
            {
                _db.Enrollments.Remove(enrollment);

                await _db.SaveChangesAsync();
            }
        }

        #endregion

        #region field operations

        public async Task<bool> ExistsFieldAsync(Guid fieldId)
        {
            return await _db.FieldEntries.AnyAsync(f => f.Id == fieldId);
        }

        public async Task<FieldEntry> GetFieldIncludingEnrollmentAndValueChoicesAsync(Guid fieldId)
        {
            return await _db.FieldEntries.Where(f => f.Id == fieldId)
                                        .Include(f => f.Enrollment)
                                        .Include(f => f.ValueChoices)
                                        .SingleOrDefaultAsync();
        }

        public async Task<List<FieldEntry>> GetFieldsIncludingChoicesAsync(Guid enrollmentId)
        {
            return await _db.FieldEntries
                .Where(p => p.Enrollment.Id == enrollmentId)
                .Include(p => p.ValueChoices)
                .ToListAsync();
        }

        public async Task<FieldEntry> AddFieldAsync(FieldEntry field, string creator)
        {
            var enrollment = await GetEnrollmentIncludingFieldsAsync(field.Enrollment.Id);
            if (enrollment == null)
            {
                throw new Exception($"Failed to find enrollment: {field.Enrollment.Id}");
            }

            if (field.ChoiceMode == FieldValueChoiceMode.None)
            {
                field.ValueChoices.Clear();
            }
            else
            {
                field.ValueChoices = field.ValueChoices
                    .Where(c => !string.IsNullOrWhiteSpace(c?.Value) && !string.IsNullOrWhiteSpace(c.DisplayText))
                    .ToList();
            }

            var now = DateTime.Now;

            field.Id = Guid.NewGuid();
            field.Name = field.Name.Trim();
            field.DisplayName = field.DisplayName.Trim();
            field.Description = field.Description.Trim();
            field.RequiredMessage = field.RequiredMessage.Trim();
            field.UpdatedBy = field.CreatedBy = creator;
            field.UpdatedTime = field.CreatedTime = now;

            if (field.ValueChoices != null)
            {
                foreach (var valueChoice in field.ValueChoices)
                {
                    valueChoice.Id = Guid.NewGuid();
                    valueChoice.UpdatedBy = valueChoice.CreatedBy = creator;
                    valueChoice.UpdatedTime = valueChoice.CreatedTime = now;
                }
            }

            enrollment.ExtraFormFields.Add(field);
            await _db.SaveChangesAsync();

            return field;
        }

        public async Task<FieldEntry> UpdateFieldAsync(FieldEntry field, string updatedBy)
        {
            var enrollment = await GetEnrollmentAsync(field.Enrollment.Id);
            if (enrollment == null)
            {
                throw new Exception($"Failed to find enrollment: {field.Enrollment.Id}");
            }

            var fieldInDb = await _db.FieldEntries.Include(f => f.ValueChoices)
                                                    .SingleOrDefaultAsync(f => f.Id == field.Id);
            if (fieldInDb == null)
            {
                throw new Exception($"Failed to find the field: {field.Id}");
            }

            // update field
            // ------------
            fieldInDb.Name = field.Name.Trim();
            fieldInDb.DisplayName = field.DisplayName.Trim();
            fieldInDb.Description = field.Description.Trim();
            fieldInDb.IsRequired = field.IsRequired;
            fieldInDb.RequiredMessage = field.RequiredMessage.Trim();
            fieldInDb.DisplayOrder = field.DisplayOrder;
            fieldInDb.TypeOfValue = field.TypeOfValue;
            fieldInDb.ChoiceMode = field.ChoiceMode;

            fieldInDb.UpdatedBy = updatedBy;
            fieldInDb.UpdatedTime = DateTime.Now;

            // update value choices
            // --------------------
            if (field.ChoiceMode == FieldValueChoiceMode.None)
            {
                fieldInDb.ValueChoices.Clear();
            }
            else
            {
                // remove choices which are not in user inputs.
                fieldInDb.ValueChoices.RemoveAll(c => field.ValueChoices.All(v => c.Id != v.Id));

                foreach (var choice in field.ValueChoices)
                {
                    var choiceInDb = fieldInDb.ValueChoices.SingleOrDefault(c => c.Id == choice.Id);

                    if (choiceInDb != null)// existing choice to update
                    {
                        choiceInDb.Value = choice.Value;
                        choiceInDb.DisplayText = choice.DisplayText;
                        choiceInDb.DisplayOrder = choice.DisplayOrder;
                        choiceInDb.Description = choice.Description;
                        choiceInDb.UpdatedBy = fieldInDb.UpdatedBy;
                        choiceInDb.UpdatedTime = fieldInDb.UpdatedTime;
                    }
                    else // new choice to add
                    {
                        choice.Id = Guid.NewGuid();
                        choice.UpdatedBy = choice.CreatedBy = fieldInDb.UpdatedBy;
                        choice.UpdatedTime = choice.CreatedTime = fieldInDb.UpdatedTime;
                        fieldInDb.ValueChoices.Add(choice);
                    }
                }
            }

            await _db.SaveChangesAsync();

            return fieldInDb;
        }

        public async Task RemoveFieldsAsync(Guid[] fieldIds)
        {
            var fieldsToRemove = _db.FieldEntries.Where(f => fieldIds.Contains(f.Id));
            _db.FieldEntries.RemoveRange(fieldsToRemove);

            await _db.SaveChangesAsync();
        }

        #endregion
    }
}
