using HPCN.UnionOnline.Data;
using HPCN.UnionOnline.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public class AccountService : IAccountService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public AccountService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<AccountService>();
        }

        public async Task<string> GenerateNewPasswordAsync(Guid userId)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception($"Failed to find the user with the guid: {userId}.");
            }

            var ticket = DateTime.Now.Ticks.ToString();
            user.Password = ticket.Substring(ticket.Length - 6);
            await _db.SaveChangesAsync();

            return user.Password;
        }

        public async Task<User> LoginAsync(string email, string employeeNo, string password)
        {
            var user = await _db.Users
                .Include(u => u.Employee)
                .FirstOrDefaultAsync(u => u.Username.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                                        u.Password.Equals(password, StringComparison.Ordinal));
            if (user == null)
            {
                return null;
            }

            if (!user.IsAdmin
                && user.Employee != null
                && !user.Employee.No.Equals(employeeNo, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return user;
        }

        public async Task ResetSystemAdmin()
        {
            var saUsername = "uoadmin@cn.hp.com";
            var saPassword = "uoadmin@cn.hp.com";
            var sa = await _db.Users.FirstOrDefaultAsync(u => saUsername.Equals(u.Username, StringComparison.OrdinalIgnoreCase));
            if (sa == null)
            {
                sa = new User
                {
                    Username = saUsername,
                    Password = saPassword,
                    IsAdmin = true,
                    Disabled = false
                };
                sa.UpdatedTime = sa.CreatedTime = DateTime.Now;
                sa.UpdatedBy = sa.CreatedBy = "system";
                _db.Users.Add(sa);
            }
            else
            {
                sa.Password = saPassword;
                sa.IsAdmin = true;
                sa.Disabled = false;
                sa.UpdatedTime = DateTime.Now;
            }

            await _db.SaveChangesAsync();
        }

        public async Task<bool> ValidatePrincipal(string username, string updatedTime)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                return false;
            }

            _logger.LogInformation(2, $"Validating UpdatedTime in principal '{updatedTime}' with UpdatedTime in database '{user.UpdatedTime}' ... ");

            if (!user.UpdatedTime.HasValue)
            {
                return false;
            }

            return user.UpdatedTime.Value.Ticks.ToString().Equals(updatedTime, StringComparison.OrdinalIgnoreCase);
        }
    }
}
