using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Enums;
using HrMnager_mvc.Entities;

namespace HrMnager_mvc.Models.ResponseModel
{
    public class HrManagerResponse : BaseResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public Gender Gender { get; set; }
        public decimal Salary { get; set; }
        public string? DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string RoleName { get; set; } = default!;
    }

    public class HrManagerDetailResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public Gender Gender { get; set; }
        public decimal Salary { get; set; }
        public DepartmentResponse Department { get; set; } = default!;
        public int DepartmentId { get; set; }
        public List<EmployeeResponse>? Employees { get; set; } = new List<EmployeeResponse>();
    }

    public class HrManagerListResponse
    {
        public List<HrManagerResponse> HrManagers { get; set; } = new List<HrManagerResponse>();
    }
}