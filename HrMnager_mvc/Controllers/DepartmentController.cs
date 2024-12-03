using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Constants;
using HrMnager_mvc.Models.RequestModels;
using HrMnager_mvc.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HrMnager_mvc.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var response = _departmentService.GetAllDepartments();
            return View(response.Departments);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var response = _departmentService.GetDepartmentById(id);
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
        public IActionResult Create(CreateDepartmentRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentService.AddDepartment(request);
                    return RedirectToAction(nameof(Index));
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
                var response = _departmentService.GetDepartmentById(id);
                UpdateDepartmentRequest request = new UpdateDepartmentRequest
                {
                    Id = response.Id,
                    Name = response.Name
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
        public IActionResult Edit(UpdateDepartmentRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentService.UpdateDepartment(request);
                    return RedirectToAction(nameof(Index));
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
                _departmentService.DeleteDepartment(id);
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