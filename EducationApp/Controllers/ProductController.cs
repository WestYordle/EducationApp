using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EducationApp.Models;
using EducationApp.Models.Data;
using EducationApp.ViewModels.ProductViewModels;

namespace EducationApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppCtx _context;

        public ProductController(AppCtx context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var appCtx = _context.Products
                .Include(s => s.Сategory)
                .OrderByDescending(o => o.Manufacturer)
                .OrderBy(f => f.Сategory);

            return View(await appCtx.ToListAsync());
            /*return _context.Products != null ? 
                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'AppCtx.Products'  is null.");
            */
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

   

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["IdСategory"] = new SelectList(_context.Сategories.OrderBy(o => o.NameСategory), "Id", "NameСategory");
            /*ViewData["IdCategory"] = new SelectList(_context.FormsOfStudy
                .Where(w => w.IdUser == user.Id), "Id", "FormOfEdu");*/
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(ViewModels.ProductViewModels.CreateProductViewModels model)
        {
            Cart cart1 = new()
            { 
                Quantity = 1,
                DateCreated = DateTime.Now,
                ProductId = model.Id
            };
            _context.Add(cart1);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Product");
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModels.ProductViewModels.CreateProductViewModels model)
        {
            if (_context.Products
                 .Where(f => f.NameProduct == model.NameProduct)
                 .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Продукт с таким именем уже существует");
            }

            if (ModelState.IsValid)
            {
                Product product = new()
                {
                    NameProduct = model.NameProduct,
                    Manufacturer = model.Manufacturer,
                    IdСategory = model.IdСategory,
                };

                /*Cost cost = new()
                {
                    NewPrice = model.Cost,
                    IdProduct = model.Id,
                    IdСategory = model.IdСategory
                };*/

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdСategory"] = new SelectList(_context.Сategories.OrderBy(o => o.NameСategory), "Id", "NameCategory", model.IdСategory);
            return View(model);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["IdСategory"] = new SelectList(_context.Сategories.OrderBy(o => o.NameСategory), "Id", "NameСategory");
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameProduct,Manufacturer,IdСategory")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {*/
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            /*return RedirectToAction(nameof(Index));*/
            /*}*/
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppCtx.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
