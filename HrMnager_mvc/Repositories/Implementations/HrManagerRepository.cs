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
    public class HrManagerRepository : IHrManagerRepository
    {
        private readonly HrManagerContext _context;
        public HrManagerRepository(HrManagerContext context)
        {
            _context = context;
        }
        public HrManager AddHrManager(HrManager hrManager)
        {
            _context.HrManagers.Add(hrManager);
            return hrManager;
        }

        public void Delete(HrManager hrManager)
        {
            _context.HrManagers.Remove(hrManager);
        }

        public HrManager? FindByEmail(string email)
        {
            return _context.HrManagers
           .Include(e => e.User)
           .Include(e => e.Department)
           .Include(e => e.User.Role)
           .FirstOrDefault(e => e.User.Email == email);
        }

        public ICollection<HrManager> GetAllHrManagers()
        {
            return _context.HrManagers
           .Include(e => e.User)
           .Include(e => e.Department)
           .Include(e => e.User.Role)
           .ToList();
        }

        public HrManager? GetById(int id)
        {
             return _context.HrManagers
                   .Include(h => h.User)
                   .SingleOrDefault(h => h.Id == id);
        }

        public HrManager UpdateHrManager(HrManager hrManager)
        {
            _context.HrManagers.Update(hrManager);
            return hrManager;
        }
    }
}