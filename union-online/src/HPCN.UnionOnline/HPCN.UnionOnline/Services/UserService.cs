using HPCN.UnionOnline.Data;
using HPCN.UnionOnline.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace HPCN.UnionOnline.Services
{
    public class UserService : IUserService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public UserService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<UserService>();
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _db.Users.SingleOrDefaultAsync(u => u.Username.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(Guid userId)
        {
            return await (from o in _db.Orders
                          where o.User.Id == userId
                          select o).ToListAsync();
        }

        public async Task<User> GetUserWithEmployeeInfoAsync(Guid userId)
        {
            return await _db.Users
                .Include(u => u.Employee)
                .SingleOrDefaultAsync(u => u.Id == userId);
        }
    }
}
