using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HrMnager_mvc.Constants;
using HrMnager_mvc.Models.RequestModels;
using HrMnager_mvc.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrMnager_mvc.Controllers
{
    [Authorize(Roles = RoleConstants.HrManager)]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IHrManagerService _hrManagerService;

        public EmployeeController(IEmployeeService employeeService, IHrManagerService hrManagerService)
        {
            _employeeService = employeeService;
            _hrManagerService = hrManagerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var hrManagerEmail = User.FindFirstValue(ClaimTypes.Email);
                var response = _employeeService.GetAllEmployees(hrManagerEmail);
                if (response.Employees == null || !response.Employees.Any())
                {
                    ViewBag.Message = "No employees found.";
                }
                return View(response.Employees);
            }
            catch (ArgumentException ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var hrManagerEmail = User.FindFirstValue(ClaimTypes.Email);
                var response = _employeeService.GetEmployeeByEmail(id, hrManagerEmail);
                return View(response);
            }
            catch (ArgumentException ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var hrManagerEmail = User.FindFirstValue(ClaimTypes.Email);
                    _employeeService.AddEmployee(request, hrManagerEmail);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ViewBag.Message = ex.Message;
                    return View("Error");
                }
            }
            return View(request);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var hrManagerEmail = User.FindFirstValue(ClaimTypes.Email);
                var response = _employeeService.GetEmployeeByEmail(id, hrManagerEmail);
                UpdateEmployeeRequest request = new UpdateEmployeeRequest
                {
                    Id  = response.Id,
                    FullName = response.FullName,
                    DepartmentName = response.DepartmentName,
                    Salary = response.Salary,
                    Email = response.Email,
                    PhoneNumber = response.PhoneNumber
                };
                return View(request);
            }
            catch (ArgumentException ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Edit(UpdateEmployeeRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var hrManagerEmail = User.FindFirstValue(ClaimTypes.Email);
                    _employeeService.UpdateEmployee(request, hrManagerEmail);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ViewBag.Message = ex.Message;
                    return View("Error");
                }
            }
            return View(request);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var hrManagerEmail = User.FindFirstValue(ClaimTypes.Email);
                _employeeService.DeleteEmployee(id, hrManagerEmail);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

    }
}

