using HPCN.UnionOnline.Models;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IUserService
    {
        Task<User> FindByEmailAsync(string email);
    }
}
