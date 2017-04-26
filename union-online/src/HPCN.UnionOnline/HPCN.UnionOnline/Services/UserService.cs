using HPCN.UnionOnline.Data;
using HPCN.UnionOnline.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
            return await _db.Users.FirstOrDefaultAsync(u => u.Username.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}
