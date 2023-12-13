using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EducationApp.Models;
using EducationApp.Models.Data;
using EducationApp.ViewModels.ProductViewModels;
using EducationApp.ViewModels.CostViewModels;

namespace EducationApp.Controllers
{
    public class CostController : Controller
    {
        private readonly AppCtx _context;

        public CostController(AppCtx context)
        {
            _context = context;
        }

        // GET: Costs
        public async Task<IActionResult> Index()
        {
            var appCtx = _context.Costs
                .Include(s => s.Product)
                .OrderByDescending(o => o.NewPrice)
                .OrderBy(f => f.Product);

            return View(await appCtx.ToListAsync());
            /*
            return _context.Costs != null ? 
                          View(await _context.Costs.ToListAsync()) :
                          Problem("Entity set 'AppCtx.Costs'  is null.");
            */
        }

        // GET: Costs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Costs == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // GET: Costs/Create
        public IActionResult Create()
        {
            ViewData["IdProduct"] = new SelectList(_context.Products.OrderBy(o => o.NameProduct), "Id", "NameProduct");
            return View();
        }

        // POST: Costs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCostViewModels model)
        {
            /*if (_context.Costs
                 .Where(f => f.NewPrice == model.NewPrice
                 .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Продукт с таким именем уже существует");
            }*/

            if (ModelState.IsValid)
            {
                Cost cost = new()
                {
                    IdProduct = model.IdProduct,
                    NewPrice = model.NewPrice,
                    Date = DateTime.Now
                };

                /*Cost cost = new()
                {
                    NewPrice = model.Cost,
                    IdProduct = model.Id,
                    IdСategory = model.IdСategory
                };*/

                _context.Add(cost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdСategory"] = new SelectList(_context.Сategories.OrderBy(o => o.NameСategory), "Id", "NameCategory", model.IdProduct);
            return View(model);
        }

        // GET: Costs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Costs == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs.FindAsync(id);
            if (cost == null)
            {
                return NotFound();
            }
            return View(cost);
        }

        // POST: Costs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,NewPrice")] Cost cost)
        {
            if (id != cost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostExists(cost.Id))
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
            return View(cost);
        }

        // GET: Costs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Costs == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // POST: Costs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Costs == null)
            {
                return Problem("Entity set 'AppCtx.Costs'  is null.");
            }
            var cost = await _context.Costs.FindAsync(id);
            if (cost != null)
            {
                _context.Costs.Remove(cost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostExists(int id)
        {
          return (_context.Costs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
