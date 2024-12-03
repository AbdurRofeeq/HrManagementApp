using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;
using HrMnager_mvc.Models;
using HrMnager_mvc.Models.RequestModels;
using HrMnager_mvc.Models.ResponseModel;
using HrMnager_mvc.Repositories.Interfaces;
using HrMnager_mvc.Services.Interfaces;
using HrManagerDetailResponse = HrMnager_mvc.Models.ResponseModel.HrManagerDetailResponse;

namespace HrMnager_mvc.Services.Implementations
{
    public class HrManagerService : IHrManagerService
    {
        private readonly IHrManagerRepository _hrManagerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HrManagerService(IHrManagerRepository hrManagerRepository, IUserRepository userRepository, IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
        {
            _hrManagerRepository = hrManagerRepository;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public HrManagerResponse AddHrManager(CreateHrManagerRequest request, int departmentId)
        {
            var department = _departmentRepository.GetDepartmentByName(request.DepartmentName);
            if (department == null)
            {
                throw new ArgumentException("Invalid Department");
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
                RoleId = 2,
                Department = department,
                DepartmentId = department.Id
            };

            _userRepository.AddUser(newUser);


            var hrManager = new HrManager
            {
                UserId = newUser.Id, 
                User = newUser,
                Department = department
            };

            _hrManagerRepository.AddHrManager(hrManager);
            _unitOfWork.Save();

            newUser.HrManagerId = hrManager.Id;
            _userRepository.UpdateUser(newUser);

            _unitOfWork.Save();

            return new HrManagerResponse
            {
                Id = hrManager.Id,
                FullName = newUser.FullName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                Salary = newUser.Salary,
                DepartmentName = hrManager.Department.Name
            };
        }

        public HrManagerResponse UpdateHrManager(UpdateHrManagerRequest request, int departmentId)
        {
            var hrManager = _hrManagerRepository.GetById(request.Id);
            if (hrManager == null)
            {
                throw new ArgumentException("HR Manager not found");
            }

            hrManager.User.FullName = request.FullName;
            hrManager.User.Email = request.Email;
            hrManager.User.PhoneNumber = request.PhoneNumber;
            hrManager.User.Gender = request.Gender;
            hrManager.User.Salary = request.Salary;
            hrManager.DepartmentId = request.DepartmentId;

            _userRepository.UpdateUser(hrManager.User);
            _hrManagerRepository.UpdateHrManager(hrManager);
            _unitOfWork.Save();

            return new HrManagerResponse
            {
                Id = hrManager.Id,
                FullName = hrManager.User.FullName,
                Email = hrManager.User.Email,
                PhoneNumber = hrManager.User.PhoneNumber,
                Gender = hrManager.User.Gender,
                Salary = hrManager.User.Salary,
                DepartmentName = hrManager.Department.Name
            };
        }

        public HrManagerDetailResponse GetHrManagerById(int id)
        {
            var hrManager = _hrManagerRepository.GetById(id);
            if (hrManager == null)
            {
                throw new ArgumentException("HR Manager not found");
            }

            if (hrManager.User == null)
            {
                throw new ArgumentException("HR Manager user details not found");
            }

            if (hrManager.Department == null)
            {
                return new HrManagerDetailResponse
                {
                    Id = hrManager.Id,
                    FullName = hrManager.User.FullName,
                    Email = hrManager.User.Email,
                };
            }

            return new HrManagerDetailResponse
            {
                Id = hrManager.Id,
                FullName = hrManager.User.FullName,
                Email = hrManager.User.Email,
                PhoneNumber = hrManager.User.PhoneNumber,
                Gender = hrManager.User.Gender,
                Salary = hrManager.User.Salary,
                DepartmentId = hrManager.DepartmentId
            };
        }



        public HrManagerListResponse GetAllHrManagers()
        {
            var hrManagers = _hrManagerRepository.GetAllHrManagers();

            return new HrManagerListResponse
            {
                HrManagers = hrManagers.Select(h => new HrManagerResponse
                {
                    Id = h.Id,
                    FullName = h.User.FullName,
                    Email = h.User.Email,
                    PhoneNumber = h.User.PhoneNumber,
                    Gender = h.User.Gender,
                    Salary = h.User.Salary,
                    DepartmentName = h.Department.Name
                }).ToList()
            };
        }

        public BaseResponse Delete(int id)
        {
            var hrmanager = _hrManagerRepository.GetById(id);
            if (hrmanager == null)
            {
                return new BaseResponse
                {
                    Message = $"HrManager Doesn't Exist!!!",
                    Status = false
                };
            }
            _hrManagerRepository.Delete(hrmanager);
            _userRepository.Delete(hrmanager.User);
            _unitOfWork.Save();
            return new BaseResponse
            {
                Message = $"HrManager Deleted Successfully!!!",
                Status = true
            };
        }

        public HrManagerDetailResponse GetHrManagerByEmail(string email)
        {
            var hrManager = _hrManagerRepository.FindByEmail(email);
            if (hrManager == null)
            {
                throw new ArgumentException("HR Manager not found");
            }

            if (hrManager.User == null)
            {
                throw new ArgumentException("HR Manager user details not found");
            }

            if (hrManager.Department == null)
            {
                return new HrManagerDetailResponse
                {
                    Id = hrManager.Id,
                    FullName = hrManager.User.FullName,
                    Email = hrManager.User.Email,
                };
            }

            return new HrManagerDetailResponse
            {
                Id = hrManager.Id,
                FullName = hrManager.User.FullName,
                Email = hrManager.User.Email,
                PhoneNumber = hrManager.User.PhoneNumber,
                Gender = hrManager.User.Gender,
                Salary = hrManager.User.Salary,
                DepartmentId = hrManager.DepartmentId
            };
        }
    }


}