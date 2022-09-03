using Microsoft.EntityFrameworkCore;
using EmployeeCrud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeeCrud.Data
{
    public class EmployeeCrudContext : IdentityDbContext<IdentityUser>
    {
        public EmployeeCrudContext (DbContextOptions<EmployeeCrudContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>()
                .Property(a => a.Salary).HasColumnType("decimal(18,2)");
        }
    }
}
