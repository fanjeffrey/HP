using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(
            string no, string emailAddress,
            string chineseName, string displayName,
            DateTime onboardDate, string phoneNumber,
            string managerEmail, string teamAdminAssistant,
            string idCardNo, string costCenter,
            string baseCity, string workCity,
            Gender gender, EmployeeType employeeType,
            string creator);
        Task UpdateAsync(Guid userId,
            string no, string emailAddress,
            string chineseName, string displayName,
            DateTime onboardDate, string phoneNumber,
            string managerEmail, string teamAdminAssistant,
            string idCardNo, string costCenter,
            string baseCity, string workCity,
            Gender gender, EmployeeType employeeType,
            string updatedBy);
        Task RemoveAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsByNoAsync(string no);
        Task<bool> ExistsByEmailAddressAsync(string emailAddress);
        Task<bool> ExistsByIdCardNoAsync(string idCardNo);
        Task<bool> ExistsByNoAsync(Guid id, string no);
        Task<bool> ExistsByEmailAddressAsync(Guid id, string emailAddress);
        Task<bool> ExistsByIdCardNoAsync(Guid id, string idCardNo);
        Task<Employee> GetAsync(Guid id);
        Task<Employee> GetAsync(string no);
        Task<int> CountAsync(string keyword);
        Task<IList<Employee>> SearchAsync(string keyword, int pageIndex, int pageSize);
    }
}
