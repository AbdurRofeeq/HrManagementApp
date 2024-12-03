using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMnager_mvc.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public int HrManagerId { get; set; }
        public HrManager HrManager { get; set; } = default!;
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = default!;
        public string Role { get; set; } = default!;
    }

}