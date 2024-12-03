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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HrManagerContext _context;
        public DepartmentRepository(HrManagerContext context)
        {
            _context = context;
        }
        public Department AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            return department;
        }

        public void Delete(Department department)
        {
            _context.Departments.Remove(department);
        }
        
        public Department? GetDepartmentWithDetails(int departmentId)
        {
            return _context.Departments
                .Include(d => d.HrManager)
                .Include(d => d.Employees)
                .FirstOrDefault(d => d.Id == departmentId);
        }

        public ICollection<Department> GetAllDepartments()
        {
            return _context.Departments
            .Include(d => d.HrManager)       
            .Include(d => d.Employees)     
            .ToList();
        }

        public Department UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            return department;
        }

        public Department? GetDepartmentByName(string name)
        {
            return _context.Departments
                .Include(d => d.HrManager)
                .Include(d => d.Employees)
                .FirstOrDefault(d => d.Name == name);
        }

        public Department? GetDepartmentById(int id)
        {
            return _context.Departments
                .Include(d => d.HrManager)
                .Include(d => d.Employees)
                .FirstOrDefault(d => d.Id == id);
        }
    }
}