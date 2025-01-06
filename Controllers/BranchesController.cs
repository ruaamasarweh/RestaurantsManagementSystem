using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gp.Data;
using Gp.Models;

namespace Gp.Controllers
{
    public class BranchesController : Controller
    {
        private SystemDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public BranchesController(SystemDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            this._context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            int restaurantID = (int)HttpContext.Session.GetInt32("RestaurantID")!;
            List<Branch> branches = await _context.Branch.Where(u => u.RestaurantID == restaurantID).ToListAsync();
            return View(branches);
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branch
                .Include(b => b.Restaurant)
                .FirstOrDefaultAsync(m => m.BranchID == id);
            if (branch == null)
            {
                return NotFound();
            }     
            return View(branch);
        }

        // GET: Branches/Create
        public IActionResult Create()
        {
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantID", "RestaurantName");
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Branch branch)
        {
            try
            {
                int restaurantID = (int)HttpContext.Session.GetInt32("RestaurantID")!;
                branch.RestaurantID=restaurantID;         

                if (branch.ImageFile != null)
                {
                    var webRootPath = _hostingEnvironment.WebRootPath; //C:\\Users\\Dell\\Desktop\\Gp\\wwwroot

                    var imageFolder = Path.Combine(webRootPath, "images"); //C:\\Users\\Dell\\Desktop\\Gp\\wwwroot\\images
                    var uniqueFileName = $"{Guid.NewGuid()}_{branch.ImageFile.FileName}";
                    var imagePath = Path.Combine(imageFolder, uniqueFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await branch.ImageFile.CopyToAsync(stream);
                    }

                    branch.ImageFilePath = Path.Combine("images", uniqueFileName);

                }

                _context.Add(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branch.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantID", "RestaurantName", branch.RestaurantID);
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Branch branch)
        {
            try
            {
                Branch existingBranch = _context.Branch.Where(u => u.BranchID == id).FirstOrDefault()!;

                int restaurantID = (int)HttpContext.Session.GetInt32("RestaurantID")!;
                existingBranch.RestaurantID = restaurantID;

                if (branch.ImageFile != null)
                {
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    var imageFolder = Path.Combine(webRootPath, "images");

                    var oldImagePath = Path.Combine(webRootPath, existingBranch.ImageFilePath!); // old image path

                    if (System.IO.File.Exists(oldImagePath)) // delete old image from images folder
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    var uniqueFileName = $"{Guid.NewGuid()}_{branch.ImageFile.FileName}"; // generate unique file name
                    var newFilePath = Path.Combine(imageFolder, uniqueFileName);

                    using (var stream = new FileStream(newFilePath, FileMode.Create)) // save new image
                    {
                        await branch.ImageFile.CopyToAsync(stream);
                    }

                    existingBranch.ImageFilePath = Path.Combine("images", uniqueFileName); // update image path
                }

                existingBranch.LocationDescription = branch.LocationDescription;
                existingBranch.OpenTime = branch.OpenTime;
                existingBranch.CloseTime = branch.CloseTime;
                existingBranch.HasIndoorSeating = branch.HasIndoorSeating;
                existingBranch.HasOutdoorSeating = branch.HasOutdoorSeating;
                existingBranch.NumOfTables = branch.NumOfTables;
                existingBranch.phoneNumber = branch.phoneNumber;

                _context.Branch.Update(existingBranch);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(branch);
            }
        }


        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var branch = await _context.Branch
                .Include(b => b.Restaurant)
                .FirstOrDefaultAsync(m => m.BranchID == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branch = await _context.Branch.FindAsync(id);
            if (branch != null)
            {
                if (!string.IsNullOrEmpty(branch.ImageFilePath))
                {
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    var imagePath = Path.Combine(webRootPath, branch.ImageFilePath);

                    if (System.IO.File.Exists(imagePath)) //delete image from images folder when restaurant owner delete the restaurant
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Branch.Remove(branch);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BranchExists(int id)
        {
            return _context.Branch.Any(e => e.BranchID == id);
        }
    }
}
