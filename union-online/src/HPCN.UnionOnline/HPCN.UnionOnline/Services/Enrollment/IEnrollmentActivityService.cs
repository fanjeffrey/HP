using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IEnrollmentActivityService
    {
        Task<bool> ExistsAsync(Guid activityId);
        Task<bool> ExistsAsync(string name);
        Task<EnrollmentActivity> CreateAsync(string name, DateTime beginTime, DateTime endTime, string description, string creator);
        Task<int> CountAsync(string keyword);
        Task<IList<EnrollmentActivity>> SearchAsync(string keyword, int pageIndex, int pageSize);
        Task<EnrollmentActivity> GetEnrollmentActivityByIdAsync(Guid value);
        Task<List<EntityProperty>> GetPropertiesAsync(Guid id);
        Task<PropertyEntry> AddPropertyAsync(Guid enrollmentActivityId, PropertyEntry property, string creator);
        Task<List<EnrollmentActivity>> GetActiveActivitiesAsync();
    }
}
