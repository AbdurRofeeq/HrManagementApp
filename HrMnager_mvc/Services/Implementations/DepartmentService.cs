using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;
using HrMnager_mvc.Models.RequestModels;
using HrMnager_mvc.Models.ResponseModel;
using HrMnager_mvc.Repositories.Interfaces;
using HrMnager_mvc.Services.Interfaces;

namespace HrMnager_mvc.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IHrManagerRepository _hrManagerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IDepartmentRepository departmentRepository, IHrManagerRepository hrManagerRepository, IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _hrManagerRepository = hrManagerRepository;
            _unitOfWork = unitOfWork;
        }

        public DepartmentResponse AddDepartment(CreateDepartmentRequest request)
        {
            var department = new Department
            {
                Name = request.Name,
            };

            _departmentRepository.AddDepartment(department);
            _unitOfWork.Save();

            return new DepartmentResponse
            {
                Id = department.Id,
                Name = department.Name,
            };
        }

        public DepartmentResponse UpdateDepartment(UpdateDepartmentRequest request)
        {
            var department = _departmentRepository.GetDepartmentById(request.Id);
            if (department == null)
            {
                throw new ArgumentException("Department not found");
            }

            department.Name = request.Name;

            _departmentRepository.UpdateDepartment(department);
            _unitOfWork.Save();

            return new DepartmentResponse
            {
                Id = department.Id,
                Name = department.Name,
            };
        }

        public DepartmentDetailResponse? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetDepartmentById(id);

            if (department == null)
            {
                throw new ArgumentException("Department not found");
            }

            if (department.HrManager == null)
            {
                return new DepartmentDetailResponse
                {
                    Id = department.Id,
                    Name = department.Name
                };
            }

            var hrManager = _hrManagerRepository.GetById(department.HrManager.DepartmentId);

            if (hrManager == null)
            {
                return new DepartmentDetailResponse
                {
                    Id = department.Id,
                    Name = department.Name
                };
            }

            var hrManagerResponse = hrManager.User == null ? null : new HrManagerResponse
            {
                Id = hrManager.Id,
                FullName = hrManager.User.FullName,
                Email = hrManager.User.Email,
                PhoneNumber = hrManager.User.PhoneNumber,
                Gender = hrManager.User.Gender,
                Salary = hrManager.User.Salary,
                DepartmentId = hrManager.DepartmentId
            };

            return new DepartmentDetailResponse
            {
                Id = department.Id,
                Name = department.Name,
                HrManager = hrManagerResponse,
                Employees = department.Employees.Select(e => new EmployeeResponse
                {
                    Id = e.Id,
                    FullName = e.User.FullName,
                    Email = e.User.Email
                }).ToList()
            };
        }

        public DepartmentListResponse GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllDepartments();

            return new DepartmentListResponse
            {
                Departments = departments.Select(d => new DepartmentResponse
                {
                    Id = d.Id,
                    Name = d.Name,
                }).ToList()
            };
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetDepartmentById(id);
            if (department == null)
            {
                throw new ArgumentException("Department not found");
            }

            _departmentRepository.Delete(department);
            _unitOfWork.Save();
            return true;
        }
    }


}
