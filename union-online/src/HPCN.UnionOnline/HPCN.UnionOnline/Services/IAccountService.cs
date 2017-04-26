using HPCN.UnionOnline.Models;
using System;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IAccountService
    {
        Task<LoginResult> LoginAsync(string email, string employeeNo, string password);
        Task ResetSystemAdmin();
        Task<bool> ValidatePrincipal(string username, string updatedTime);
    }

    public class LoginResult
    {
        public bool Succeeded { get; set; } = false;
        public User User { get; set; }
    }
}
