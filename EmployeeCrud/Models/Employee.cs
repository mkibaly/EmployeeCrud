using System;

namespace EmployeeCrud.Models
{
    public class Employee: Entity
    {
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal Salary { get; set; }
    }
}
