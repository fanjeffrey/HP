using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.Extensions;
using HPCN.UnionOnline.Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IEnrollingService _enrollingService;
        private readonly IUserService _userSerivce;
        private readonly ILogger _logger;

        public HomeController(
            IActivityService activityService,
            IEnrollmentService enrollmentService,
            IEnrollingService enrollingService,
            IUserService userService,
            ILoggerFactory loggerFactory)
        {
            _activityService = activityService;
            _enrollmentService = enrollmentService;
            _enrollingService = enrollingService;
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

            var enrollment = await _enrollmentService.GetEnrollmentIncludingFieldsAndChoicesAsync(id.Value);

            // enrollment not found
            if (enrollment == null)
            {
                return NotFound();
            }

            // enrollment not ready
            if (!_enrollingService.IsReadyForEnrolling(enrollment))
            {
                return View("EnrollmentNotReady", enrollment);
            }

            // exceed max count of enrollees
            if (await _enrollingService.ExceedsMaxCountOfEnrollees(enrollment))
            {
                return View("ExceedMaxCountOfEnrollees", enrollment);
            }

            var model = new EnrollingViewModel
            {
                Enrollment = enrollment,
            };

            var userId = Guid.Parse(User.GetUserId());
            var user = (await _userSerivce.GetUserWithEmployeeInfoAsync(userId));
            if (user?.Employee != null)
            {
                model.EmployeeNo = user.Employee.No;
                model.EmailAddress = user.Employee.EmailAddress;
                model.Name = user.Employee.ChineseName;
                model.PhoneNumber = user.Employee.PhoneNumber;
            }
            else
            {
                model.EmailAddress = user.Username;
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(EnrollingViewModel model)
        {
            var enrollment = await _enrollmentService.GetEnrollmentIncludingFieldsAndChoicesAsync(model.Enrollment.Id);
            if (enrollment == null)
            {
                return NotFound();
            }

            if (!_enrollingService.IsReadyForEnrolling(enrollment))
            {
                return View("EnrollmentNotReady", enrollment);
            }

            model.Enrollment = enrollment;

            // already enrolled
            if (await _enrollingService.IsAlreadyEnrolled(model.EmployeeNo, model.Enrollment))
            {
                return View("AlreadyEnrolled", model);
            }

            if (ModelState.IsValid)
            {
                var fieldInputs = (from item in Request.Form
                                   where item.Key.StartsWith("FieldInputs.")
                                   select item).ToDictionary(item => item.Key, item => item.Value.ToString());

                await _enrollingService.CreateAsync(model.Enrollment.Id,
                    model.EmployeeNo, model.EmailAddress, model.Name, model.PhoneNumber, fieldInputs,
                    Guid.Parse(User.GetUserId()), User.GetUsername());

                return RedirectToAction("Enrollments");
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
