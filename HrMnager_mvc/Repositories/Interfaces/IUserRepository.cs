using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;

namespace HrMnager_mvc.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? FindByEmail(string email);
        ICollection<User> GetAllUsers();
        void Delete(User user);
        User AddUser(User user);
        User UpdateUser(User user);
    }
}