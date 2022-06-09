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
    public class OrdersController : Controller
    {
        private readonly MyEshopDBContext _context;

        public OrdersController(MyEshopDBContext context)
        {
            _context = context;
        }

        // GET: Orders
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
/*        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Orders.ToListAsync());
        }*/

        public async Task<IActionResult> Index(int search)
        {
            return View(_context.Orders.Where(x => x.OrderID.Equals(search) || search == 0).ToList());
        }

        public IActionResult TrackMyOrder(int search)
        {
            return View(_context.Orders.Where(x => x.OrderID.Equals(search) || search == 0).ToList());

        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Orders orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderID == id);
            orders.OrderDetails = OrderDetails(id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public List<OrderDetails> OrderDetails(int? id)
        {
            
            if (id == null)
            {
                return new List<OrderDetails>();
            }
            List<OrderDetails> orderProducts =  _context.OrderDetails.Where(x => x.OrderId == id).ToList();
            foreach(OrderDetails product in orderProducts)
            {
                product.Products = _context.Products.FirstOrDefault(p => p.ProductID == product.ProductId);
            }
            return orderProducts;
        }
        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,OrderStatus,CustomerName,Email,Address,City,StateCode,PhoneNumber,Notes,OrderDate")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,OrderStatus,CustomerName,Email,Address,City,StateCode,PhoneNumber,Notes,OrderDate")] Orders orders)
        {
            if (id != orders.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderID))
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
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}
