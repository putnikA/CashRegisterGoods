using CashRegisterGoods.Models;
using CashRegisterGoods.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashRegisterGoods.Controllers
{
    public class GoodsController : Controller
    {
        private readonly AppDbContext _context;

        public GoodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Goods
        public async Task<IActionResult> Index()
        {
            return View(await _context.Goods.ToListAsync());
        }

        // GET: Goods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goods = await _context.Goods
                .FirstOrDefaultAsync(m => m.PLU == id);
            if (goods == null)
            {
                return NotFound();
            }

            return View(goods);
        }

        // GET: Goods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Goods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PLU,Name,Quantity,NetPrice,PDV,Margin,SellingPricePerUnit,Barcode")] Goods goods)
        {
            if (ModelState.IsValid)
            {
                // Check if a record with the same PLU value already exists in the database
                if (_context.Goods.Any(g => g.PLU == goods.PLU))
                {
                    ModelState.AddModelError("PLU", "A record with the same PLU already exists.");
                    return View(goods);
                }

                // Check if a record with the same Barcode value already exists in the database
                if (_context.Goods.Any(g => g.Barcode == goods.Barcode))
                {
                    ModelState.AddModelError("Barcode", "A record with the same Barcode already exists.");
                    return View(goods);
                }

                _context.Add(goods);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goods);
        }



        // GET: Goods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goods = await _context.Goods.FindAsync(id);
            if (goods == null)
            {
                return NotFound();
            }

            // Set the options for the PDV select element
            var pdvOptions = new List<SelectListItem>
            {
               new SelectListItem { Text = "10%", Value = "10%" },
               new SelectListItem { Text = "20%", Value = "20%" }
            };
            ViewBag.PDVOptions = pdvOptions;

            return View(goods);
        }


    // POST: Goods/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PLU,Name,Quantity,NetPrice,PDV,Margin,SellingPricePerUnit,Barcode")] Goods goods)
        {
            if (id != goods.PLU)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check if the PLU value has changed
                var existingGoods = await _context.Goods.FindAsync(id);
                if (existingGoods.PLU != goods.PLU)
                {
                    // Check if a record with the same PLU value already exists in the database
                    if (_context.Goods.Any(g => g.PLU == goods.PLU))
                    {
                        ModelState.AddModelError("PLU", "A record with the same PLU already exists.");
                        return View(goods);
                    }
                }

                // Check if a record with the same Barcode value already exists in the database
                if (_context.Goods.Any(g => g.Barcode == goods.Barcode && g.PLU != id))
                {
                    ModelState.AddModelError("Barcode", "A record with the same Barcode already exists.");
                    return View(goods);
                }

                try
                {
                    _context.Update(goods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodsExists(goods.PLU))
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

            // If the model state is not valid, repopulate the PDV options
            var pdvOptions = new List<SelectListItem>
            {
               new SelectListItem { Text = "10%", Value = "10%" },
               new SelectListItem { Text = "20%", Value = "20%" }
             };
                 ViewBag.PDVOptions = pdvOptions;

            return View(goods);
        }

        // GET: Goods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goods = await _context.Goods
                .FirstOrDefaultAsync(m => m.PLU == id);
            if (goods == null)
            {
                return NotFound();
            }

            return View(goods);
        }

        // POST: Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goods = await _context.Goods.FindAsync(id);
            _context.Goods.Remove(goods);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodsExists(int id)
        {
            return _context.Goods.Any(e => e.PLU == id);
        }
    }
}
