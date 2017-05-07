using HPCN.UnionOnline.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace HPCN.UnionOnline.Services
{
    public interface IUserService
    {
        Task<User> FindByEmailAsync(string email);
        Task<User> GetUserWithEmployeeInfoAsync(Guid guid);
    }
}
