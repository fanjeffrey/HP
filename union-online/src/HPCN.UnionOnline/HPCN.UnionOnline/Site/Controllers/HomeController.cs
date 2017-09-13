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
    public class HomeController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IUserService _userSerivce;
        private readonly ILogger _logger;

        public HomeController(
            IActivityService activityService,
            IEnrollmentService enrollmentService,
            IUserService userService,
            ILoggerFactory loggerFactory)
        {
            _activityService = activityService;
            _enrollmentService = enrollmentService;
            _userSerivce = userService;
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
            var activeEnrollments = await _enrollmentService.GetActiveEnrollmentsAsync();
            if (activeEnrollments == null)
            {
                return View("NoActiveActivities");
            }

            return View(activeEnrollments);
        }

        [Authorize]
        public async Task<IActionResult> Enroll(Guid? id)
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

            var model = new EnrollViewModel
            {
                Enrollment = enrollment
            };

            var userId = Guid.Parse(User.GetUserId());
            var user = (await _userSerivce.GetUserWithEmployeeInfoAsync(userId));

            if (user?.Employee != null)
            {
                model.Enrollee = new Enrollee
                {
                    EmployeeNo = user.Employee.No,
                    EmailAddress = user.Employee.EmailAddress,
                    Name = user.Employee.ChineseName,
                    PhoneNumber = user.Employee.PhoneNumber
                };
            }
            else
            {
                model.Enrollee = new Enrollee
                {
                    EmailAddress = user.Username
                };
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
