using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IEnrollingService _enrollingService;
        private readonly ILogger _logger;

        public HomeController(
            IActivityService activityService,
            IEnrollmentService enrollmentService,
            IEnrollingService enrollingService,
            ILoggerFactory loggerFactory)
        {
            _activityService = activityService;
            _enrollmentService = enrollmentService;
            _enrollingService = enrollingService;
            _logger = loggerFactory.CreateLogger<HomeController>();
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
            else if (User.IsAdmin())
            {
                return RedirectToAction(nameof(ActivityController.Index), "Activity");
            }
            else
            {
                return RedirectToAction(nameof(PortalController.Index), "portal");
            }
        }

        [Authorize(Policy = "EmployeeOnly")]
        public async Task<IActionResult> Exchange()
        {
            var activity = await _activityService.GetActiveActivityAsync();
            if (activity == null)
            {
                return View("NoActiveActivities");
            }

            return View(activity);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
