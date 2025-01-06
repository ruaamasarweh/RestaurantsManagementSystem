using Gp.Data;
using Gp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Gp.Controllers.API
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly SystemDbContext _context;


        public OrderController(SystemDbContext context)
        {
            _context = context;

        }
        [HttpPost("submitOrder")]
        public async Task<IActionResult> SubmitOrder(List<OrderDto> orders)
        {
            if (orders == null || !orders.Any())
            {
                return BadRequest("Order list cannot be empty.");
            }

            try
            {
                foreach (var orderDto in orders)
                {

                    var reservationDish = await _context.ReservationDish
                        .FirstOrDefaultAsync(rd => rd.ID_Dish == orderDto.DishID && rd.ID_Reservation == orderDto.ReservationID);

                    if (reservationDish == null)
                    {

                        reservationDish = new ReservationDish
                        {
                            ID_Reservation = orderDto.ReservationID,
                            ID_Dish = orderDto.DishID
                        };

                        _context.ReservationDish.Add(reservationDish);
                        await _context.SaveChangesAsync();
                    }

                    var order = new Order
                    {
                        ID_Reservation_Dish = reservationDish.ID_Reservation_Dish,
                        TableNumber = orderDto.TableNumber,
                        Quantity = orderDto.Quantity,
                        Time = orderDto.Time,
                    };

                    _context.Order.Add(order);
                }


                await _context.SaveChangesAsync();

                return Ok(new { message = "Order submitted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} - Inner exception: {ex.InnerException?.Message}");
            }
        }

    }
}