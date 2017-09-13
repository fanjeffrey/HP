using HPCN.UnionOnline.Models;
using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.Extensions;
using HPCN.UnionOnline.Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ILogger _logger;

        public EnrollmentController(
            IEnrollmentService enrollmentService,
            ILoggerFactory loggerFactory)
        {
            _enrollmentService = enrollmentService;
            _logger = loggerFactory.CreateLogger<EnrollmentController>();
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1)
        {
            var pageSize = 20;
            var model = new EnrollmentSearchViewModel
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = await _enrollmentService.CountEnrollmentsAsync(keyword),
                Enrollments = await _enrollmentService.SearchEnrollmentsAsync(keyword, pageIndex, pageSize)
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View(new EnrollmentCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnrollmentCreateViewModel model)
        {
            if (model.EndTime <= model.BeginTime)
            {
                ModelState.AddModelError(string.Empty, "End Time can't be earlier than Begin Time!");
            }

            if (model.MaxCountOfEnrollees <= 0)
            {
                ModelState.AddModelError(string.Empty, "Max count of enrollees must be larger than Zero!");
            }

            if (await _enrollmentService.ExistsEnrollmentAsync(model.Name))
            {
                ModelState.AddModelError(string.Empty, "Name alreay exists!");
            }

            if (ModelState.IsValid)
            {
                var creator = User.GetUsername();
                await _enrollmentService.CreateEnrollmentAsync(model.Name, model.BeginTime, model.EndTime, model.Description, model.MaxCountOfEnrollees, model.SelfEnrollmentOnly, creator);
                _logger.LogInformation(1, $"{creator} created enrollment - {model.Name}");
                return RedirectToAction("Index");
            }

            return View(model);
        }

        #region actions about Fields

        public async Task<IActionResult> Fields(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentService.GetEnrollmentIncludingFieldsAsync(id.Value);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        public async Task<IActionResult> AddField(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentService.GetEnrollmentAsync(id.Value);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(new EnrollmentAddFieldViewModel
            {
                Enrollment = enrollment,
                DisplayOrder = 0
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddField(FieldEntry field)
        {
            if (!await _enrollmentService.ExistsEnrollmentAsync(field.Enrollment.Id))
            {
                return NotFound();
            }

            await _enrollmentService.AddFieldAsync(field, User.GetUsername());

            return RedirectToAction(nameof(EnrollmentController.Fields), new { Id = field.Enrollment.Id });
        }

        public async Task<IActionResult> FieldDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var field = await _enrollmentService.GetFieldIncludingEnrollmentAndValueChoicesAsync(id.Value);
            if (field == null)
            {
                return NotFound();
            }

            return View(field);
        }

        public async Task<IActionResult> EditField(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var field = await _enrollmentService.GetFieldIncludingEnrollmentAndValueChoicesAsync(id.Value);
            if (field == null)
            {
                return NotFound();
            }

            return View(field);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditField(FieldEntry field)
        {
            if (!await _enrollmentService.ExistsEnrollmentAsync(field.Enrollment.Id))
            {
                return NotFound();
            }

            if (!await _enrollmentService.ExistsFieldAsync(field.Id))
            {
                return NotFound();
            }

            await _enrollmentService.UpdateFieldAsync(field, User.GetUsername());

            return RedirectToAction(nameof(EnrollmentController.Fields), new { Id = field.Enrollment.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFields(Guid id, Guid[] fieldIds)
        {
            if (fieldIds == null || fieldIds.Length == 0)
            {
                return RedirectToAction(nameof(EnrollmentController.Fields), new { Id = id });
            }

            if (!await _enrollmentService.ExistsEnrollmentAsync(id))
            {
                return NotFound();
            }

            await _enrollmentService.RemoveFieldsAsync(fieldIds);

            return RedirectToAction(nameof(EnrollmentController.Fields), new { Id = id });
        }

        #endregion
    }
}