using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeCrud.Data;
using EmployeeCrud.Models;
using EmployeeCrud.Dtos;

namespace EmployeeCrud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : Controller
    {
        private readonly EmployeeCrudContext _context;

        public EmployeesController(EmployeeCrudContext context)
        {
            _context = context;
        }

        // GET: Employees
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Index()
        {
            return await _context.Employee.ToListAsync();
        }

        // GET: Employees/5
        [HttpGet("id")]
        public async Task<ActionResult<Employee>> Details(Guid? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // POST: Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult<Employee>> Create(EmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var emp = new Employee
            {
                Id = Guid.NewGuid(),
                Name = employee.Name,
                JoinDate = employee.JoinDate,
                Salary = employee.Salary
            };
            _context.Add(emp);
            await _context.SaveChangesAsync();

            return emp;
        }

        // PUT: Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("id")]
        public async Task<IActionResult> Edit(Guid id, EmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dbEmployee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dbEmployee == null)
            {
                return NotFound();
            }

            dbEmployee.Salary = employee.Salary;
            dbEmployee.Name = employee.Name;
            dbEmployee.JoinDate = employee.JoinDate;

            _context.Update(dbEmployee);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // delete: Employees/5
        [HttpDelete("id")]
        public async Task Delete(Guid id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
        }

    }
}
