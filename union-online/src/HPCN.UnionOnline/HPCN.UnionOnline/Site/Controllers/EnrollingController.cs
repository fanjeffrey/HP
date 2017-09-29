using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class EnrollingController : Controller
    {
        private readonly IEnrollingService _enrollingService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly ILogger _logger;

        public EnrollingController(
            IEnrollingService enrollingService,
            IEnrollmentService enrollmentService,
            ILoggerFactory loggerFactory)
        {
            _enrollingService = enrollingService;
            _enrollmentService = enrollmentService;
            _logger = loggerFactory.CreateLogger<EnrollingController>();
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
    }
}