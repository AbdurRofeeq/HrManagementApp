using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMnager_mvc.Services.Interfaces
{
    public interface IUnitOfWork
    {
        int Save();
    }
}