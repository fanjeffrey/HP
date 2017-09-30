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
    public class EmployeeService : IEmployeeService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public EmployeeService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<EmployeeService>();
        }

        public async Task<Employee> CreateAsync(
            string no, string emailAddress,
            string chineseName, string displayName,
            DateTime onboardDate, string phoneNumber,
            string managerEmail, string teamAdminAssistant,
            string idCardNo, string costCenter,
            string baseCity, string workCity,
            Gender gender, EmployeeType employeeType,
            string creator)
        {
            emailAddress = emailAddress?.Trim();

            // check if email address has been used as username
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Username.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                throw new Exception("The email address has already been used as the UserName of a user.");
            }

            var now = DateTime.Now;

            var employee = new Employee
            {
                No = no?.Trim(),
                EmailAddress = emailAddress?.Trim(),
                ChineseName = chineseName?.Trim(),
                DisplayName = displayName?.Trim(),
                OnboardDate = onboardDate.Date,
                PhoneNumber = phoneNumber?.Trim(),
                ManagerEmail = managerEmail?.Trim(),
                TeamAdminAssistant = teamAdminAssistant?.Trim(),
                IdCardNo = idCardNo?.Trim(),
                CostCenter = costCenter?.Trim(),
                BaseCity = baseCity?.Trim(),
                WorkCity = workCity?.Trim(),
                Gender = gender,
                EmployeeType = employeeType,
                EmployeeStatus = EmployeeState.Active
            };
            employee.UpdatedBy = employee.CreatedBy = creator;
            employee.UpdatedTime = employee.CreatedTime = now;

            // create user for this employee
            user = new User
            {
                Id = Guid.NewGuid(),
                Username = employee.EmailAddress,
                Password = employee.OnboardDate.ToString("yyMMdd"),
                Disabled = false,
                IsAdmin = false
            };
            user.UpdatedBy = user.CreatedBy = creator;
            user.UpdatedTime = user.CreatedTime = now;
            user.Employee = employee;

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return employee;
        }

        public async Task UpdateAsync(Guid userId,
            string no, string emailAddress,
            string chineseName, string displayName,
            DateTime onboardDate, string phoneNumber,
            string managerEmail, string teamAdminAssistant,
            string idCardNo, string costCenter,
            string baseCity, string workCity,
            Gender gender, EmployeeType employeeType,
            string updatedBy)
        {
            var user = await _db.Users
                .Include(u => u.Employee)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Employee == null)
            {
                throw new Exception($"Failed to find the user with the GUID: {userId}.");
            }

            // update employee info
            user.Employee.No = no?.Trim();
            user.Employee.EmailAddress = emailAddress?.Trim();
            user.Employee.ChineseName = chineseName?.Trim();
            user.Employee.DisplayName = displayName?.Trim();
            user.Employee.OnboardDate = onboardDate;
            user.Employee.PhoneNumber = phoneNumber?.Trim();
            user.Employee.ManagerEmail = managerEmail?.Trim();
            user.Employee.TeamAdminAssistant = teamAdminAssistant?.Trim();
            user.Employee.IdCardNo = idCardNo?.Trim();
            user.Employee.CostCenter = costCenter?.Trim();
            user.Employee.BaseCity = baseCity?.Trim();
            user.Employee.WorkCity = workCity?.Trim();
            user.Employee.Gender = gender;
            user.Employee.EmployeeType = employeeType;

            // update the username for the employee
            user.Username = user.Employee.EmailAddress;

            // log this update
            user.UpdatedBy = user.Employee.UpdatedBy = updatedBy;
            user.UpdatedTime = user.Employee.UpdatedTime = DateTime.Now;

            await _db.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid userId)
        {
            var employee = await _db.Employees
                .Include(e => e.User)
                .SingleOrDefaultAsync(e => e.UserId == userId);

            // just set Employee Status to Inactive instead of a true deletion
            employee.EmployeeStatus = EmployeeState.Inactive;
            // disable his/her user so that he/she can't login
            employee.User.Disabled = true;

            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid userId)
        {
            return await _db.Employees.AnyAsync(e => e.UserId == userId);
        }

        public async Task<bool> ExistsByEmailAddressAsync(string emailAddress)
        {
            return await _db.Employees.AnyAsync(e => e.EmailAddress.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> ExistsByEmailAddressAsync(Guid userId, string emailAddress)
        {
            return await _db.Employees.AnyAsync(e => e.UserId != userId && e.EmailAddress.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> ExistsByIdCardNoAsync(string idCardNo)
        {
            return await _db.Employees.AnyAsync(e => e.IdCardNo.Equals(idCardNo, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> ExistsByIdCardNoAsync(Guid userId, string idCardNo)
        {
            return await _db.Employees.AnyAsync(e => e.UserId != userId && e.IdCardNo.Equals(idCardNo, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> ExistsByNoAsync(string no)
        {
            return await _db.Employees.AnyAsync(e => e.No.Equals(no, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> ExistsByNoAsync(Guid userId, string no)
        {
            return await _db.Employees.AnyAsync(e => e.UserId != userId && e.No.Equals(no, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Employee> GetAsync(Guid userId)
        {
            return await _db.Employees
                .Include(e => e.User)
                .SingleOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<Employee> GetAsync(string no)
        {
            return await _db.Employees.SingleOrDefaultAsync(e => e.No == no);
        }

        public async Task<int> CountAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _db.Employees.CountAsync();
            }

            keyword = keyword.Trim();

            return await _db.Employees
                .Where(e => e.No.Contains(keyword)
                    || e.ChineseName.Contains(keyword)
                    || e.DisplayName.Contains(keyword)
                    || e.EmailAddress.Contains(keyword)
                    || e.IdCardNo.Contains(keyword)
                    || e.PhoneNumber.Contains(keyword)
                    || e.BaseCity.Contains(keyword)
                    || e.WorkCity.Contains(keyword)
                )
                .CountAsync();
        }

        public async Task<IList<Employee>> SearchAsync(string keyword, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 20 : pageSize;
            keyword = keyword?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _db.Employees
                    .Include(e => e.User)
                    .OrderBy(e => e.No)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return await SearchQuery(keyword)
                .Include(e => e.User)
                .OrderBy(e => e.No)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        private IQueryable<Employee> SearchQuery(string keyword)
        {
            return _db.Employees
                .Where(e => e.No.Contains(keyword)
                    || e.ChineseName.Contains(keyword)
                    || e.DisplayName.Contains(keyword)
                    || e.EmailAddress.Contains(keyword)
                    || e.IdCardNo.Contains(keyword)
                    || e.PhoneNumber.Contains(keyword)
                    || e.BaseCity.Contains(keyword)
                    || e.WorkCity.Contains(keyword)
                );
        }
    }
}
