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
    public class PointsService : IPointsService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public PointsService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<PointsService>();
        }

        public async Task<double> CalculatePointsTotal(Guid userId)
        {
            return CalculatePointsTotal(await _db.Users.Include(u => u.Employee).SingleOrDefaultAsync(u => u.Id == userId));
        }

        public double CalculatePointsTotal(User user)
        {
            if (user == null || user.Employee == null)
            {
                // 找不到员工信息的，一律显示零积分
                return 0d;
            }

            if (user.Employee.OnboardDate > DateTime.Now)
            {
                // 员工入职日期在此刻之后， 一律显示零积分
                return 0d;
            }

            var now = DateTime.Now;
            int pointsBeginMonth;
            if (user.Employee.OnboardDate.Year < now.Year)
            {
                // 不是今年入职的，积分起始月份一律从1月份开始
                pointsBeginMonth = 1;
            }
            else if (user.Employee.OnboardDate.Day < 15)
            {
                // 15号前入职的，当月开始算
                pointsBeginMonth = user.Employee.OnboardDate.Month;
            }
            else
            {
                // 15号及以后入职的，下月开始算
                pointsBeginMonth = user.Employee.OnboardDate.Month + 1;
            }

            var pointsPerMonth = 2;

            var annualPoints = (now.Month - pointsBeginMonth + 1) * pointsPerMonth;

            return annualPoints;
        }

        public async Task<double> CalculatePointsBalance(Guid userId)
        {
            var pointsTotal = await CalculatePointsTotal(userId);
            var pointsConsumed = await CalculateConsumedPoints(userId);

            return pointsTotal - pointsConsumed;
        }

        public async Task<double> CalculateConsumedPoints(Guid userId)
        {
            var orders = await (from o in _db.Orders
                                where o.User.Id == userId && o.CreatedTime.Value.Year == DateTime.Now.Year
                                select o).ToListAsync();
            if (orders == null)
            {
                return 0d;
            }

            return orders
                .Where(o => o.Status == OrderState.Created || o.Status == OrderState.Completed)
                .Sum(o => o.PointsAmount);
        }
    }
}
