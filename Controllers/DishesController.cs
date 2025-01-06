using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gp.Data;
using Gp.Models;

namespace Gp.Controllers
{
    public class DishesController : Controller
    {
        private SystemDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public DishesController(SystemDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            this._context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Dishes
        public IActionResult Index()
        {
            int categoryID = (int)HttpContext.Session.GetInt32("CategoryID")!;
            List<Dish>dishes=_context.Dish.Where(u=>u.DishCategoryID == categoryID).ToList();
            return View(dishes);
        }

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dish
                .Include(d => d.DishCategory)
                .FirstOrDefaultAsync(m => m.DishID == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            ViewData["DishCategoryID"] = new SelectList(_context.DishCategory, "DishCategoryID", "Name");
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dish dish)
        {
            try { 
            int categoryID = (int)HttpContext.Session.GetInt32("CategoryID")!;
            dish.DishCategoryID = categoryID;
                if (dish.ImageFile != null)
                {
                    var webRootPath = _hostingEnvironment.WebRootPath; //C:\\Users\\Dell\\Desktop\\Gp\\wwwroot

                    var imageFolder = Path.Combine(webRootPath, "images"); //C:\\Users\\Dell\\Desktop\\Gp\\wwwroot\\images
                    var uniqueFileName = $"{Guid.NewGuid()}_{dish.ImageFile.FileName}";
                    var imagePath = Path.Combine(imageFolder, uniqueFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await dish.ImageFile.CopyToAsync(stream);
                    }

                    dish.ImageFilePath = Path.Combine("images", uniqueFileName);

                }

                _context.Add(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dish.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            ViewData["DishCategoryID"] = new SelectList(_context.DishCategory, "DishCategoryID", "Name", dish.DishCategoryID);
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Dish dish)
        {
            try
            {

                Dish existingDish = _context.Dish.Where(u => u.DishID == id).FirstOrDefault()!; 
                int categoryID = (int)HttpContext.Session.GetInt32("CategoryID")!;
              

                if (dish.ImageFile != null)
                {
                    var webRootPath = _hostingEnvironment.WebRootPath; //C:\\Users\\Dell\\Desktop\\Gp\\wwwroot
                    var imageFolder = Path.Combine(webRootPath, "images"); //C:\\Users\\Dell\\Desktop\\Gp\\wwwroot\\images

                    
                    var oldImagePath = Path.Combine(webRootPath, existingDish.ImageFilePath!);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath); // Delete the old image
                    }


                    var uniqueFileName = $"{Guid.NewGuid()}_{dish.ImageFile.FileName}";
                    var newImagePath = Path.Combine(imageFolder, uniqueFileName);


                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        await dish.ImageFile.CopyToAsync(stream);
                    }


                    existingDish.ImageFilePath = Path.Combine("images", uniqueFileName);
                }
                else
                {
                    existingDish.ImageFilePath = existingDish.ImageFilePath;
                }
                existingDish.DishCategoryID = categoryID;
                existingDish.DishName = dish.DishName;
                existingDish.Details = dish.Details;
                existingDish.Price = dish.Price;
                _context.Dish.Update(existingDish);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch
            {
                return View(dish);
            }
        }

        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dish
                .Include(d => d.DishCategory)
                .FirstOrDefaultAsync(m => m.DishID == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dish.FindAsync(id);
            if (dish != null)
            {
                if (!string.IsNullOrEmpty(dish.ImageFilePath))
                {
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    var imagePath = Path.Combine(webRootPath, dish.ImageFilePath);

                    if (System.IO.File.Exists(imagePath)) //delete image from images folder when restaurant owner delete the restaurant
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                _context.Dish.Remove(dish);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dish.Any(e => e.DishID == id);
        }
    }
}
