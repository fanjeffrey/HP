using HPCN.UnionOnline.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class EnrollController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger;

        public EnrollController(
            IEmployeeService employeeService,
            ILoggerFactory loggerFactory)
        {
            _employeeService = employeeService;
            _logger = loggerFactory.CreateLogger<EnrollController>();
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