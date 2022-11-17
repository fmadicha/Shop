using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ShopContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ShopContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 3;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)_db.Products.Count() / pageSize);

            return View(await _db.Products.OrderByDescending(p => p.Id)
                .Include(p=>p.Category)
                .Skip((p - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_db.Categories, "id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = new SelectList(_db.Categories, "id", "Name", product.CategoryId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace("", "-");

                var slug = await _db.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "The product already exists");
                    return View(product);
                }



                if (product.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "/Images");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    product.Image = imageName;
                }
                _db.Add(product);
                await _db.SaveChangesAsync();

                TempData["Success"] = "The product has been created!";

                return RedirectToAction("Index");
            }
            return View(product);
        }


        public async Task<IActionResult> Edit(long id)
        {
            Product product =await  _db.Products.FindAsync(id);
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id ,Product product)
        {
            ViewBag.Categories = new SelectList(_db.Categories, "id", "Name", product.CategoryId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace("", "-");

                var slug = await _db.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "The product already exists");
                    return View(product);
                }



                if (product.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "/Images");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    product.Image = imageName;
                }
                _db.Update(product);
                await _db.SaveChangesAsync();

                TempData["Success"] = "The product has been edited!";

               
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(long id)
        {
            Product product = await _db.Products.FindAsync(id);

            if(!string.Equals(product.Image, "noimage.png"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "/Images");
                
                string oldImagePath = Path.Combine(uploadsDir, product.Image);

                if(System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            TempData["Success"] = "The product has been deleted!";
           
            return RedirectToAction("Index");
        }

    }
}
