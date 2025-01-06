using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gp.Data;
using Gp.Models;

namespace Gp.Controllers
{
    public class AdminController : Controller
    {
        private readonly SystemDbContext _context;

        public AdminController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: Admin
     
        public async Task<IActionResult> Index()
        {
            int appOwnerID = (int)HttpContext.Session.GetInt32("appOwnerID")!;
            List<User> restaurantsOwners = await _context.User
                .Where(u => u.UserType!.TypeName == "Restaurant Owner")
                .ToListAsync();
            return View(restaurantsOwners);

        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.User
                            .Include(u => u.UserType)
                            .FirstOrDefaultAsync(m => m.UserID == id && m.UserType!.TypeName == "Restaurant Owner");
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["UserTypeID"] = new SelectList(_context.UserType
                .Where(ut => ut.TypeName == "Restaurant Owner"), "UserTypeID", "TypeName");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                
                int? appOwnerID = HttpContext.Session.GetInt32("appOwnerID");
                var restaurantOwnerType = await _context.UserType
                    .FirstOrDefaultAsync(ut => ut.TypeName == "Restaurant Owner");

               
                user.UserTypeID = restaurantOwnerType!.UserTypeID; 
                _context.Add(user);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetInt32("restaurantOwnerID", user.UserID);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(u => u.UserID == id && u.UserType.TypeName == "Restaurant Owner");
            if (user == null)
            {
                return NotFound();
            }

            ViewData["UserTypeID"] = new SelectList(_context.UserType
                .Where(ut => ut.TypeName == "Restaurant Owner"), "UserTypeID", "TypeName", user.UserTypeID);
            return View(user);
        }
        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
         
            int? appOwnerID = HttpContext.Session.GetInt32("appOwnerID");
     
            if (id != user.UserID)
            {
                return NotFound();
            }

            var existingUser = await _context.User
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(u => u.UserID == id && u.UserType.TypeName == "Restaurant Owner");

      
            ViewData["UserTypeID"] = new SelectList(_context.UserType
                .Where(ut => ut.TypeName == "Restaurant Owner"), "UserTypeID", "TypeName", user.UserTypeID);

            if (ModelState.IsValid)
            {
                try
                {
                    
                    existingUser.FullName = user.FullName;
                    existingUser.UserName = user.UserName;
                    existingUser.PhoneNum = user.PhoneNum;
                    existingUser.Address = user.Address;
                    existingUser.Email = user.Email;
                    existingUser.Password = user.Password;

                    _context.Update(existingUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(user);
        }



        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.UserID == id && m.UserType.TypeName == "Restaurant Owner");
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.UserID == id && u.UserType.TypeName == "Restaurant Owner");

            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserID == id && e.UserType.TypeName == "Restaurant Owner");
        }
    }
}