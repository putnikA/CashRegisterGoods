using CashRegisterGoods.AllGoods;
using CashRegisterGoods.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<IActionResult> Create([Bind("PLU,Name,Quantity,NetPrice,SellingPricePerUnit,Barcode")] Goods goods)
        {
            Console.WriteLine($"Quantity value on form submission: {goods.Quantity}");

            if (ModelState.IsValid)
            {
                _context.Add(goods);
                Console.WriteLine($"Entity state before SaveChangesAsync: {_context.Entry(goods).State}");
                await _context.SaveChangesAsync();
                Console.WriteLine($"Entity state after SaveChangesAsync: {_context.Entry(goods).State}");

            }
            return View(goods);
        }
        /*public async Task<IActionResult> Create([Bind("PLU,Name,Quantity,NetPrice,SellingPricePerUnit,Barcode")] Goods goods)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(goods);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             return View(goods);
         }*/



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
            return View(goods);
        }

        // POST: Goods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PLU,Name,Quantity,NetPrice,SellingPricePerUnit,Barcode")] Goods goods)
        {
            if (id != goods.PLU)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
