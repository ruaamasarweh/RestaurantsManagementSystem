using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gp.Data;
using Gp.Models;
namespace Gp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly SystemDbContext _context;

        public EmployeesController(SystemDbContext context)
        {
            _context = context;
        }
        

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            int branchID = (int)HttpContext.Session.GetInt32("BranchID")!;
            List<Employee> employees = await _context.Employee
                                                     .Where(u => u.BranchID == branchID)
                                                     .Include(u => u.EmployeeType)
                                                     .ToListAsync();
            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Branch)
                .Include(e => e.EmployeeType)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {

            ViewBag.EmployeeType = new SelectList(_context.EmployeeType, "EmployeeTypeID", "EmpType");
            ViewData["BranchID"] = new SelectList(_context.Branch, "BranchID", "BranchName");

            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {

            try
            {
                int branchID = (int)HttpContext.Session.GetInt32("BranchID")!;
                employee.BranchID = branchID;
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.EmployeeType = new SelectList(_context.EmployeeType, "EmployeeTypeID", "EmpType");
                return View(employee);
            }
        }



        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }


            ViewBag.EmployeeType = new SelectList(_context.EmployeeType, "EmployeeTypeID", "EmpType");
            ViewData["BranchID"] = new SelectList(_context.Branch, "BranchID", "BranchName");

            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound();
            }

            try
            {

                var existingEmployee = await _context.Employee.FindAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }


                int branchID = (int)HttpContext.Session.GetInt32("BranchID")!;
                employee.BranchID = branchID;


                existingEmployee.FullName = employee.FullName;
                existingEmployee.UserName = employee.UserName;
                existingEmployee.PhoneNum = employee.PhoneNum;
                existingEmployee.Address = employee.Address;
                existingEmployee.Email = employee.Email;
                existingEmployee.Password = employee.Password;
                existingEmployee.EmployeeTypeID = employee.EmployeeTypeID;
                existingEmployee.BranchID = branchID;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.EmployeeID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Branch)
                .Include(e => e.EmployeeType)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeID == id);
        }
        public IActionResult SelectEmployee(int branchID, string selectedController, string selectedAction)
        {
            HttpContext.Session.SetInt32("BranchID", branchID);
            return RedirectToAction(selectedAction, selectedController);
        }

    }

}

