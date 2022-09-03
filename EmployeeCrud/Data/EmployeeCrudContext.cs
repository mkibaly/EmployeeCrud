using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeCrud.Models;

namespace EmployeeCrud.Data
{
    public class EmployeeCrudContext : DbContext
    {
        public EmployeeCrudContext (DbContextOptions<EmployeeCrudContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeCrud.Models.Employee> Employee { get; set; } = default!;
    }
}
