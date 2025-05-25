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
    }
}