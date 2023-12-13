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
    public class СategoryController : Controller
    {
        private readonly AppCtx _context;

        public СategoryController(AppCtx context)
        {
            _context = context;
        }

        // GET: Сategories
        public async Task<IActionResult> Index()
        {
            return _context.Сategories != null ? 
                          View(await _context.Сategories.ToListAsync()) :
                          Problem("Entity set 'AppCtx.Сategories'  is null.");
        }

        // GET: Сategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Сategories == null)
            {
                return NotFound();
            }

            var сategory = await _context.Сategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (сategory == null)
            {
                return NotFound();
            }

            return View(сategory);
        }

        // GET: Сategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Сategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameСategory")] Сategory сategory)
        {
           /* if (ModelState.IsValid)
            {
               
            }*/
            _context.Add(сategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            /*return View(сategory);*/
        }

        // GET: Сategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Сategories == null)
            {
                return NotFound();
            }

            var сategory = await _context.Сategories.FindAsync(id);
            if (сategory == null)
            {
                return NotFound();
            }
            return View(сategory);
        }

        // POST: Сategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameСategory")] Сategory сategory)
        {
            if (id != сategory.Id)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {*/
                try
                {
                    _context.Update(сategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!СategoryExists(сategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            /*}*/
            return View(сategory);
        }

        // GET: Сategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Сategories == null)
            {
                return NotFound();
            }

            var сategory = await _context.Сategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (сategory == null)
            {
                return NotFound();
            }

            return View(сategory);
        }

        // POST: Сategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Сategories == null)
            {
                return Problem("Entity set 'AppCtx.Сategories'  is null.");
            }
            var сategory = await _context.Сategories.FindAsync(id);
            if (сategory != null)
            {
                _context.Сategories.Remove(сategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool СategoryExists(int id)
        {
          return (_context.Сategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
