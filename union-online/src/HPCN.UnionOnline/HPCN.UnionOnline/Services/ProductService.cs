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
    public class ProductService : IProductService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public ProductService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<ProductService>();
        }

        public async Task<Product> AddAsync(Guid id, string name, double pointsPayment, double selfPayment, string description, string pictureFileName)
        {
            name = name?.Trim();

            var product = new Product
            {
                Id = id,
                Name = name,
                PointsPayment = pointsPayment,
                SelfPayment = selfPayment,
                Description = description,
                PictureFileName = pictureFileName
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return product;
        }

        public async Task UpdateAsync(Guid id, string name, string description, double pointsPayment, double selfPayment, string pictureFileName)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                product.Name = name;
                product.Description = description;
                product.PointsPayment = pointsPayment;
                product.SelfPayment = selfPayment;
                product.PictureFileName = pictureFileName;

                await _db.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _db.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _db.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> CountAsync(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _db.Products.CountAsync();
            }

            return await _db.Products
                .Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword))
                .CountAsync();
        }

        public async Task<IList<Product>> SearchAsync(string keyword, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 20 : pageSize;
            keyword = keyword?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _db.Products
                    .OrderBy(p => p.Name)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return await _db.Products
                .Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword))
                .OrderBy(p => p.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public IQueryable<Product> GetProductsAsync(ICollection<ActivityProduct> activityProducts)
        {
            var productIds = from ap in activityProducts where ap != null && ap.Product != null select ap.Product.Id;
            return (from p in _db.Products
                    where !productIds.Any(id => id == p.Id)
                    select p).AsNoTracking();
        }
    }
}
