using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;
using HrMnager_mvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HrMnager_mvc.Constants;
using System.Security.Claims;
using HrMnager_mvc.Services.Implementations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HrMnager_mvc.Controllers
{
    [Authorize(Roles = RoleConstants.Employee + "," + RoleConstants.HrManager)]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IEmployeeService _employeeService;
        private readonly IHrManagerService _hrManagerService;

        public RequestController(IRequestService requestService, IEmployeeService employeeService, IHrManagerService hrManagerService)
        {
            _requestService = requestService;
            _hrManagerService = hrManagerService;
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Employee)]
        public IActionResult EmployeeRequests()
        {
            var employeeEmail = User.FindFirstValue(ClaimTypes.Email);
            var employee = _employeeService.FindByEmail(employeeEmail);
            if (employee == null)
            {
                return Forbid();
            }
            var requests = _requestService.GetRequestsForEmployee(employee.Id);
            return View("EmployeeRequests", requests);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.HrManager)]
        public IActionResult HrManagerRequests()
        {
            var hrManagerEmail = User.FindFirstValue(ClaimTypes.Email);
            var hrManager = _hrManagerService.GetHrManagerByEmail(hrManagerEmail);
            if (hrManager == null)
            {
                return Forbid();
            }
            var requests = _requestService.GetRequestsForHrManager(hrManagerEmail);
            return View("HrManagerRequests", requests);
        }


        [HttpGet]
        [Authorize(Roles = RoleConstants.Employee + "," + RoleConstants.HrManager)]
        public IActionResult RequestDetails(int id)
        {
            var request = _requestService.GetRequestById(id);
            if (request == null)
            {
                return NotFound();
            }
            return View("RequestDetails", request);
        }

        [HttpGet]
        public IActionResult CreateRequest()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Employee)]
        public IActionResult CreateRequest(Request request, int employeeId)
        {
            request.EmployeeId = employeeId;
            _requestService.CreateRequest(request);
            return RedirectToAction("EmployeeRequests");
        }

        public IActionResult ApproveRequest(int requestId)
        {
            var hrManagerEmail = User.FindFirstValue(ClaimTypes.Email);
            if (hrManagerEmail == null)
            {
                return Unauthorized("HR Manager email claim not found.");
            }

            var employeeId = _requestService.GetEmployeeIdByRequestId(requestId);
            if (employeeId == null)
            {
                return NotFound("Employee not found for the specified request.");
            }

            HttpContext.Session.SetString($"ApprovalMessage-{employeeId}", "Your request has been approved.");

            _requestService.ApproveRequest(requestId, hrManagerEmail);

            return RedirectToAction("HrManagerRequests");
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.HrManager)]
        public IActionResult RejectRequest(int requestId)
        {
            var hrManagerEmail = User.FindFirstValue(ClaimTypes.Email);
            if (hrManagerEmail == null)
            {
                return Unauthorized("HR Manager email claim not found.");
            }

            var employeeId = _requestService.GetEmployeeIdByRequestId(requestId);
            if (employeeId == null)
            {
                return NotFound("Employee not found for the specified request.");
            }

            var hrManager = _hrManagerService.GetHrManagerByEmail(hrManagerEmail);
            if (hrManager == null)
            {
                return NotFound("HR Manager not found.");
            }

             HttpContext.Session.SetString($"RejectionMessage-{employeeId}", "Your request has been rejected.");
            _requestService.RejectRequest(requestId, hrManagerEmail);
            return RedirectToAction("HrManagerRequests");
        }
    }
}