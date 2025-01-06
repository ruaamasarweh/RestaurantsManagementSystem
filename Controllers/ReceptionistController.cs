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
    public class ReceptionistController : Controller
    {
        private readonly SystemDbContext _context;

        public ReceptionistController(SystemDbContext context)
        {
            _context = context;
        }
        // GET: Receptionist
        public async Task<IActionResult> Index(string sortOrder)
        {
            int branchID = (int)HttpContext.Session.GetInt32("BranchID")!;
            ViewBag.ReservationStatus = _context.ReservationStatus.ToList();
            List<Reservation> reservations = await _context.Reservation
                .Where(r => r.BranchID == branchID)
                .Include(r => r.Branch)
                .Include(r => r.ReservationStatus)
                .Include(r => r.User)
                .ToListAsync();


            reservations = sortOrder switch
            {
                "date_desc" => reservations.OrderByDescending(r => r.Date).ToList(),
                "date_asc" => reservations.OrderBy(r => r.Date).ToList(),
                _ => reservations.OrderBy(r => r.Date).ToList()
            };

            return View(reservations);
        }


        [HttpPost]
        public IActionResult UpdateAllReservationStatus(Dictionary<int, int?> SelectedStatuses)
        {
            if (SelectedStatuses != null)
            {
                foreach (var entry in SelectedStatuses)
                {
                    int reservationID = entry.Key;
                    int? newStatusID = entry.Value;

                    if (newStatusID.HasValue)
                    {
                        var reservation = _context.Reservation.Find(reservationID);

                        if (reservation != null)
                        {
                            reservation.ReservationStatusID = newStatusID.Value;
                        }
                    }
                }

              
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

       
            return View();
        }

    }
}
