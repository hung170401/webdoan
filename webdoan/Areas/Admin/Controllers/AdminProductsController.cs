using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webdoan.Areas.Admin.Models;
using webdoan.Helpper;
using webdoan.Models;

namespace webdoan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly DataContext _context;

        public AdminProductsController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminProducts
        public async Task<IActionResult> Index()
        {
              return View(await _context.Products.ToListAsync());
        }

        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,MetaDesc,MetaKey,UnitsInStock")] Products products, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                products.ProductName = Utilities.ToTitleCase(str: products.ProductName);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(products.ProductName) + extension;
                    products.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                }
                if (string.IsNullOrEmpty(products.Thumb)) products.Thumb = "default.jpg";
                products.Alias = Utilities.SEOUrl(products.ProductName);
                products.DateModified = DateTime.Now;
                products.DateCreated = DateTime.Now;

                _context.Add(products);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
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

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,MetaDesc,MetaKey,UnitsInStock")] Products products, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

           
            if (ModelState.IsValid)
                {
                    products.ProductName = Utilities.ToTitleCase(str: products.ProductName);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(products.ProductName) + extension;
                        products.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(products.Thumb)) products.Thumb = "default.jpg";
                    products.Alias = Utilities.SEOUrl(products.ProductName);
                    products.DateModified = DateTime.Now;
                  

                    _context.Update(products);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
            }
                return View(products);
            
        }

        // GET: Admin/AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'DataContext.Products'  is null.");
            }
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
          return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
