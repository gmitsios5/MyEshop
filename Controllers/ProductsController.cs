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
    public class ProductsController : Controller
    {
        private readonly MyEshopDBContext _context;

        public ProductsController(MyEshopDBContext context)
        {
            _context = context;
        }
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        // GET: Products
        public IActionResult Index(string search)
        {
            return View(_context.Products.Where(x => x.ProductTitle.Contains(search) || search == null).ToList());
            /*return View( _context.Products.ToList());*/
        }


        public IActionResult ProductsforCustomers()
        {
            return View(_context.Products.Where(x => x.Quantity>=1).ToList());
            /*return View( _context.Products.ToList());*/
        }

        public IActionResult Search(string search)
        {
            return View( _context.Products.Where(x => x.ProductTitle.Contains(search) || search == null).ToList());
        }
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        [Authorize(Policy = Constants.Policies.RequireAdmin)]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Constants.Policies.RequireAdmin)]

        public async Task<IActionResult> Create([Bind("ProductID,ProductImage,ProductTitle,Description,Value,Quantity")] Products products)
        {
            if (ModelState.IsValid)
            {
                UploadImage(products);
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        private void UploadImage(Products products)
        {
            var file = HttpContext.Request.Form.Files;
            
            if (file.Count() > 0)
            {
                string ProductImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "Images/", ProductImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                products.ProductImage = ProductImageName;
            }
            else if (products.ProductImage == null)
            {
                products.ProductImage = "Default_Product_Image.png";
            }
            else
            {
                products.ProductImage = products.ProductImage;
            }
        }

        // GET: Products/Edit/5
        [Authorize(Policy = Constants.Policies.RequireAdmin)]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            
            
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductImage,ProductTitle,Description,Value,Quantity")] Products products)
        {
            if (id != products.ProductID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    UploadImage(products);
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductID))
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
            return View(products);
        }

        // GET: Products/Delete/5
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
