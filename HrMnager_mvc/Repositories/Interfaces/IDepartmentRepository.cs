using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;

namespace HrMnager_mvc.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        ICollection<Department> GetAllDepartments();
        public Department? GetDepartmentWithDetails(int departmentId);
        void Delete(Department department);
        Department AddDepartment(Department department);
        Department UpdateDepartment(Department department);
        public Department? GetDepartmentByName(string name);
        public Department? GetDepartmentById(int id);
    }
}