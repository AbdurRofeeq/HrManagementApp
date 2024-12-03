using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Enums;

namespace HrMnager_mvc.Models.ResponseModel
{
    public class EmployeeResponse : BaseResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public Gender? Gender { get; set; }
        public decimal? Salary { get; set; }
        public string DepartmentName { get; set; } = default!;
        public string? RoleName {get; set;}
    }

    public class EmployeeDetailResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public Gender Gender { get; set; }
        public decimal Salary { get; set; }
        public DepartmentResponse Department { get; set; } = default!;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = default!;
    }

    public class EmployeeListResponse
    {
        public List<EmployeeResponse> Employees { get; set; } = new List<EmployeeResponse>();
    }
}