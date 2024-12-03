using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Context;
using HrMnager_mvc.Entities;
using HrMnager_mvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HrMnager_mvc.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly HrManagerContext _context;
        public UserRepository(HrManagerContext context)
        {
            _context = context;
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            return user;
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public User? FindByEmail(string email)
        {
            var user = _context.Users
               .Include(u => u.Role)
               .Include(u => u.HrManager)
               .Include(u => u.Employee)
               .FirstOrDefault(u => u.Email == email);
            return user;
        }
        public ICollection<User> GetAllUsers()
        {
            return _context.Users
              .Include(u => u.Role)
              .Include(u => u.HrManager)
              .Include(u => u.Employee)
              .ToList();
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            return user;
        }
    }
}
