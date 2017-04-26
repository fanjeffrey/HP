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

        public async Task<LoginResult> LoginAsync(string email, string employeeNo, string password)
        {
            var user = await _db.Users
                .Include(u => u.Employee)
                .FirstOrDefaultAsync(u => u.Username.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                                        u.Password.Equals(password, StringComparison.Ordinal));

            var result = new LoginResult();

            if (user == null)
            {
                return result;
            }
            if (user.Employee != null && user.Employee.No.Equals(employeeNo, StringComparison.OrdinalIgnoreCase))
            {
                result.Succeeded = true;
            }
            else if (user.IsAdmin)
            {
                result.Succeeded = true;
            }

            result.User = user;

            return result;
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
