using HPCN.UnionOnline.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly ILogger _logger;

        public HomeController(
            IActivityService activityService,
            ILoggerFactory loggerFactory)
        {
            _activityService = activityService;
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
