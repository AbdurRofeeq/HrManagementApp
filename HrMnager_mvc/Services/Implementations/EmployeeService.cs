using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;
using HrMnager_mvc.Models.RequestModels;
using HrMnager_mvc.Models.ResponseModel;
using HrMnager_mvc.Repositories;
using HrMnager_mvc.Repositories.Interfaces;
using HrMnager_mvc.Services.Interfaces;

namespace HrMnager_mvc.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IHrManagerRepository _hrManagerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUserRepository userRepository, IDepartmentRepository departmentRepository, IHrManagerRepository hrManagerRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _hrManagerRepository = hrManagerRepository;
            _unitOfWork = unitOfWork;
        }

        public EmployeeResponse AddEmployee(CreateEmployeeRequest request, string hrManagerEmail)
        {
            var hrManager = _hrManagerRepository.FindByEmail(hrManagerEmail);
            if (hrManager == null)
            {
                throw new ArgumentException("Invalid HR Manager");
            }

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

            var newUser = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                Salary = request.Salary,
                PasswordHash = hashedPassword,
                Salt = salt,
                RoleId = 3,
                Department = hrManager.Department,
                DepartmentId = hrManager.DepartmentId
            };
         

            _userRepository.AddUser(newUser);

            var newEmployee = new Employee
            {
                User = newUser,
                DepartmentId = hrManager.DepartmentId,
                Role = "Employee",
                HrManagerId = hrManager.Id,
                UserId = newUser.Id,
                Department = newUser.Department,
            };

            _employeeRepository.AddEmployee(newEmployee);
            _unitOfWork.Save();

             newUser.EmployeeId = newEmployee.Id;
            _userRepository.UpdateUser(newUser);

            _unitOfWork.Save();
            return new EmployeeResponse
            {
                Id = newEmployee.Id,
                FullName = newUser.FullName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                Gender = newUser.Gender,
                Salary = newUser.Salary,
                RoleName = newEmployee.Role,
                DepartmentName = hrManager.Department.Name
            };
        }

        public EmployeeResponse UpdateEmployee(UpdateEmployeeRequest request, string hrManagerEmail)
        {
            var hrManager = _hrManagerRepository.FindByEmail(hrManagerEmail);
            var existingEmployee = _employeeRepository.GetById(request.Id);
            if (hrManager == null || existingEmployee == null || hrManager.DepartmentId != existingEmployee.DepartmentId)
            {
                throw new ArgumentException("Invalid HR Manager or Employee");
            }

            existingEmployee.User.FullName = request.FullName;
            existingEmployee.User.Email = request.Email;

            _userRepository.UpdateUser(existingEmployee.User);
            _employeeRepository.UpdateEmployee(existingEmployee);
            _unitOfWork.Save();

            return new EmployeeResponse
            {
                Id = existingEmployee.Id,
                FullName = existingEmployee.User.FullName,
                Email = existingEmployee.User.Email,
            };
        }

        public EmployeeDetailResponse GetEmployeeByEmail(int id, string hrManagerEmail)
        {
            var hrManager = _hrManagerRepository.FindByEmail(hrManagerEmail);
            var employee = _employeeRepository.GetById(id);
            if (hrManager == null || employee == null || hrManager.DepartmentId != employee.DepartmentId)
            {
                throw new ArgumentException("Invalid HR Manager or Employee");
            }

            return new EmployeeDetailResponse
            {
                Id = employee.Id,
                FullName = employee.User.FullName,
                Email = employee.User.Email,
                Salary = employee.User.Salary,
                PhoneNumber = employee.User.PhoneNumber,
                DepartmentName = employee.Department.Name
            };
        }

        public EmployeeListResponse GetAllEmployees(int hrManagerId)
        {
            var hrManager = _hrManagerRepository.GetById(hrManagerId);
            if (hrManager == null)
            {
                throw new ArgumentException("Invalid HR Manager");
            }

            var employees = _employeeRepository.GetAllEmployeesByDepartment(hrManager.DepartmentId);

            return new EmployeeListResponse
            {
                Employees = employees.Select(employee => new EmployeeResponse
                {
                    Id = employee.Id,
                    FullName = employee.User.FullName,
                    Email = employee.User.Email,
                }).ToList()
            };
        }

        public EmployeeListResponse GetAllEmployees(string hrManagerEmail)
        {
            var hrManager = _hrManagerRepository.FindByEmail(hrManagerEmail);
            if (hrManager == null)
            {
                throw new ArgumentException("Invalid HR Manager");
            }

            var employees = _employeeRepository.GetAllEmployeesByDepartment(hrManager.DepartmentId);

            return new EmployeeListResponse
            {
                Employees = employees.Select(employee => new EmployeeResponse
                {
                    Id = employee.Id,
                    FullName = employee.User.FullName,
                    Email = employee.User.Email,
                    DepartmentName = hrManager.Department.Name,
                    Gender = employee.User.Gender,
                    RoleName = "Employee",
                    PhoneNumber = employee.User.PhoneNumber,
                    Salary = employee.User.Salary
                }).ToList()
            };
        }

        public bool DeleteEmployee(int id, string hrManagerEmail)
        {
            var hrManager = _hrManagerRepository.FindByEmail(hrManagerEmail);
            var employee = _employeeRepository.GetById(id);
            if (hrManager == null || employee == null || hrManager.DepartmentId != employee.DepartmentId)
            {
                throw new ArgumentException("Invalid HR Manager or Employee");
            }
            
            _employeeRepository.Delete(employee);
            _userRepository.Delete(employee.User);
            _unitOfWork.Save();
            return true;
        }

        public EmployeeDetailResponse FindByEmail(string employeeEmail)
        {
            var employee = _employeeRepository.FindByEmail(employeeEmail);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found");
            }

            if (employee.User == null)
            {
                throw new ArgumentException("Employee user details not found");
            }

            return new EmployeeDetailResponse
            {
                Id = employee.Id,
                FullName = employee.User.FullName,
                Email = employee.User.Email,
                PhoneNumber = employee.User.PhoneNumber,
                Gender = employee.User.Gender,
                Salary = employee.User.Salary,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department.Name,
            };
        }
    }

}