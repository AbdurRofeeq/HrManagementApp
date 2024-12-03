using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;

namespace HrMnager_mvc.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        IEnumerable<Request> GetAllRequests();
        Request GetRequestById(int requestId);
        void AddRequest(Request request);
        void UpdateRequest(Request request);
        void DeleteRequest(int requestId);
        Department GetDepartmentById(int departmentId);
    }
}