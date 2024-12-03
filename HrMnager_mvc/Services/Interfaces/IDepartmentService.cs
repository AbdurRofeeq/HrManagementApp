using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Models.RequestModels;
using HrMnager_mvc.Models.ResponseModel;

namespace HrMnager_mvc.Services.Interfaces
{
    public interface IDepartmentService
    {
        DepartmentResponse AddDepartment(CreateDepartmentRequest request);
        DepartmentResponse UpdateDepartment(UpdateDepartmentRequest request);
        DepartmentDetailResponse? GetDepartmentById(int id);
        DepartmentListResponse GetAllDepartments();
        bool DeleteDepartment(int id);
    }
}