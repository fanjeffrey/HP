using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IProductService
    {
        Task<Product> AddAsync(Guid productId, string name, double pointsPayment, double selfPayment, string description, string pictureFileName);
        Task UpdateAsync(Guid id, string name, string description, double pointsPayment, double selfPayment, string pictureFileName);
        Task RemoveAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<int> CountAsync(string keyword);
        Task<IList<Product>> SearchAsync(string keyword, int pageIndex, int pageSize);
        IQueryable<Product> GetProductsAsync(ICollection<ActivityProduct> activityProducts);
    }
}
