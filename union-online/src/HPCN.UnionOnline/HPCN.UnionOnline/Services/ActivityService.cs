using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPCN.UnionOnline.Models;
using HPCN.UnionOnline.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace HPCN.UnionOnline.Services
{
    public class ActivityService : IActivityService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public ActivityService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<ActivityService>();
        }

        public async Task<Activity> CreateAsync(string name, DateTime beginTime, DateTime endTime, string description, string creator)
        {
            name = name?.Trim();

            var activity = new Activity
            {
                Name = name,
                BeginTime = beginTime,
                EndTime = endTime,
                Description = description
            };
            activity.UpdatedTime = activity.CreatedTime = DateTime.Now;
            activity.UpdatedBy = activity.CreatedBy = creator;

            _db.Activities.Add(activity);
            await _db.SaveChangesAsync();

            return activity;
        }

        public async Task UpdateAsync(Guid id, string name, DateTime beginTime, DateTime endTime, string description, string updatedBy)
        {
            var activity = await _db.Activities.SingleOrDefaultAsync(a => a.Id == id);
            if (activity != null)
            {
                activity.Name = name.Trim();
                activity.BeginTime = beginTime;
                activity.EndTime = endTime;
                activity.Description = description;
                activity.UpdatedTime = DateTime.Now;
                activity.UpdatedBy = updatedBy;

                await _db.SaveChangesAsync();
            }
        }

        public async Task OpenAsync(Guid id)
        {
            var activity = await _db.Activities.SingleOrDefaultAsync(a => a.Id == id);
            if (activity != null)
            {
                activity.Status = ActivityState.Active;

                await _db.SaveChangesAsync();
            }
        }

        public async Task CloseAsync(Guid id)
        {
            var activity = await _db.Activities.SingleOrDefaultAsync(a => a.Id == id);
            if (activity != null)
            {
                activity.Status = ActivityState.Closed;

                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var activity = await _db.Activities.SingleOrDefaultAsync(a => a.Id == id);
            if (activity != null)
            {
                _db.Activities.Remove(activity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _db.Activities.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(name.Trim()))
            {
                return false;
            }

            return await _db.Activities.AnyAsync(a => a.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<bool> ExistsAsync(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(name.Trim()))
            {
                return false;
            }

            return await _db.Activities.AnyAsync(a => a.Id != id && a.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<Activity> GetActivityByIdAsync(Guid id)
        {
            return await _db.Activities.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Activity> GetActiveActivityAsync()
        {
            var activity = await _db.Activities
                .OrderBy(a => a.BeginTime)
                .FirstOrDefaultAsync(a => a.Status == ActivityState.Active);

            if (activity == null)
            {
                return null;
            }

            activity.ActivityProducts = await _db.ActivityProducts
                .Include(ap => ap.Product)
                .Where(ap => ap.Activity.Id == activity.Id)
                .ToListAsync();

            return activity;
        }

        public async Task<int> CountAsync(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _db.Activities.CountAsync();
            }

            return await _db.Activities
                .Where(a => a.Name.Contains(keyword) || a.Description.Contains(keyword))
                .CountAsync();
        }

        public async Task<IList<Activity>> SearchAsync(string keyword, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 20 : pageSize;
            keyword = keyword?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _db.Activities
                    .OrderBy(p => p.Name)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return await _db.Activities
                .Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword))
                .OrderBy(p => p.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<ICollection<ActivityProduct>> GetProductsAsync(Guid activityId)
        {
            return await _db.ActivityProducts
                .Include(ap => ap.Product)
                .Where(p => p.Activity.Id == activityId)
                .ToListAsync();
        }

        public async Task AddProductsAsync(Guid activityId, IEnumerable<Guid> productIds, string creator)
        {
            var activity = await _db.Activities.SingleOrDefaultAsync(a => a.Id == activityId);
            var activityProducts = await _db.ActivityProducts.Include(ap => ap.Product).Where(ap => ap.Activity.Id == activityId).ToListAsync();

            if (activity != null)
            {
                var now = DateTime.Now;
                var products = await _db.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
                foreach (var p in products)
                {
                    if (activityProducts.Any(ap => ap.Product.Id == p.Id)) continue;

                    var activityProduct = new ActivityProduct
                    {
                        Id = Guid.NewGuid(),
                        Activity = activity,
                        Product = p,
                        PointsPayment = p.PointsPayment,
                        SelfPayment = p.SelfPayment
                    };
                    activityProduct.UpdatedTime = activityProduct.CreatedTime = now;
                    activityProduct.UpdatedBy = activityProduct.CreatedBy = creator;

                    _db.ActivityProducts.Add(activityProduct);
                }

                if (products.Count > 0)
                {
                    activity.UpdatedTime = now;
                    activity.UpdatedBy = creator;
                }

                await _db.SaveChangesAsync();
            }
        }

        public async Task RemoveActivityProductsAsync(Guid activityId, IEnumerable<Guid> activityProductIds, string updatedBy)
        {
            var productsToRemove = _db.ActivityProducts.Where(ap => activityProductIds.Contains(ap.Id));
            _db.ActivityProducts.RemoveRange(productsToRemove);

            var activity = await _db.Activities.FirstOrDefaultAsync(a => a.Id == activityId);
            if (activity != null)
            {
                activity.UpdatedTime = DateTime.Now;
                activity.UpdatedBy = updatedBy;
            }

            await _db.SaveChangesAsync();
        }
    }
}
