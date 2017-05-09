using HPCN.UnionOnline.Models;
using System;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IAccountService
    {
        Task<User> LoginAsync(string email, string employeeNo, string password);
        Task ResetSystemAdmin();
        Task<bool> ValidatePrincipal(string username, string updatedTime);
        Task<string> GenerateNewPasswordAsync(Guid userId);
    }
}
