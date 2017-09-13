using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IEnrollmentService
    {
        Task<bool> ExistsEnrollmentAsync(Guid id);
        Task<bool> ExistsEnrollmentAsync(string name);
        Task<int> CountEnrollmentsAsync(string keyword);
        Task<Enrollment> GetEnrollmentAsync(Guid enrollmentId);
        Task<Enrollment> GetEnrollmentIncludingFieldsAsync(Guid enrollmentId);
        Task<List<Enrollment>> GetActiveEnrollmentsAsync();
        Task<List<Enrollment>> SearchEnrollmentsAsync(string keyword, int pageIndex, int pageSize);
        Task<Enrollment> CreateEnrollmentAsync(string name, DateTime beginTime, DateTime endTime, string description, int maxCountOfEnrollees, bool selfEnrollmentOnly, string creator);

        Task<bool> ExistsFieldAsync(Guid fieldId);
        Task<FieldEntry> GetFieldIncludingEnrollmentAndValueChoicesAsync(Guid fieldId);
        Task<List<FieldEntry>> GetFieldsIncludingChoicesAsync(Guid enrollmentId);
        Task<FieldEntry> AddFieldAsync(FieldEntry field, string creator);
        Task<FieldEntry> UpdateFieldAsync(FieldEntry field, string updatedBy);
        Task RemoveFieldsAsync(Guid[] fieldIds);
    }
}
