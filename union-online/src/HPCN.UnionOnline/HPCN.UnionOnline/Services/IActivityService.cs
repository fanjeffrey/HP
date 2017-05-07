using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IActivityService
    {
        Task<Activity> CreateAsync(string name, DateTime beginTime, DateTime endTime, string description, string creator);
        Task UpdateAsync(Guid id, string name, DateTime beginTime, DateTime endTime, string description, string updatedBy);
        Task OpenAsync(Guid id);
        Task CloseAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<bool> ExistsAsync(Guid id, string name);
        Task<Activity> GetActivityByIdAsync(Guid id);
        Task<Activity> GetActiveActivityAsync();
        Task<int> CountAsync(string keyword);
        Task<IList<Activity>> SearchAsync(string keyword, int pageIndex, int pageSize);
        Task<ICollection<ActivityProduct>> GetProductsAsync(Guid activityId);
        Task AddProductsAsync(Guid activityId, IEnumerable<Guid> productIds, string creator);
        Task RemoveActivityProductsAsync(Guid activityId, IEnumerable<Guid> activityProductIds, string updatedBy);
    }
}
