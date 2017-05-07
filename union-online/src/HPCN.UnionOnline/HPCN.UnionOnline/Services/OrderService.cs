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
    public class OrderService : IOrderService
    {
        private readonly HPCNUnionOnlineDbContext _db;
        private readonly ILogger _logger;

        public OrderService(
            HPCNUnionOnlineDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _db = dbContext;
            _logger = loggerFactory.CreateLogger<OrderService>();
        }

        public async Task<Order> Create(IEnumerable<CartProduct> cartProducts, Guid userId)
        {
            var user = await _db.Users
                .Include(u => u.Orders)
                .SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception($"Failed to find the user with the GUID: {userId}.");
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Status = OrderState.Created,
                Details = new List<OrderDetail>()
            };

            var now = DateTime.Now;

            foreach (var item in cartProducts)
            {
                var orderDetail = new OrderDetail
                {
                    Id = Guid.NewGuid(),
                    Quantity = item.Quantity,
                    PointsPayment = item.ActivityProduct.PointsPayment,
                    PointsPaymentAmount = item.ActivityProduct.PointsPayment * item.Quantity,
                    SelfPayment = item.ActivityProduct.SelfPayment,
                    SelfPaymentAmount = item.ActivityProduct.SelfPayment * item.Quantity,

                    ActivityId = item.ActivityProduct.Activity.Id,
                    AcitivityName = item.ActivityProduct.Activity.Name,
                    ActivityProductId = item.ActivityProduct.Id,
                    ProductName = item.ActivityProduct.Product.Name
                };
                orderDetail.UpdatedBy = orderDetail.CreatedBy = user.Username;
                orderDetail.UpdatedTime = orderDetail.CreatedTime = now;

                order.Details.Add(orderDetail);

                // remove this cart item
                var cartItem = await _db.CartProducts.SingleOrDefaultAsync(ci => ci.Id == item.Id);
                _db.CartProducts.Remove(cartItem);
            }

            order.UpdatedBy = order.CreatedBy = user.Username;
            order.UpdatedTime = order.CreatedTime = now;
            order.PointsAmount = order.Details.Sum(d => d.PointsPaymentAmount);
            order.MoneyAmount = order.Details.Sum(d => d.SelfPaymentAmount);

            user.Orders.Add(order);

            await _db.SaveChangesAsync();

            return order;
        }

        public async Task CancelAsync(Guid id, string updatedBy)
        {
            var order = await _db.Orders.SingleOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                throw new Exception($"Failed to find the order with the GUID: {id}.");
            }

            if (order.Status == OrderState.Completed || order.Status == OrderState.Aborted)
            {
                throw new Exception($"An order in status 'Completed' or 'Aborted' can't be cancelled.");
            }

            if (order.Status == OrderState.Created)
            {
                order.Status = OrderState.Cancelled;
                order.UpdatedBy = updatedBy;
                order.UpdatedTime = DateTime.Now;

                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _db.Orders.AnyAsync(o => o.Id == id);
        }

        public async Task<int> Count(Guid userId)
        {
            return await _db.Orders.Where(o => o.User.Id == userId).CountAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(Guid userId)
        {
            return await _db.Orders
                .Include(o => o.Details)
                .Where(o => o.User.Id == userId)
                .ToListAsync();
        }
    }
}
