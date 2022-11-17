using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Data.Components;

using Shop.Models;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ShopContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(string categorySlug = "", int p = 1)
        {
            int pageSize = 3;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.CategorySlug = categorySlug;


            if (categorySlug == "")
            {
                ViewBag.TotalPages = (int)Math.Ceiling((decimal)_db.Products.Count() / pageSize);
                var model = await _db.Products.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync();
                return View(model);
            }

            Category category = await _db.Categories.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();
            if (category == null) RedirectToAction("Index");

            var productsByCategory = _db.Products.Where(p => p.CategoryId == category.Id);
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)productsByCategory.Count() / pageSize);
            return View(await productsByCategory.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
        }

       
    }
}
