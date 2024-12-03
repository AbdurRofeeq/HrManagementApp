using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Context;
using HrMnager_mvc.Entities;
using HrMnager_mvc.Models;
using HrMnager_mvc.Repositories.Interfaces;
using HrMnager_mvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HrMnager_mvc.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HrManagerContext _context;
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo, HrManagerContext context)
        {
            _userRepo = userRepo;
            _context = context;
        }


        public UserResponse Login(LoginModel model)
        {
            var user = _userRepo.FindByEmail(model.Email);
            if (user == null)
            {
                return new UserResponse
                {
                    Status = false,
                    Message = "Invalid Credentials"
                };
            }

            if (user.Role.Id == 0)
            {
                return new UserResponse
                {
                    Status = false,
                    Message = "User role is not assigned"
                };
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
            if (!isPasswordValid) return new UserResponse
            {
                Status = false,
                Message = "Invalid Credentials"
            };

            var response = new UserResponse
            {
                Status = true,
                Message = "Login Successfully",
                Email = user.Email,
                Role = user.Role.Name,
                EmployeeId = user.EmployeeId,
                HrManagerId = user.HrManagerId
            };

            var result = response;
            return result;

        }



    }
}


