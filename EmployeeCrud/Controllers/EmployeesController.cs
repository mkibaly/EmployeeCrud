using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeCrud.Data;
using EmployeeCrud.Models;

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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Employee>> Create([Bind("Name,JoinDate,Salary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid();
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return employee;
        }

        // PUT: Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("id")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,JoinDate,Salary,Id")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            var dbEmployee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbEmployee == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Update(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // delete: Employees/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
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
