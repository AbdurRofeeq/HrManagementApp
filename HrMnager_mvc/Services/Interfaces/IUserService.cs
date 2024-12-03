using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;
using HrMnager_mvc.Models;

namespace HrMnager_mvc.Services.Interfaces
{
    public interface IUserService
    {
        UserResponse Login (LoginModel model);
    }
}