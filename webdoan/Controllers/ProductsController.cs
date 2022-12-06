using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Drawing.Design;
using webdoan.Areas.Admin.Models;
using webdoan.Models;

namespace webdoan.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        public ProductsController(DataContext context)
        {
            _context = context;
        }
        [Route("products.html", Name = ("ShopProduct"))]
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                var lsTinDangs = _context.Products
                    .AsNoTracking()
                    .OrderBy(x => x.DateCreated);
                PagedList<Products> models = new PagedList<Products>(lsTinDangs, pageNumber, pageSize);

                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }


        }
        
        [Route("/product-{slug}-{id:long}.html", Name = ("ProductDetails"))]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _context.Products
                .FirstOrDefault(m => (m.ProductId == id) && (m.Active == true));
            if (product == null)
            {
                return NotFound();
            }
            return View(product);


        }

    }
}
