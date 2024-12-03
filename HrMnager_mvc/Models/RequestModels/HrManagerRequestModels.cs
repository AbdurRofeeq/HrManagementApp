using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Enums;
using HrMnager_mvc.Models.ResponseModel;

namespace HrMnager_mvc.Models.RequestModels
{
    public class CreateHrManagerRequest
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; } 
        public Gender Gender { get; set; }
        public decimal Salary { get; set; }
        public string Password { get; set; } = default!;
        public int RoleId { get; set; }
        public string DepartmentName { get; set; }
    }
    public class UpdateHrManagerRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public Gender Gender { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
    }



}