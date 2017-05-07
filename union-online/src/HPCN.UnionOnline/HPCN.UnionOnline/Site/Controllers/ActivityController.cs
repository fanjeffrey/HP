using HPCN.UnionOnline.Models;
using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.Extensions;
using HPCN.UnionOnline.Site.ViewModels;
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
    [Authorize(Policy = "AdminOnly")]
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IProductService _productService;
        private readonly ILogger _logger;

        public ActivityController(
            IActivityService activityService,
            IProductService productService,
            ILoggerFactory loggerFactory)
        {
            _activityService = activityService;
            _productService = productService;
            _logger = loggerFactory.CreateLogger<ActivityController>();
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1)
        {
            var pageSize = 20;
            var model = new ActivitySearchViewModel
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = await _activityService.CountAsync(keyword),
                Activities = await _activityService.SearchAsync(keyword, pageIndex, pageSize)
            };

            return View(model);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _activityService.GetActivityByIdAsync(id.Value);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityCreateViewModel model)
        {
            if (model.EndTime <= model.BeginTime)
            {
                ModelState.AddModelError(string.Empty, "End Time can't be earlier than Begin Time!");
            }

            if (await _activityService.ExistsAsync(model.Name))
            {
                ModelState.AddModelError(string.Empty, "Name alreay exists!");
            }

            if (ModelState.IsValid)
            {
                var creator = User.GetUsername();
                await _activityService.CreateAsync(model.Name, model.BeginTime, model.EndTime, model.Description, creator);
                _logger.LogInformation(1, $"{creator} created activity - {model.Name}");
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _activityService.GetActivityByIdAsync(id.Value);
            if (activity == null)
            {
                return NotFound();
            }

            return View(new ActivityEditViewModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                BeginTime = activity.BeginTime,
                EndTime = activity.EndTime
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ActivityEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (model.EndTime <= model.BeginTime)
            {
                ModelState.AddModelError(string.Empty, "End Time can't be earlier than Begin Time!");
            }

            if (await _activityService.ExistsAsync(model.Id, model.Name))
            {
                ModelState.AddModelError(string.Empty, "Another activity has the same name!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedBy = User.GetUsername();
                    await _activityService.UpdateAsync(model.Id, model.Name, model.BeginTime, model.EndTime, model.Description, updatedBy);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _activityService.ExistsAsync(id))
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
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _activityService.GetActivityByIdAsync(id.Value);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _activityService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Open(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _activityService.GetActivityByIdAsync(id.Value);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        [HttpPost, ActionName("Open")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OpenConfirmed(Guid id)
        {
            await _activityService.OpenAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Close(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _activityService.GetActivityByIdAsync(id.Value);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        [HttpPost, ActionName("Close")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseConfirmed(Guid id)
        {
            await _activityService.CloseAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Products(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _activityService.GetActivityByIdAsync(id.Value);
            if (activity == null)
            {
                return NotFound();
            }

            activity.ActivityProducts = await _activityService.GetProductsAsync(activity.Id);
            var model = new ActivityProductsViewModel
            {
                Activity = activity,
                ProdcutCatalog = _productService.GetProductsAsync(activity.ActivityProducts)
            };

            return View(model);
        }

        public async Task<IActionResult> AddProducts(Activity activity, IEnumerable<Product> products)
        {
            var productIds = from p in products where p != null select p.Id;
            if (products.Count() == 0)
            {
                return RedirectToAction(nameof(ActivityController.Products), new { Id = activity.Id });
            }

            await _activityService.AddProductsAsync(activity.Id, productIds, User.GetUsername());

            return RedirectToAction(nameof(ActivityController.Products), new { Id = activity.Id });
        }

        public async Task<IActionResult> RemoveProducts(Activity activity, IEnumerable<ActivityProduct> activityProducts)
        {
            var activityProductIds = from ap in activityProducts where ap != null select ap.Id;
            if (activityProductIds.Count() == 0)
            {
                return RedirectToAction(nameof(ActivityController.Products), new { Id = activity.Id });
            }

            await _activityService.RemoveActivityProductsAsync(activity.Id, activityProductIds, User.GetUsername());

            return RedirectToAction(nameof(ActivityController.Products), new { Id = activity.Id });
        }
    }
}
