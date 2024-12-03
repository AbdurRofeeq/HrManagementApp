using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrMnager_mvc.Context;
using HrMnager_mvc.Services.Interfaces;

namespace HrMnager_mvc.Services.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HrManagerContext _context;
        public UnitOfWork(HrManagerContext context)
        {
            _context = context;                     
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}