using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class PortalController : Controller
    {
        private readonly IEnrollingService _enrollingService;
        private readonly IUserService _userSerivce;
        private readonly ILogger _logger;

        public PortalController(
            IEnrollingService enrollingService,
            IUserService userService,
            ILoggerFactory loggerFactory)
        {
            _enrollingService = enrollingService;
            _userSerivce = userService;
            _logger = loggerFactory.CreateLogger<HomeController>();
        }

        public async Task<IActionResult> Index()
        {
            var myEnrollings = await _enrollingService.GetEnrollingsAsync(Guid.Parse(User.GetUserId()));
            var enrollmentsInMyEnrollings = myEnrollings.Select(e => e.Enrollment.Id);
            var enrolleesInEnrollments = await _enrollingService.GetEnrolleesInEnrollments(enrollmentsInMyEnrollings);

            ViewBag.MyEnrollings = myEnrollings;
            ViewBag.EnrolleesInEnrollments = enrolleesInEnrollments;
            return View();
        }
    }
}