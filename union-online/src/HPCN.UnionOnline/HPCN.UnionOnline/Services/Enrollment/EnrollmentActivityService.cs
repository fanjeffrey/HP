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
    public class EnrollmentActivityService : IEnrollmentActivityService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public EnrollmentActivityService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<EnrollmentActivityService>();
        }

        public async Task<EnrollmentActivity> CreateAsync(string name, DateTime beginTime, DateTime endTime, string description, string creator)
        {
            name = name?.Trim();

            var activity = new EnrollmentActivity
            {
                Name = name,
                BeginTime = beginTime,
                EndTime = endTime,
                Description = description,
                Status = ActivityState.Pending
            };
            activity.UpdatedTime = activity.CreatedTime = DateTime.Now;
            activity.UpdatedBy = activity.CreatedBy = creator;

            _db.EnrollmentActivities.Add(activity);
            await _db.SaveChangesAsync();

            return activity;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(name.Trim()))
            {
                return false;
            }

            return await _db.EnrollmentActivities.AnyAsync(a => a.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<int> CountAsync(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _db.Activities.CountAsync();
            }

            return await _db.EnrollmentActivities
                .Where(a => a.Name.Contains(keyword) || a.Description.Contains(keyword))
                .CountAsync();
        }

        public async Task<IList<EnrollmentActivity>> SearchAsync(string keyword, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 20 : pageSize;
            keyword = keyword?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _db.EnrollmentActivities
                    .OrderBy(p => p.Name)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return await _db.EnrollmentActivities
                .Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword))
                .OrderBy(p => p.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
