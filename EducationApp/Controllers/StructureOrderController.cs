using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EducationApp.Models;
using EducationApp.Models.Data;

namespace EducationApp.Controllers
{
    public class StructureOrderController : Controller
    {
        private readonly AppCtx _context;

        public StructureOrderController(AppCtx context)
        {
            _context = context;
        }

        // GET: StructuriesOrders
        public async Task<IActionResult> Index()
        {
              return _context.StructureOrders != null ? 
                          View(await _context.StructureOrders.ToListAsync()) :
                          Problem("Entity set 'AppCtx.StructureOrders'  is null.");
        }

        // GET: StructuriesOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StructureOrders == null)
            {
                return NotFound();
            }

            var structureOrder = await _context.StructureOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (structureOrder == null)
            {
                return NotFound();
            }

            return View(structureOrder);
        }

        // GET: StructuriesOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StructuriesOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cost")] StructureOrder structureOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(structureOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(structureOrder);
        }

        // GET: StructuriesOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StructureOrders == null)
            {
                return NotFound();
            }

            var structureOrder = await _context.StructureOrders.FindAsync(id);
            if (structureOrder == null)
            {
                return NotFound();
            }
            return View(structureOrder);
        }

        // POST: StructuriesOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cost")] StructureOrder structureOrder)
        {
            if (id != structureOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(structureOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StructureOrderExists(structureOrder.Id))
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
            return View(structureOrder);
        }

        // GET: StructuriesOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StructureOrders == null)
            {
                return NotFound();
            }

            var structureOrder = await _context.StructureOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (structureOrder == null)
            {
                return NotFound();
            }

            return View(structureOrder);
        }

        // POST: StructuriesOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StructureOrders == null)
            {
                return Problem("Entity set 'AppCtx.StructureOrders'  is null.");
            }
            var structureOrder = await _context.StructureOrders.FindAsync(id);
            if (structureOrder != null)
            {
                _context.StructureOrders.Remove(structureOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StructureOrderExists(int id)
        {
          return (_context.StructureOrders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
