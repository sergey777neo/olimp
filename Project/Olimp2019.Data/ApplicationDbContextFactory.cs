using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Olimp2019.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Olimp2019.Data
{
    public class ApplicationDbContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {            
            
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Olimp2019;Trusted_Connection=True;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
