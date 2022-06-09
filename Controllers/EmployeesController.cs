#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyEshop.Areas.Core;
using MyEshop.Areas.Identity.Data;
using MyEshop.Models;

namespace MyEshop.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MyEshopDBContext _context;

        public EmployeesController(MyEshopDBContext context)
        {
            _context = context;
        }

        // GET: Employees
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public IActionResult Index(string search)
        {
            return View(_context.Employee.Where(x => x.EmployeeName.Contains(search) || search == null).ToList());
            /*return View( _context.Products.ToList());*/
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeImage,EmployeeName,BirthDate,HiringDate")] Employee employee)
        {
            UploadImage(employee);
            if (ModelState.IsValid)
            {
               
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        private void UploadImage(Employee employee)
        {
            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string EmployeeImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "Images/", EmployeeImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                employee.EmployeeImage = EmployeeImageName;
            }
            else if (employee.EmployeeImage == null)
            {
                employee.EmployeeImage = "user_profile_icon_free_vector.jpg";
            }
            else
            {
                employee.EmployeeImage = employee.EmployeeImage;
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
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeImage,EmployeeName,BirthDate,HiringDate")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }
            UploadImage(employee);
            if (ModelState.IsValid)
            {
                try
                {
                   
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
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
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
