using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Context;
using HrMnager_mvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrMnager_mvc.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrManagerContext _context;
        public EmployeeRepository(HrManagerContext context)
        {
            _context = context;
        }
        public Employee AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            return employee;
        }

        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public Employee? FindByEmail(string email)
        {
            return _context.Employees
            .Include(e => e.User)
            .Include(e => e.Department)
            .Include(e => e.User.Role)
            .FirstOrDefault(e => e.User.Email == email);
        }

        public ICollection<Employee> GetAllEmployees()
        {
            return _context.Employees
           .Include(e => e.User)
           .Include(e => e.Department)
           .Include(e => e.User.Role)
           .ToList();
        }

        public Employee? GetById(int id)
        {
               return _context.Employees
                   .Include(h => h.User)
                   .SingleOrDefault(h => h.Id == id);
        }

        public Employee UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployeesByDepartment(int departmentId)
        {
            return _context.Employees
                           .Include(e => e.User)
                           .Where(e => e.DepartmentId == departmentId)
                           .ToList();
        }
    }
}