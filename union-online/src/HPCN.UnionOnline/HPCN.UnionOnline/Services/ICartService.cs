using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface ICartService
    {
        Task Add(Guid activityProductId, int quantity, Guid userId);
        Task Update(Guid cartProductId, int quantity, string username);
        Task Remove(Guid id);
        Task<CartProduct> GetCartProductByIdAsync(Guid id);
        Task<int> Count(Guid userId);
        Task<IEnumerable<CartProduct>> GetCartItems(Guid userId);
        Task<IEnumerable<CartProduct>> GetCartItems(IEnumerable<Guid> cartProductIds);
    }
}
