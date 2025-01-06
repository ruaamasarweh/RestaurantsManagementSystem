using Gp.Data;
using Gp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Gp.Controllers.API
{
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly SystemDbContext _context;

        public ReservationController(SystemDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/branch/submitReservation")]
        public async Task<IActionResult> SubmitReservation(ReservationDto reservationDto)
        {
            if (reservationDto == null)
            {
                return BadRequest("Invalid reservation data.");
            }

            try
            {
                int pendingID=_context.ReservationStatus.Where(u=>u.ReservationnStatus=="Pending").Select(i=>i.ReservationStatusID).FirstOrDefault();
               
                var reservation = new Reservation
                {
                    Date = reservationDto.Date,                       
                    Time = reservationDto.Time,                       
                    NumOfPeople = reservationDto.NumOfPeople,        
                    TableZone = reservationDto.TableZone,             
                    UserID = reservationDto.UserID,                  
                    BranchID = reservationDto.BranchID,               
                    ReservationStatusID = pendingID
                };

                _context.Reservation.Add(reservation);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Reservation submitted successfully", reservationId = reservation.ReservationID });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("api/getReservation")]
        public IActionResult GetReservations(int customerID)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}/";
            var reservations = _context.Reservation
                .Include(r=>r.Branch)
                    .ThenInclude(b=>b!.Restaurant)
                .Include(r => r.ReservationStatus)
                .Where(r => r.UserID == customerID)
                .Select(r => new
                {
                r.ReservationID,
                r.NumOfPeople,
                r.TableZone,
                r.Date,
                r.Time,
                    ReservationStatus = r.ReservationStatus!.ReservationnStatus,
                    Branch = new {
                    r.Branch!.BranchID,
                    r.Branch.LocationDescription,
                    r.Branch.phoneNumber,
                    branchImageUrl= baseUrl + r.Branch.ImageFilePath!.Replace("\\", "/"),
                    r.Branch.OpenTime,
                    r.Branch.CloseTime,
                    r.Branch.HasIndoorSeating,
                    r.Branch.HasOutdoorSeating,
                    r.Branch.NumOfTables,
                },
                    Restaurant = new
                    {
                        r.Branch.Restaurant!.RestaurantID,
                        r.Branch.Restaurant.RestaurantName,
                        ImageUrl = baseUrl + r.Branch.Restaurant.ImageFilePath!.Replace("\\", "/"),
                    }


                }).ToList();
            return Ok(reservations);

        }

    }
}





