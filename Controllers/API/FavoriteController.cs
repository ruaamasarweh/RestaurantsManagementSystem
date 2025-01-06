using Gp.Data;
using Gp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gp.Controllers.API
{

    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private SystemDbContext _context;
        public FavoriteController(SystemDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("api/branches/getFavBranches")]
        public IActionResult GetFavBranches(int userID)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}/";

            var restaurants = _context.Restaurant
                .Where(r => r.Branches!
                    .Any(b => _context.Favorite.Any(f => f.UserID == userID && f.BranchID == b.BranchID)))
                .Select(r => new
                {
                    r.RestaurantID,
                    r.RestaurantName,
                    Branches = r.Branches!
                        .Where(b => _context.Favorite.Any(f => f.UserID == userID && f.BranchID == b.BranchID))
                        .Select(b => new BranchDto
                        {
                            BranchID = b.BranchID,
                            LocationDescription = b.LocationDescription,
                            BranchImageUrl = baseUrl + b.ImageFilePath!.Replace("\\", "/"),
                        }).ToList()
                })
                .ToList();

            return Ok(restaurants);
        }


        [HttpPost]
        [Route("api/branches/addRemoveFav")]
        public async Task<IActionResult> AddRemoveFav(AddRemove fav) {
            var existingFavorite = _context.Favorite
                .FirstOrDefault(f => f.UserID == fav.userID && f.BranchID == fav.branchID);
            if (existingFavorite != null)
            {
                _context.Favorite.Remove(existingFavorite);
                _context.SaveChanges();
                return Ok(new { message = "Branch removed from favorite." });
            }
            else{
                var newFavorite = new Favorite
                {
                    UserID = fav.userID,
                    BranchID = fav.branchID
                };
                _context.Favorite.Add(newFavorite);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Branch added to favorite." });
            }
        }
       
    }
}
