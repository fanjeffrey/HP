using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HPCN.UnionOnline.Data;
using HPCN.UnionOnline.Models;
using Microsoft.AspNetCore.Authorization;
using HPCN.UnionOnline.Site.ViewModels;
using HPCN.UnionOnline.Services;
using Microsoft.Extensions.Logging;
using HPCN.UnionOnline.Site.Extensions;

namespace HPCN.UnionOnline.Site.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ILogger _logger;

        public CartController(
            ICartService cartService,
            ILoggerFactory loggerFactory)
        {
            _cartService = cartService;
            _logger = loggerFactory.CreateLogger<CartController>();
        }

        public async Task<IActionResult> Index()
        {
            var cartItems = await _cartService.GetCartItems(Guid.Parse(User.GetUserId()));
            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CartAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _cartService.Add(model.ActivityProductId, model.Quantity, Guid.Parse(User.GetUserId()));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CartUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cartProduct = await _cartService.GetCartProductByIdAsync(model.CartProductId);
                if (cartProduct == null)
                {
                    return Json("Cart item not found.");
                }

                if (cartProduct.User == null ||
                    !cartProduct.User.Id.ToString().Equals(User.GetUserId(), StringComparison.OrdinalIgnoreCase))
                {
                    return Json("This cart item is not owned by you.");
                }

                await _cartService.Update(model.CartProductId, model.Quantity, User.GetUsername());
                return Json("Success");
            }
            return Json("Error");
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartProduct = await _cartService.GetCartProductByIdAsync(id.Value);
            if (cartProduct == null)
            {
                return NotFound();
            }

            return View(cartProduct);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _cartService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
