using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IOrderService
    {
        Task<Order> Create(IEnumerable<CartProduct> cartProducts, Guid userId);
        Task CancelAsync(Guid id, string updatedBy);
        Task<bool> ExistsAsync(Guid id);
        Task<int> Count(Guid userId);
        Task<IEnumerable<Order>> GetOrdersByUserId(Guid guid);
    }
}
