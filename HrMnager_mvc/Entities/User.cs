using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Enums;

namespace HrMnager_mvc.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public Gender Gender { get; set; } = default!;
        public decimal Salary { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Salt { get; set; } = default!;
        public int? RoleId { get; set; } 
        public Role? Role { get; set; } 
        public int? HrManagerId { get; set; }
        public HrManager? HrManager { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; } 
    }
}