using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Models.RequestModels;
using HrMnager_mvc.Models.ResponseModel;

namespace HrMnager_mvc.Services.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeResponse AddEmployee(CreateEmployeeRequest request, string hrManagerEmail);
        EmployeeResponse UpdateEmployee(UpdateEmployeeRequest request, string hrManagerEmail);
        EmployeeDetailResponse GetEmployeeByEmail(int id, string hrManagerEmail);
        EmployeeDetailResponse FindByEmail(string employeeEmail);
        EmployeeListResponse GetAllEmployees(int hrManagerId);
        EmployeeListResponse GetAllEmployees(string hrManagerEmail);
        bool DeleteEmployee(int id, string hrManagerEmail);
        
    } 
}