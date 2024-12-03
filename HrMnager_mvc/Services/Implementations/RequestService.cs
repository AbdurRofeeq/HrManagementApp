using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Constants;
using HrMnager_mvc.Entities;
using HrMnager_mvc.Enums;
using HrMnager_mvc.Repositories;
using HrMnager_mvc.Repositories.Implementations;
using HrMnager_mvc.Repositories.Interfaces;
using HrMnager_mvc.Services.Interfaces;

namespace HrMnager_mvc.Services.Implementations
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repository;
        private readonly IHrManagerRepository _hrManagerRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RequestService(IRequestRepository repository, IHrManagerRepository hrManagerRepository, IEmployeeRepository employeeRepository)
        {
            _repository = repository;
            _hrManagerRepository = hrManagerRepository;
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<Request> GetRequestsForEmployee(int employeeId)
        {
            return _repository.GetAllRequests().Where(r => r.EmployeeId == employeeId);
        }

        public IEnumerable<Request> GetRequestsForHrManager(int requestId)
        {
            var manager = _repository.GetRequestById(requestId);
            if (manager == null || manager.Employee.Role != RoleConstants.HrManager)
            {
                return Enumerable.Empty<Request>();
            }

            var department = _repository.GetDepartmentById(manager.Employee.DepartmentId);
            if (department == null || department.HrManager.Id != requestId)
            {
                return Enumerable.Empty<Request>();
            }

            return _repository.GetAllRequests().Where(r => r.Employee.DepartmentId == department.Id);
        }

        public Request GetRequestById(int requestId)
        {
            return _repository.GetRequestById(requestId);
        }

        public void CreateRequest(Request request)
        {
            request.Status = RequestStatus.Pending;
            request.DateCreated = DateTime.UtcNow;
            _repository.AddRequest(request);
        }

        public void ApproveRequest(int requestId, int managerId)
        {
            var request = _repository.GetRequestById(requestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }

            var manager = _repository.GetRequestById(managerId);
            if (manager == null || manager.Employee.Role != RoleConstants.HrManager)
            {
                throw new Exception("Manager not found or not authorized");
            }

            if (request.Employee.DepartmentId == manager.Employee.DepartmentId)
            {
                request.Status = RequestStatus.Approved;
                request.ApprovedById = managerId;
                request.DateApproved = DateTime.UtcNow;
                _repository.UpdateRequest(request);
            }
            else
            {
                throw new Exception("Manager not authorized to approve this request");
            }
        }

        public IEnumerable<Request> GetRequestsForHrManager(string managerEmail)
        {
            var hrManager = _hrManagerRepository.FindByEmail(managerEmail);
            var department = _repository.GetDepartmentById(hrManager.DepartmentId);
            if (department == null || department.HrManager.Id != hrManager.Id || hrManager == null)
            {
                return Enumerable.Empty<Request>();
            }

            return _repository.GetAllRequests().Where(r => r.Employee.DepartmentId == department.Id);
        }



        public void ApproveRequest(int requestId, string hrManagerEmail)
        {
            var request = _repository.GetRequestById(requestId);
            var hrManager = _hrManagerRepository.FindByEmail(hrManagerEmail);
            if (request == null || hrManager == null)
            {
                throw new Exception("Request Or HrManager not found");
            }

            if (request.Employee.DepartmentId == hrManager.DepartmentId)
            {
                request.Status = RequestStatus.Approved;
                request.ApprovedById = hrManager.Id;
                request.DateApproved = DateTime.UtcNow;
                _repository.UpdateRequest(request);
            }
            else
            {
                throw new Exception("Manager not authorized to approve this request");
            }
        }

        public void RejectRequest(int requestId, string hrManagerEmail)
        {
            var request = _repository.GetRequestById(requestId);
            var hrManager = _hrManagerRepository.FindByEmail(hrManagerEmail);
            if (request == null || hrManager == null)
            {
                throw new Exception("Request Or HrManager not found");
            }

            if (request.Employee.DepartmentId == hrManager.DepartmentId)
            {
                request.Status = RequestStatus.Rejected;
                _repository.UpdateRequest(request);
            }
            else
            {
                throw new Exception("Manager not authorized to reject this request");
            }
        }

        public int? GetEmployeeIdByRequestId(int requestId)
        {
            var request = _repository.GetRequestById(requestId);
            return request?.EmployeeId;
        }

    }
}