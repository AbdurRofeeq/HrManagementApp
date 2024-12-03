using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMnager_mvc.Models.RequestModels
{
    public class CreateDepartmentRequest
    {
        public string Name { get; set; } = default!;
        public int HrManagerId { get; set; }
    }

    public class UpdateDepartmentRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int HrManagerId { get; set; }
    }
}