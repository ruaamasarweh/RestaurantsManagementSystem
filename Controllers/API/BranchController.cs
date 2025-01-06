using Gp.Data;
using Gp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gp.Controllers.API
{
    [ApiController]
    public class BranchController : ControllerBase
    {
        private SystemDbContext _context;
        public BranchController(SystemDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("api/restaurant/getBranchDetails")]
        public IActionResult GetBranchDetails(int restaurantID, int branchID)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}/";

            var branch = _context.Branch
                .Where(b => b.RestaurantID == restaurantID && b.BranchID == branchID)
                .Select(b => new BranchDto
                {
                    BranchID = b.BranchID,
                    LocationDescription = b.LocationDescription,
                    BranchImageUrl = baseUrl + b.ImageFilePath!.Replace("\\", "/"),
                    PhoneNumber = b.phoneNumber,
                    OpenTime = b.OpenTime,
                    CloseTime = b.CloseTime,
                    HasIndoorSeating = b.HasIndoorSeating,
                    HasOutdoorSeating = b.HasOutdoorSeating,
                    NumOfTables = b.NumOfTables
                })
                .FirstOrDefault();

            if (branch == null)
            {
                return NotFound("Branch not found");
            }

            return Ok(branch);
        }

    }
}
