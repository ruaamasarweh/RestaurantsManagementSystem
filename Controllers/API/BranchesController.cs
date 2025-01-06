using Gp.Data;
using Gp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gp.Controllers.API
{
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private SystemDbContext _context;
        public BranchesController(SystemDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("api/restaurant/getBranches")]
        public IActionResult GetBranches(int restaurantID)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}/";

            var branches = _context.Branch
                .Where(b => b.RestaurantID == restaurantID)
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
                .ToList();

            return Ok(branches);
        }

    }
}
