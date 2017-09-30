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
    [Authorize(Policy = "EmployeeOnly")]
    public class EnrollingController : Controller
    {
        private readonly IEnrollingService _enrollingService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IUserService _userSerivce;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger;

        public EnrollingController(
            IEnrollingService enrollingService,
            IEnrollmentService enrollmentService,
            IUserService userService,
            IEmployeeService employeeService,
            ILoggerFactory loggerFactory)
        {
            _enrollingService = enrollingService;
            _enrollmentService = enrollmentService;
            _userSerivce = userService;
            _employeeService = employeeService;
            _logger = loggerFactory.CreateLogger<EnrollingController>();
        }

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

            var model = new EnrollingViewModel
            {
                Enrollment = enrollment,
            };

            // enrollment not ready
            if (!_enrollingService.IsReadyForEnrolling(enrollment))
            {
                return View("EnrollmentNotReady", model);
            }

            // exceed max count of enrollees
            if (await _enrollingService.ExceedsMaxCountOfEnrollees(enrollment))
            {
                return View("ExceedMaxCountOfEnrollees", model);
            }

            var user = await _userSerivce.GetUserWithEmployeeInfoAsync(Guid.Parse(User.GetUserId()));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(EnrollingViewModel model)
        {
            var enrollment = await _enrollmentService.GetEnrollmentIncludingFieldsAndChoicesAsync(model.Enrollment.Id);
            if (enrollment == null)
            {
                return NotFound();
            }

            model.Enrollment = enrollment;

            // enrollment not ready
            if (!_enrollingService.IsReadyForEnrolling(enrollment))
            {
                return View("EnrollmentNotReady", model);
            }

            // exceed max count of enrollees
            if (await _enrollingService.ExceedsMaxCountOfEnrollees(enrollment))
            {
                return View("ExceedMaxCountOfEnrollees", model);
            }

            // already enrolled
            if (await _enrollingService.IsAlreadyEnrolled(model.EmployeeNo, enrollment))
            {
                return View("AlreadyEnrolled", model);
            }

            // check if self-enroll only
            var user = await _userSerivce.GetUserWithEmployeeInfoAsync(Guid.Parse(User.GetUserId()));
            if (enrollment.SelfEnrollmentOnly && user.Employee.No != model.EmployeeNo)
            {
                return View("SelfEnrollmentOnly", model);
            }

            if (ModelState.IsValid)
            {
                var fieldInputs = (from item in Request.Form
                                   where item.Key.StartsWith("FieldInputs.")
                                   select item).ToDictionary(item => item.Key, item => item.Value.ToString());

                await _enrollingService.CreateAsync(enrollment.Id,
                    model.EmployeeNo, model.EmailAddress, model.Name, model.PhoneNumber, fieldInputs,
                    Guid.Parse(User.GetUserId()), User.GetUsername());

                return RedirectToAction("Enrollments");
            }

            return View(model);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolling = await _enrollingService.GetEnrollingIncludingEnrollmentAndFieldInputsAsync(id.Value);
            if (enrolling == null)
            {
                return NotFound();
            }

            var model = new EnrollingDetailsViewModel
            {
                Enrolling = enrolling,
                Enrollment = await _enrollmentService.GetEnrollmentIncludingFieldsAndChoicesAsync(enrolling.Enrollment.Id),
                Enrollees = await _enrollingService.GetEnrolleesAsync(enrolling.Enrollment.Id)
            };

            return View(model);
        }

        public async Task<IActionResult> QueryEmployee(string no)
        {
            if (string.IsNullOrWhiteSpace(no))
            {
                return Json(new
                {
                    emailAddress = string.Empty,
                    name = string.Empty,
                    phoneNumber = string.Empty
                });
            }

            var employee = await _employeeService.GetAsync(no);
            if (employee == null)
            {
                return Json(new
                {
                    emailAddress = string.Empty,
                    name = string.Empty,
                    phoneNumber = string.Empty
                });
            }

            return Json(new
            {
                emailAddress = employee.EmailAddress,
                name = employee.ChineseName,
                phoneNumber = employee.PhoneNumber
            });
        }
    }
}