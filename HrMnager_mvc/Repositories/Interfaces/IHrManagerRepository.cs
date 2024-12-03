using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Entities;

namespace HrMnager_mvc.Repositories.Interfaces
{
    public interface IHrManagerRepository
    {
        HrManager? FindByEmail(string email);
        ICollection<HrManager> GetAllHrManagers();
        void Delete(HrManager HrManager);
        HrManager AddHrManager(HrManager hrManager);
        HrManager UpdateHrManager(HrManager HrManager);
        HrManager? GetById(int id);
       
    }
}