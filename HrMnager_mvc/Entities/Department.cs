using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMnager_mvc.Entities
{

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public HrManager HrManager { get; set; } = default!;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}