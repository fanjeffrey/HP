using HPCN.UnionOnline.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IEnrollmentActivityService _enrollmentActivityService;
        private readonly ILogger _logger;

        public HomeController(
            IActivityService activityService,
            IEnrollmentActivityService enrollmentActivityService,
            ILoggerFactory loggerFactory)
        {
            _activityService = activityService;
            _enrollmentActivityService = enrollmentActivityService;
            _logger = loggerFactory.CreateLogger<HomeController>();
        }

        public IActionResult Index()
        {
            return RedirectToAction("Exchange");
        }

        public async Task<IActionResult> Exchange()
        {
            var activity = await _activityService.GetActiveActivityAsync();
            if (activity == null)
            {
                return View("NoActiveActivities");
            }

            return View(activity);
        }

        public async Task<IActionResult> Enrollments()
        {
            var activities = await _enrollmentActivityService.GetActiveActivitiesAsync();
            if (activities == null)
            {
                return View("NoActiveActivities");
            }

            return View(activities);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
