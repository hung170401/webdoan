using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webdoan.Areas.Admin.Models;
using webdoan.Models;

namespace webdoan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly DataContext _context;

        public AdminCategoriesController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminCategories
        public async Task<IActionResult> Index()
        {
              return View(await _context.Categories.ToListAsync());
        }

        // GET: Admin/AdminCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Admin/AdminCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCategories/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatId,CatName,Published,Description")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }

        // GET: Admin/AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }

        // POST: Admin/AdminCategories/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatId,CatName,Published,Description")] Categories categories)
        {
            if (id != categories.CatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(categories.CatId))
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
            return View(categories);
        }

        // GET: Admin/AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Admin/AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'DataContext.Categories'  is null.");
            }
            var categories = await _context.Categories.FindAsync(id);
            if (categories != null)
            {
                _context.Categories.Remove(categories);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesExists(int id)
        {
          return _context.Categories.Any(e => e.CatId == id);
        }
    }
}
