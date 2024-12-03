using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;

namespace HrMnager_mvc.Services.Interfaces
{
    public interface IRequestService
    {
        IEnumerable<Request> GetRequestsForEmployee(int employeeId);
        IEnumerable<Request> GetRequestsForHrManager(int managerId);
        IEnumerable<Request> GetRequestsForHrManager(string managerEmail);
        Request GetRequestById(int requestId);
        void CreateRequest(Request request);
        void ApproveRequest(int requestId, int managerId);
        void ApproveRequest(int requestId, string hrManagerEmail);
        void RejectRequest(int requestId, string hrManagerEmail);
        int? GetEmployeeIdByRequestId(int requestId);
    }
}