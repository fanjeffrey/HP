using HPCN.UnionOnline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public class OperationService : IOperationService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public OperationService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<OperationService>();
        }

        public async Task LogProductCreation(Guid productId, string username)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
            product.UpdatedTime = product.CreatedTime = DateTime.Now;
            product.UpdatedBy = product.CreatedBy = username;

            await _db.SaveChangesAsync();
        }

        public async Task LogProductUpdate(Guid productId, string username)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
            product.UpdatedTime = DateTime.Now;
            product.UpdatedBy = username;

            await _db.SaveChangesAsync();
        }
    }
}
