using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HrMnager_mvc.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<HrManagerContext>
    {
        public HrManagerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HrManagerContext>();
            var connectionString = "Server=localhost;User=root;Database=HrManagerMvcDb;Password=K34532";
            
            optionsBuilder.UseMySQL(connectionString);
            return new HrManagerContext(optionsBuilder.Options);
        }
    }
}