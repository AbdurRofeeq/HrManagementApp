using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Constants;
using HrMnager_mvc.Models.RequestModels;
using HrMnager_mvc.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrMnager_mvc.Controllers
{
    namespace YourNamespace.Controllers
    {
        [Authorize(Roles = RoleConstants.Admin)]
        public class HrManagerController : Controller
        {
            private readonly IHrManagerService _hrManagerService;

            public HrManagerController(IHrManagerService hrManagerService)
            {
                _hrManagerService = hrManagerService;
            }

            [HttpGet]
            public IActionResult Index()
            {
                try
                {
                    var response = _hrManagerService.GetAllHrManagers();
                    if (response.HrManagers == null || !response.HrManagers.Any())
                    {
                        ViewBag.Message = "No HR Managers found.";
                    }
                    return View(response.HrManagers);
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
                    var response = _hrManagerService.GetHrManagerById(id);
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
            public IActionResult Create(CreateHrManagerRequest request, int departmentId)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _hrManagerService.AddHrManager(request, departmentId);
                        return RedirectToAction(nameof(Index), new { departmentId });
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
                    var response = _hrManagerService.GetHrManagerById(id);
                    UpdateHrManagerRequest request = new UpdateHrManagerRequest
                    {
                        Id = response.Id,
                        FullName = response.FullName
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
            public IActionResult Edit(UpdateHrManagerRequest request, int departmentId)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _hrManagerService.UpdateHrManager(request, departmentId);
                        return RedirectToAction(nameof(Index), new { departmentId });
                    }
                    catch (ArgumentException ex)
                    {
                        ViewBag.Message = ex.Message;
                        return View("Error");
                    }
                }
                return View(request);
            }

            public IActionResult Delete(int id)
            {
                var hrManager = _hrManagerService.GetHrManagerById(id);
                if (hrManager == null)
                {
                    return NotFound();
                }
                return View(hrManager);
            }

            [HttpPost, ActionName("Delete")]
            public IActionResult DeleteConfirmed(int id)
            {
                try
                {
                    _hrManagerService.Delete(id);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ViewBag.Message = ex.Message;
                    return View("Error");
                }
            }

        }
    }

}