using Microsoft.AspNetCore.Mvc;
using PERFILES_SA.Models;
using System.Collections.Generic;
using System.Linq;

namespace PERFILES_SA.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly PERFILES_SA.Data.DatabaseHelper _dbHelper = new PERFILES_SA.Data.DatabaseHelper();
        public IActionResult Index()
        {
            return View(_dbHelper.GetAllDepartments());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,IsActive")] Department department)
        {
            if (ModelState.IsValid)
            {
                _dbHelper.AddDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _dbHelper.GetAllDepartments().FirstOrDefault(d => d.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,IsActive")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbHelper.UpdateDepartment(department);
                }
                catch (System.Exception ex)
                {
                    // Log the error (consider using a proper logging framework)
                    System.Console.WriteLine($"Error updating department: {ex.Message}");
                    ModelState.AddModelError("", "No se pudieron guardar los cambios. Inténtalo de nuevo y, si el problema persiste, consulta al administrador del sistema.");
                    return View(department);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _dbHelper.GetAllDepartments().FirstOrDefault(d => d.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _dbHelper.DeleteDepartment(id);
            }
            catch (System.Exception ex)
            {
                // Log the error (consider using a proper logging framework)
                System.Console.WriteLine($"Error deleting department: {ex.Message}");
                // Optionally add a model error to display on the view
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
            return RedirectToAction(nameof(Index));
        }
    }
}