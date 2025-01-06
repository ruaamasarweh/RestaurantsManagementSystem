using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gp.Data;
using Gp.Models;

namespace Gp.Controllers
{
    public class DishCategoriesController : Controller
    {
        private readonly SystemDbContext _context;

        public DishCategoriesController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: DishCategories
        public IActionResult Index(int RestaurantID)
        {
            int restaurantID = (int)HttpContext.Session.GetInt32("RestaurantID")!;
            List<DishCategory> categories = _context.DishCategory.Where(u => u.RestaurantID == restaurantID).ToList();
            return View(categories);
        }

        // GET: DishCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory = await _context.DishCategory
                .Include(d => d.Restaurant)
                .FirstOrDefaultAsync(m => m.DishCategoryID == id);
            if (dishCategory == null)
            {
                return NotFound();
            }

            return View(dishCategory);
        }

        // GET: DishCategories/Create
        public IActionResult Create()
        {
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantID", "RestaurantName");
            return View();
        }

        // POST: DishCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DishCategory dishCategory)
        {
            try
            {
                int restaurantID = (int)HttpContext.Session.GetInt32("RestaurantID")!;
                dishCategory.RestaurantID = restaurantID;
                _context.Add(dishCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(dishCategory);
            }
        }

        // GET: DishCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory = await _context.DishCategory.FindAsync(id);
            if (dishCategory == null)
            {
                return NotFound();
            }
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantID", "RestaurantName", dishCategory.RestaurantID);
            return View(dishCategory);
        }

        // POST: DishCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DishCategory dishCategory)
        {
            try
            {
                int restaurantID = (int)HttpContext.Session.GetInt32("RestaurantID")!;
                dishCategory.RestaurantID = restaurantID;
                _context.Update(dishCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishCategoryExists(dishCategory.DishCategoryID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: DishCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory = await _context.DishCategory
                .Include(d => d.Restaurant)
                .FirstOrDefaultAsync(m => m.DishCategoryID == id);
            if (dishCategory == null)
            {
                return NotFound();
            }

            return View(dishCategory);
        }

        // POST: DishCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dishCategory = await _context.DishCategory.FindAsync(id);
            if (dishCategory != null)
            {
                _context.DishCategory.Remove(dishCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult SelectCategory(int categoryID, string selectedController, string selectedAction)
        {
            HttpContext.Session.SetInt32("CategoryID", categoryID);
            return RedirectToAction(selectedAction, selectedController);
        }
        private bool DishCategoryExists(int id)
        {
            return _context.DishCategory.Any(e => e.DishCategoryID == id);
        }
    }
}
