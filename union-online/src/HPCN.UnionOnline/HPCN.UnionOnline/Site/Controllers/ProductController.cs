using HPCN.UnionOnline.Data;
using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.Extensions;
using HPCN.UnionOnline.Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductPictureService _pictureService;
        private readonly ProductPictureOptions _pictureOptions;
        private readonly IOperationService _operationService;
        private readonly ILogger _logger;

        public ProductController(
            IProductService productService,
            IProductPictureService pictureService,
            IOptions<ProductPictureOptions> _pictureOptionsAccessor,
            IOperationService operationService,
            ILoggerFactory loggerFactory
            )
        {
            _productService = productService;
            _pictureService = pictureService;
            _pictureOptions = _pictureOptionsAccessor.Value;
            _operationService = operationService;
            _logger = loggerFactory.CreateLogger<ProductController>();
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1)
        {
            var pageSize = 30;
            var model = new ProductSearchViewModel
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = await _productService.CountAsync(keyword),
                Products = await _productService.SearchAsync(keyword, pageIndex, pageSize)
            };

            return View(model);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductAddViewModel model)
        {
            if (model.Picture != null && !_pictureService.ValidateContentType(model.Picture.ContentType))
            {
                ModelState.AddModelError(string.Empty, "Accept only PNG or JPG file.");
            }

            if (ModelState.IsValid)
            {
                var productId = Guid.NewGuid();
                var pictureFileName = await _pictureService.Save(model.Picture, _pictureOptions, productId);
                var newProduct = await _productService.AddAsync(
                    productId,
                    model.Name,
                    model.PointsPayment,
                    model.SelfPayment,
                    model.Description,
                    pictureFileName);

                await _operationService.LogProductCreation(newProduct.Id, User.GetUsername());

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(new ProductEditViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PointsPayment = product.PointsPayment,
                SelfPayment = product.SelfPayment,
                PictureFileName = product.PictureFileName
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (model.Picture != null && !_pictureService.ValidateContentType(model.Picture.ContentType))
            {
                ModelState.AddModelError(string.Empty, "Accept only PNG or JPG file.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Picture != null && model.Picture.Length > 0)
                    {
                        model.PictureFileName = await _pictureService.Save(model.Picture, _pictureOptions, model.Id);
                    }

                    await _productService.UpdateAsync(
                           model.Id,
                           model.Name,
                           model.Description,
                           model.PointsPayment,
                           model.SelfPayment,
                           model.PictureFileName);

                    await _operationService.LogProductUpdate(model.Id, User.GetUsername());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _productService.ExistsAsync(model.Id))
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

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _productService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}
