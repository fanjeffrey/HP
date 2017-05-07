using HPCN.UnionOnline.Data;
using HPCN.UnionOnline.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public class CartService : ICartService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public CartService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<CartService>();
        }

        public async Task Add(Guid activityProductId, int quantity, Guid userId)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException(nameof(quantity));
            }

            var user = await _db.Users
                .Include(u => u.CartPoducts)
                .SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception($"Failed to find the user: {userId}.");
            }

            var activityProduct = await _db.ActivityProducts
                .Include(ap => ap.CartProducts)
                .FirstOrDefaultAsync(ap => ap.Id == activityProductId);
            if (activityProduct == null)
            {
                throw new Exception($"Failed to find the activity product: {activityProductId}.");
            }

            CartProduct cartItem = null;

            try
            {
                cartItem = await _db.CartProducts
                    .SingleOrDefaultAsync(cp => cp.ActivityProduct.Id == activityProduct.Id && cp.User.Id == user.Id);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Sequence contains more than one element"))
            {
                _logger.LogInformation("Merge multiple rows into one.");

                var list = await _db.CartProducts
                    .Where(cp => cp.ActivityProduct.Id == activityProduct.Id && cp.User.Id == user.Id)
                    .OrderBy(cp => cp.UpdatedTime)
                    .ToListAsync();

                cartItem = list.FirstOrDefault();
                if (cartItem != null)
                {
                    var rest = list.Skip(1);
                    cartItem.Quantity += rest.Sum(cp => cp.Quantity);
                    _db.CartProducts.RemoveRange(rest);
                }
            }

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                cartItem.UpdatedTime = DateTime.Now;
                cartItem.UpdatedBy = user.Username;
            }
            else
            {
                cartItem = new CartProduct
                {
                    Id = Guid.NewGuid(),
                    Quantity = quantity
                };

                cartItem.UpdatedTime = cartItem.CreatedTime = DateTime.Now;
                cartItem.UpdatedBy = cartItem.CreatedBy = user.Username;

                user.CartPoducts.Add(cartItem);
                activityProduct.CartProducts.Add(cartItem);
            }

            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid cartProductId, int quantity, string username)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException($"Argument {nameof(quantity)} can't be less than or equal to ZERO.");
            }

            var cartItem = await _db.CartProducts.SingleOrDefaultAsync(cp => cp.Id == cartProductId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                cartItem.UpdatedTime = DateTime.Now;
                cartItem.UpdatedBy = username;

                await _db.SaveChangesAsync();
            }
        }

        public async Task Remove(Guid id)
        {
            var cp = await _db.CartProducts.SingleOrDefaultAsync(i => i.Id == id);
            if (cp != null)
            {
                _db.CartProducts.Remove(cp);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<CartProduct> GetCartProductByIdAsync(Guid id)
        {
            return await _db.CartProducts
                .Include(i => i.ActivityProduct)
                .Include(ap => ap.ActivityProduct.Activity)
                .Include(ap => ap.ActivityProduct.Product)
                .SingleOrDefaultAsync(cp => cp.Id == id);
        }

        public async Task<int> Count(Guid userId)
        {
            return await _db.CartProducts
                .Where(ci => ci.User.Id == userId)
                .CountAsync();
        }

        public async Task<IEnumerable<CartProduct>> GetCartItems(Guid userId)
        {
            return await _db.CartProducts
                .Include(i => i.ActivityProduct)
                .Include(ap => ap.ActivityProduct.Activity)
                .Include(ap => ap.ActivityProduct.Product)
                .Where(ci => ci.User.Id == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CartProduct>> GetCartItems(IEnumerable<Guid> cartProductIds)
        {
            return await _db.CartProducts
                .Include(i => i.ActivityProduct)
                .Include(ap => ap.ActivityProduct.Activity)
                .Include(ap => ap.ActivityProduct.Product)
                .Where(ci => cartProductIds.Contains(ci.Id))
                .ToListAsync();
        }
    }
}
