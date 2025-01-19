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
    public class WaiterController : Controller
    {
        private readonly SystemDbContext _context;

        public WaiterController(SystemDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
          
            int branchID = (int)HttpContext.Session.GetInt32("BranchID")!;

          
            var orders = _context.Order
                .Where(o => o.ReservationDish != null && o.ReservationDish.Reservation != null
                            && o.ReservationDish.Reservation.BranchID == branchID)  
                .Include(o => o.ReservationDish)
                    .ThenInclude(rd => rd!.Reservation)
                    .ThenInclude(r => r!.User) 
                .AsEnumerable()
                .GroupBy(o => new
                {
                    o.TableNumber,
                    Time = o.Time.ToString("MMM dd, yyyy hh:mm tt"), 
                    UserName = o.ReservationDish?.Reservation?.User?.UserName 
                })
                .OrderByDescending(g => DateTime.ParseExact(g.Key.Time, "MMM dd, yyyy hh:mm tt", null))  
                .Select(g => new
                {
                    TableNumber = g.Key.TableNumber,
                    Time = g.Key.Time,
                    UserName = g.Key.UserName,
                    OrderID = g.First().OrderID,  
                    IsTaken = g.First().IsTaken  
                })
                .ToList();

            return View(orders);
        }

        public IActionResult ViewOrders(int tableNumber, string time)
        {
            DateTime parsedTime = DateTime.ParseExact(time, "MMM dd, yyyy hh:mm tt", null);

            var orders = _context.Order
                .Include(o => o.ReservationDish)
                    .ThenInclude(rd => rd!.Dish)
                .AsEnumerable()
                .Where(o => o.TableNumber == tableNumber &&
                            o.Time.ToString("MMM dd, yyyy hh:mm tt") == parsedTime.ToString("MMM dd, yyyy hh:mm tt"))
                .ToList();

            var totalPrice = orders.Sum(order => order.ReservationDish!.Dish!.Price * order.Quantity);

            ViewBag.TotalPrice = totalPrice;
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsTaken(int tableNumber, string time)
        {
            DateTime parsedTime = DateTime.ParseExact(time, "MMM dd, yyyy hh:mm tt", null);


            var orders = _context.Order
                .AsEnumerable()
                .Where(o => o.TableNumber == tableNumber &&
                            o.Time.ToString("MMM dd, yyyy hh:mm tt") == parsedTime.ToString("MMM dd, yyyy hh:mm tt"))
                .ToList();

            if (orders.Any())
            {

                foreach (var order in orders)
                {
                    order.IsTaken = !order.IsTaken;
                }


                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
