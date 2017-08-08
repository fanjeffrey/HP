using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.Extensions;
using HPCN.UnionOnline.Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger;

        public EmployeeController(
            IEmployeeService employeeService,
            ILoggerFactory loggerFactory
            )
        {
            _employeeService = employeeService;
            _logger = loggerFactory.CreateLogger<ActivityController>();
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1)
        {
            var pageSize = 30;
            var model = new EmployeeSearchViewModel
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = await _employeeService.CountAsync(keyword),
                Employees = await _employeeService.SearchAsync(keyword, pageIndex, pageSize)
            };

            return View(model);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(EmployeeAddViewModel model)
        {
            // check no
            if (await _employeeService.ExistsByNoAsync(model.No))
            {
                ModelState.AddModelError(string.Empty, $"Employee No: '{model.No}' already exists.");
            }

            // check email address
            if (await _employeeService.ExistsByEmailAddressAsync(model.EmailAddress))
            {
                ModelState.AddModelError(string.Empty, $"Email Address: '{model.EmailAddress}' already exists.");
            }

            // check id card no
            //if (await _employeeService.ExistsByIdCardNoAsync(model.IdCardNo))
            //{
            //    ModelState.AddModelError(string.Empty, $"Id Card No: '{model.IdCardNo}' already exists.");
            //}

            if (ModelState.IsValid)
            {
                // save to database
                await _employeeService.CreateAsync(
                    model.No, model.EmailAddress,
                    model.ChineseName, model.DisplayName,
                    model.OnboardDate, model.PhoneNumber,
                    model.ManagerEmail, model.TeamAdminAssistant,
                    model.IdCardNo, model.CostCenter,
                    model.BaseCity, model.WorkCity,
                    model.Gender, model.EmployeeType,
                    User.GetUsername());

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(new EmployeeEditViewModel
            {
                UserId = employee.UserId,
                No = employee.No,
                EmailAddress = employee.EmailAddress,
                ChineseName = employee.ChineseName,
                DisplayName = employee.DisplayName,
                OnboardDate = employee.OnboardDate,
                PhoneNumber = employee.PhoneNumber,
                ManagerEmail = employee.ManagerEmail,
                TeamAdminAssistant = employee.TeamAdminAssistant,
                IdCardNo = employee.IdCardNo,
                CostCenter = employee.CostCenter,
                BaseCity = employee.BaseCity,
                WorkCity = employee.WorkCity,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EmployeeEditViewModel model)
        {
            if (id != model.UserId)
            {
                return NotFound();
            }

            // check no
            if (await _employeeService.ExistsByNoAsync(id, model.No))
            {
                ModelState.AddModelError(string.Empty, $"Employee No: '{model.No}' already exists.");
            }

            // check email address
            if (await _employeeService.ExistsByEmailAddressAsync(id, model.EmailAddress))
            {
                ModelState.AddModelError(string.Empty, $"Email Address: '{model.EmailAddress}' already exists.");
            }

            // check id card no
            //if (await _employeeService.ExistsByIdCardNoAsync(id, model.IdCardNo))
            //{
            //    ModelState.AddModelError(string.Empty, $"Id Card No: '{model.IdCardNo}' already exists.");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    // save to database
                    await _employeeService.UpdateAsync(model.UserId,
                        model.No, model.EmailAddress,
                        model.ChineseName, model.DisplayName,
                        model.OnboardDate, model.PhoneNumber,
                        model.ManagerEmail, model.TeamAdminAssistant,
                        model.IdCardNo, model.CostCenter,
                        model.BaseCity, model.WorkCity,
                        model.Gender, model.EmployeeType,
                        User.GetUsername());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _employeeService.ExistsAsync(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _employeeService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}
