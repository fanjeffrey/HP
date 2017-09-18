using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly string _cookieScheme = "HPCN.UnionOnline.CookieScheme";

        public AccountController(
            IAccountService accountService,
            IUserService userService,
            IEmailSender emailSender,
            ILoggerFactory loggerFactory)
        {
            _accountService = accountService;
            _userService = userService;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing cookie to ensure a clean login process
            await HttpContext.Authentication.SignOutAsync(_cookieScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _accountService.LoginAsync(model.Email, model.EmployeeNo, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                if (user.Disabled)
                {
                    _logger.LogWarning(2, "User account disabled.");
                    return View("Disabled");
                }

                var userClaims = new List<Claim>{
                        new Claim("userid", user.Id.ToString()),
                        new Claim("username", model.Email),
                        new Claim("isadmin", user.IsAdmin.ToString()),
                        new Claim("updatedtime", user.UpdatedTime.Value.Ticks.ToString())
                    };
                var principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "AuthenticationTypes.Password"));
                await HttpContext.Authentication.SignInAsync(_cookieScheme, principal);

                _logger.LogInformation(1, "User logged in.");
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync(_cookieScheme);
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _userService.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, $"Failed to find the user with this email: {model.Email}.");
                return View(model);
            }

            if (user.Employee == null)
            {
                ModelState.AddModelError(string.Empty, $"Failed to find the employee info with this email {model.Email}.");
                return View(model);
            }

            if (!user.Employee.No.Equals(model.EmployeeNo, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError(string.Empty, $"The email {model.Email} and the employee no {model.EmployeeNo} do not match.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var password = await _accountService.GenerateNewPasswordAsync(user.Id);
                await _emailSender.SendEmailAsync(model.Email, "SHP UNION PASSWORD RESET (DO NOT REPLY)", $"Your password: {password}");

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetSystemAdmin()
        {
            if ("localhost".Equals(Request.Host.Host, StringComparison.OrdinalIgnoreCase))
            {
                await _accountService.ResetSystemAdmin();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region helper methods

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
