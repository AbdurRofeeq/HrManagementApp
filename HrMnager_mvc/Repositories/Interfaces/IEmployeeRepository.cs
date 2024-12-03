using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;

namespace HrMnager_mvc.Repositories
{
    public interface IEmployeeRepository
    {
        Employee? FindByEmail(string email);
        ICollection<Employee> GetAllEmployees();
        void Delete(Employee employee);
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        IEnumerable<Employee> GetAllEmployeesByDepartment(int departmentId);
        Employee? GetById(int id);
    }
}