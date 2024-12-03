using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HrMnager_mvc.Constants;
using HrMnager_mvc.Models;
using HrMnager_mvc.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace HrMnager_mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminDashBoard()
        {
            return View();
        }

        public IActionResult HrManagerDashBoard()
        {
            return View();
        }

        public IActionResult EmployeeDashBoard()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userService.Login(model);
                if (response.Status)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, response.Email),
                        new Claim(ClaimTypes.NameIdentifier, response.EmployeeId.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, response.HrManagerId.ToString()),
                    };
                    claims.Add(new Claim(ClaimTypes.Role, response.Role));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    if (response.Role == RoleConstants.Admin)
                    {
                        return RedirectToAction("AdminDashBoard");
                    }
                    if (response.Role == RoleConstants.HrManager)
                    {
                        return RedirectToAction("HrManagerDashBoard");
                    }
                    if (response.Role == RoleConstants.Employee)
                    {
                        var approvalMessage = HttpContext.Session.GetString($"ApprovalMessage-{response.EmployeeId}");
                        var rejectionMessage = HttpContext.Session.GetString($"RejectionMessage-{response.EmployeeId}");

                        ViewData["ApprovalMessage"] = approvalMessage;
                        ViewData["RejectionMessage"] = rejectionMessage;
                        return RedirectToAction("EmployeeDashBoard");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}