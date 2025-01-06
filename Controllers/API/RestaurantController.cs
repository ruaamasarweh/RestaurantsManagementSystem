using Gp.Data;
using Gp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gp.Controllers.API
{
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private SystemDbContext _context;
        public RestaurantController(SystemDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("api/restaurant/getRestaurants")]
        public IActionResult GetRestaurants()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}/";

            var restaurants = _context.Restaurant
                .Include(r => r.DishCategories!)
                    .ThenInclude(dc => dc.Dishes!)
                .Include(r => r.Branches!)
                .Select(r => new
                {
                    r.RestaurantID,
                    r.RestaurantName,
                    ImageUrl = baseUrl + r.ImageFilePath!.Replace("\\", "/"),

                    
                    DishCategories = r.DishCategories != null ? r.DishCategories.Select(dc => new DishCategoryDto
                    {
                        DishCategoryID=dc.DishCategoryID,
                        Name = dc.Name,
                        Dishes = dc.Dishes != null
                            ? dc.Dishes.Select(d => new DishDto
                            {
                                DishID=d.DishID,
                                DishName = d.DishName,
                                DishImageUrl = baseUrl + d.ImageFilePath!.Replace("\\", "/"),
                                Price = d.Price,
                                Details = d.Details
                            }).ToList()
                            : new List<DishDto>()
                    }).ToList() : new List<DishCategoryDto>(),

                   
                    Branches = r.Branches != null ? r.Branches.Select(b => new BranchDto
                    {
                        BranchID=b.BranchID,
                        LocationDescription = b.LocationDescription,
                        BranchImageUrl = baseUrl + b.ImageFilePath!.Replace("\\", "/"),
                        PhoneNumber = b.phoneNumber,
                        OpenTime = b.OpenTime,
                        CloseTime = b.CloseTime,
                        HasIndoorSeating = b.HasIndoorSeating,
                        HasOutdoorSeating = b.HasOutdoorSeating,
                        NumOfTables = b.NumOfTables
                    }).ToList() : new List<BranchDto>()
                }).ToList();


            return Ok(restaurants);
        }

        [HttpGet]
        [Route("api/restaurant/getRestaurantsBrief")]
        public IActionResult GetRestaurantsBrief()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}/";

            var restaurants = _context.Restaurant
                .Select(r => new
                {
                    r.RestaurantID,
                    r.RestaurantName,
                    ImageUrl = baseUrl + r.ImageFilePath!.Replace("\\", "/"),
                })
                .ToList();

            return Ok(restaurants);
        }





    }


}

