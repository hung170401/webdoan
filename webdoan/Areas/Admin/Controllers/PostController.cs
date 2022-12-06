using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using webdoan.Areas.Admin.Models;
using webdoan.Helpper;
using webdoan.Models;

namespace webdoan.Areas.Admin.Controllers
{
        [Area("Admin")]
        public class PostController : Controller
        {
            private readonly DataContext _context;
            public PostController(DataContext context)
            {
                _context = context;
            }
            public IActionResult Index()
            {
                var mnList = _context.Posts.OrderBy(m => m.PostID).ToList();
                return View(mnList);

            }
            public IActionResult Create()
            {
                var mnList = (from m in _context.Posts
                              select new SelectListItem()
                              {
                                  Text = m.Title,
                                  Value = m.PostID.ToString(),
                              }).ToList();
                mnList.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = "0"
                });
                ViewBag.mnList = mnList;

                return View();

            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> CreateAsync(Post post, Microsoft.AspNetCore.Http.IFormFile fThumb)
            {
            if (ModelState.IsValid)
            {
                post.Abstract = Utilities.ToTitleCase(str: post.Abstract);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(post.Abstract) + extension;
                    post.Images = await Utilities.UploadFile(fThumb, @"post", image.ToLower());
                }
                if (string.IsNullOrEmpty(post.Images)) post.Images = "default.jpg";
                
                post.CreatedDate = DateTime.Now;

                _context.Add(post);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
            public IActionResult Delete(int? id)
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var mn = _context.Posts.Find(id);
                if (mn == null)
                {
                    return NotFound();
                }
                return View(mn);
            }
            [HttpPost]
            public IActionResult Delete(int id)
            {

                var delePost = _context.Posts.Find(id);
                if (delePost == null)
                {
                    return NotFound();
                }
                _context.Posts.Remove(delePost);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            public IActionResult Edit(int? id)
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var mn = _context.Posts.Find(id);
                if (mn == null)
                {
                    return NotFound();
                }

                var mnList = (from m in _context.Posts
                              select new SelectListItem()
                              {
                                  Text = m.Title,
                                  Value = m.PostID.ToString(),
                              }).ToList();
                mnList.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
                ViewBag.mnList = mnList;

                return View(mn);

            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditAsync(Post post, Microsoft.AspNetCore.Http.IFormFile fThumb)
            {
            if (ModelState.IsValid)
            {
                post.Abstract = Utilities.ToTitleCase(str: post.Abstract);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(post.Abstract) + extension;
                    post.Images = await Utilities.UploadFile(fThumb, @"post", image.ToLower());
                }
                if (string.IsNullOrEmpty(post.Images)) post.Images = "default.jpg";
                post.CreatedDate = DateTime.Now;

                _context.Update(post);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
        }
    
}
