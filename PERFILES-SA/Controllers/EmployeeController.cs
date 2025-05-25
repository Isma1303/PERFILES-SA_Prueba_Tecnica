using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PERFILES_SA.Models;
using PERFILES_SA.Data;
namespace PERFILES_SA.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        public IActionResult Index(DateTime? startDate, DateTime? endDate, int? departmentId)
        {
            var employees = _dbHelper.GetAllEmployees();
            var query = employees.AsQueryable();

            if (departmentId.HasValue)
            {
                query = query.Where(e => e.DepartmentId == departmentId.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(e => e.HireDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(e => e.HireDate <= endDate.Value);
            }

            ViewBag.Departments = new SelectList(_dbHelper.GetAllDepartments(), "Id", "Name");
            return View(query.ToList());
        }

        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_dbHelper.GetAllDepartments().Where(d => d.IsActive), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Names,DPI,DateOfBirth,Gender,HireDate,DepartmentId,IsActive")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbHelper.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_dbHelper.GetAllDepartments().Where(d => d.IsActive), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _dbHelper.GetAllEmployees().FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_dbHelper.GetAllDepartments().Where(d => d.IsActive), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Names,DPI,DateOfBirth,Gender,HireDate,DepartmentId,IsActive")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _dbHelper.UpdateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_dbHelper.GetAllDepartments().Where(d => d.IsActive), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int id)
        {
            var employee = _dbHelper.GetAllEmployees().FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _dbHelper.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}