using Gp.Data;
using Gp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Controllers.API
{
    [ApiController]
    public class MenuController : ControllerBase
    {
        private SystemDbContext _context;
        public MenuController(SystemDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("api/restaurant/getMenu")]
        public IActionResult GetMenu(int restaurantID)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}/";

            var dishCategories = _context.DishCategory
                .Where(dc => dc.RestaurantID == restaurantID)
                .Select(dc => new DishCategoryDto
                {
                    DishCategoryID = dc.DishCategoryID,
                    Name = dc.Name,
                    Dishes = dc.Dishes != null
                        ? dc.Dishes.Select(d => new DishDto
                        {
                            DishID = d.DishID,
                            DishName = d.DishName,
                            DishImageUrl = baseUrl + d.ImageFilePath!.Replace("\\", "/"),
                            Price = d.Price,
                            Details = d.Details
                        }).ToList()
                        : new List<DishDto>()
                })
                .ToList();

            if (dishCategories == null || !dishCategories.Any())
            {
                return NotFound("No menu available for this restaurant.");
            }

            return Ok(dishCategories);
        }

    }
}
