using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMnager_mvc.Models.ResponseModel
{
    public class DepartmentResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? HrManagerId { get; set; }
        public int? EmployeeId { get; set; }
    }

    public class DepartmentDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public HrManagerResponse? HrManager { get; set; }
        public List<EmployeeResponse>? Employees { get; set; } = new List<EmployeeResponse>();
    }

    public class DepartmentListResponse
    {
        public List<DepartmentResponse> Departments { get; set; } = new List<DepartmentResponse>();
    }


}