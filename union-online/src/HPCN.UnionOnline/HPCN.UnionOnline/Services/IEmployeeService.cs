using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(string no, string emailAddress, string chineseName, string displayName, Gender gender, DateTime onboardDate,
            string idCardNo, string phoneNumber, string baseCity, string workCity, string costCenter, EmployeeType employeeType, string creator);
        Task UpdateAsync(Guid id, string no, string emailAddress, string chineseName, string displayName, Gender gender, DateTime onboardDate,
            string idCardNo, string phoneNumber, string baseCity, string workCity, string costCenter, EmployeeType employeeType, string updatedBy);
        Task RemoveAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsByNoAsync(string no);
        Task<bool> ExistsByEmailAddressAsync(string emailAddress);
        Task<bool> ExistsByIdCardNoAsync(string idCardNo);
        Task<bool> ExistsByNoAsync(Guid id, string no);
        Task<bool> ExistsByEmailAddressAsync(Guid id, string emailAddress);
        Task<bool> ExistsByIdCardNoAsync(Guid id, string idCardNo);
        Task<Employee> GetAsync(Guid id);
        Task<int> CountAsync(string keyword);
        Task<IList<Employee>> SearchAsync(string keyword, int pageIndex, int pageSize);
    }
}
