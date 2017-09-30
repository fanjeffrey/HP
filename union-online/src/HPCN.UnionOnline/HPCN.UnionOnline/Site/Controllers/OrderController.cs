using HPCN.UnionOnline.Models;
using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class OrderController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IPointsService _pointsService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public OrderController(
            ICartService cartService,
            IOrderService orderService,
            IPointsService pointsService,
            IUserService userService,
            ILoggerFactory loggerFactory)
        {
            _cartService = cartService;
            _orderService = orderService;
            _pointsService = pointsService;
            _userService = userService;
            _logger = loggerFactory.CreateLogger<OrderController>();
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetOrdersByUserId(Guid.Parse(User.GetUserId()));
            return View(orders.OrderByDescending(o => o.CreatedTime));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IEnumerable<CartProduct> cartProducts)
        {
            var cartProductIds = from cp in cartProducts where cp != null select cp.Id;
            if (cartProductIds.Count() == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            cartProducts = await _cartService.GetCartItems(cartProductIds);

            // user view his cart items, and then admin close the activity 
            // (all the the products of this activity in user's cart will be removed)
            // user create an order with removed item(s).
            if (cartProductIds.Count() > cartProducts.Count())
            {
                ViewBag.CountOfMissedCartItems = cartProductIds.Count() - cartProducts.Count();
                return View("CartItemsMissed");
            }

            var invalidCartItems = cartProducts.Where(cp =>
                cp.ActivityProduct.Activity.Status == ActivityState.Closed ||
                cp.ActivityProduct.Activity.EndTime < DateTime.Now).ToList();
            if (invalidCartItems.Count() > 0)
            {
                return View("CartItemsInvalid", invalidCartItems);
            }

            var pointsNeeded = cartProducts.Sum(cp => cp.Quantity * cp.ActivityProduct.PointsPayment);
            var pointsBalance = await _pointsService.CalculatePointsBalance(Guid.Parse(User.GetUserId()));
            if (pointsNeeded > pointsBalance)
            {
                return View("NoEnoughPoints");
            }

            // create order
            var order = await _orderService.Create(cartProducts, Guid.Parse(User.GetUserId()));

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(Guid id)
        {
            try
            {
                await _orderService.CancelAsync(id, User.GetUsername());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _orderService.ExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index");
        }
    }
}
