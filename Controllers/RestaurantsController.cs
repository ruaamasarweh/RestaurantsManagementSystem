using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gp.Data;
using Gp.Models;

namespace Gp.Controllers
{
    public class RestaurantsController : Controller
    {
        private SystemDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public RestaurantsController(SystemDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            this._context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            int restaurantOwnerID = (int)HttpContext.Session.GetInt32("restaurantOwnerID")!;
            List<Restaurant> restaurants = await _context.Restaurant.Where(u => u.UserID == restaurantOwnerID).ToListAsync();
            return View(restaurants);
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RestaurantID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "Address");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Restaurant restaurant)
        {
            try
            {
                int restaurantOwnerID = (int)HttpContext.Session.GetInt32("restaurantOwnerID")!;
                restaurant.UserID = restaurantOwnerID;

                if (restaurant.ImageFile != null)
                {
                    var webRootPath = _hostingEnvironment.WebRootPath; //C:\\Users\\Dell\\Desktop\\Gp\\wwwroot

                    var imageFolder = Path.Combine(webRootPath, "images"); //C:\\Users\\Dell\\Desktop\\Gp\\wwwroot\\images
                    var uniqueFileName = $"{Guid.NewGuid()}_{restaurant.ImageFile.FileName}";
                    var imagePath = Path.Combine(imageFolder, uniqueFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await restaurant.ImageFile.CopyToAsync(stream);
                    }

                    restaurant.ImageFilePath = Path.Combine("images", uniqueFileName);

                }

                _context.Restaurant.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "Address", restaurant.UserID);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Restaurant restaurant)
        {
            if (id != restaurant.RestaurantID)
            {
                return NotFound();
            }

            try
            {
                int restaurantOwnerID = (int)HttpContext.Session.GetInt32("restaurantOwnerID")!;

                Restaurant existingRestaurant = _context.Restaurant.Where(u => u.RestaurantID == id).FirstOrDefault()!;

                if (restaurant.ImageFile != null)
                {
                    var webRootPath = _hostingEnvironment.WebRootPath; 
                    var imageFolder = Path.Combine(webRootPath, "images"); 

                   
                    var oldImagePath = Path.Combine(webRootPath, existingRestaurant.ImageFilePath!);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath); 
                    }

                   
                    var fileExtension = Path.GetExtension(restaurant.ImageFile.FileName);
                    var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                    var newFilePath = Path.Combine(imageFolder, uniqueFileName);

                   
                    using (var stream = new FileStream(newFilePath, FileMode.Create))
                    {
                        await restaurant.ImageFile.CopyToAsync(stream);
                    }

                    
                    existingRestaurant.ImageFilePath = Path.Combine("images", uniqueFileName);
                }

               
                existingRestaurant.UserID = restaurantOwnerID;
                existingRestaurant.RestaurantName = restaurant.RestaurantName;

              
                _context.Restaurant.Update(existingRestaurant);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(restaurant);
            }
        }


        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RestaurantID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurant.Remove(restaurant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurant.Any(e => e.RestaurantID == id);
        }

        public IActionResult SelectRestaurant(int restaurantID, string selectedController, string selectedAction)
        {
            HttpContext.Session.SetInt32("RestaurantID", restaurantID);
            return RedirectToAction(selectedAction, selectedController);
        }

    }
}
