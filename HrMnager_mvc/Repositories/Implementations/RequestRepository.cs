using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Context;
using HrMnager_mvc.Entities;
using HrMnager_mvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HrMnager_mvc.Repositories.Implementations
{
    public class RequestRepository : IRequestRepository
    {
        private readonly HrManagerContext _context;

        public RequestRepository(HrManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<Request> GetAllRequests()
        {
            return _context.Requests.Include(r => r.Employee)
                                    .Include(r => r.ApprovedBy)
                                    .ToList();
        }

        public Request? GetRequestById(int requestId)
        {
            return _context.Requests.Include(r => r.Employee)
                                    .Include(r => r.ApprovedBy)
                                    .FirstOrDefault(r => r.Id == requestId);
        }

        public void AddRequest(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
        }

        public void UpdateRequest(Request request)
        {
            _context.Requests.Update(request);
            _context.SaveChanges();
        }

        public void DeleteRequest(int requestId)
        {
            var request = _context.Requests.Find(requestId);
            if (request != null)
            {
                _context.Requests.Remove(request);
                _context.SaveChanges();
            }
        }

        public Department? GetDepartmentById(int departmentId)
        {
            return _context.Departments.Include(d => d.HrManager)
                                       .FirstOrDefault(d => d.Id == departmentId);
        }
    }
}