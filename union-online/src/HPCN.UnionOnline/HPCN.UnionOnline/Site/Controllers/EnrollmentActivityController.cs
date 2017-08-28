using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.Extensions;
using HPCN.UnionOnline.Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class EnrollmentActivityController : Controller
    {
        private readonly IEnrollmentActivityService _activityService;
        private readonly ILogger _logger;

        public EnrollmentActivityController(
            IEnrollmentActivityService activityService,
            ILoggerFactory loggerFactory)
        {
            _activityService = activityService;
            _logger = loggerFactory.CreateLogger<EnrollmentActivityController>();
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1)
        {
            var pageSize = 20;
            var model = new EnrollmentActivitySearchViewModel
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = await _activityService.CountAsync(keyword),
                Activities = await _activityService.SearchAsync(keyword, pageIndex, pageSize)
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnrollmentActivityCreateViewModel model)
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
    }
}
