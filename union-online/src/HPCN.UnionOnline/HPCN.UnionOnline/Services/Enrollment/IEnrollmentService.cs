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
        Task<bool> ExistsEnrollmentAsync(Guid id, string name);
        Task<int> CountEnrollmentsAsync(string keyword);
        Task<Enrollment> GetEnrollmentAsync(Guid enrollmentId);
        Task<Enrollment> GetEnrollmentIncludingFieldsAsync(Guid enrollmentId);
        Task<Enrollment> GetEnrollmentIncludingFieldsAndChoicesAsync(Guid enrollmentId);
        Task<List<Enrollment>> GetActiveEnrollmentsAsync();
        Task<List<Enrollment>> SearchEnrollmentsAsync(string keyword, int pageIndex, int pageSize);
        Task<Enrollment> CreateEnrollmentAsync(string name, DateTime beginTime, DateTime endTime, string description, int maxCountOfEnrollees, bool selfEnrollmentOnly, string creator);
        Task<Enrollment> UpdateEnrollmentAsync(Guid id, string name, DateTime beginTime, DateTime endTime, int maxCountOfEnrollees, bool selfEnrollmentOnly, string description, string creator);
        Task OpenEnrollmentAsync(Guid enrollmentId, string openedBy);
        Task CloseEnrollmentAsync(Guid enrollmentId, string closedBy);
        Task DeleteEnrollmentAsync(Guid enrollmentId);
        Task<Enrollment> CloneEnrollmentAsync(Guid enrollmentId, string newName, string clonedBy);

        Task<bool> ExistsFieldAsync(Guid fieldId);
        Task<FieldEntry> GetFieldIncludingEnrollmentAndValueChoicesAsync(Guid fieldId);
        Task<List<FieldEntry>> GetFieldsIncludingChoicesAsync(Guid enrollmentId);
        Task<FieldEntry> AddFieldAsync(FieldEntry field, string creator);
        Task<FieldEntry> UpdateFieldAsync(FieldEntry field, string updatedBy);
        Task RemoveFieldsAsync(Guid[] fieldIds);
    }
}
