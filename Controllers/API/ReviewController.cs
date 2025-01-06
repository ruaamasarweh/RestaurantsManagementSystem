using Gp.Data;
using Gp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gp.Controllers.API
{
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private SystemDbContext _context;
        public ReviewController(SystemDbContext context)
        {
            this._context = context;
        }

         [HttpGet]
        [Route("api/branch/reviews")]

        public IActionResult GetReviews(int branchID)
        {
            var reviews=_context.Review
                .Where(r=>r.BranchID==branchID)
                .Select(r => new { 
                    r.ReviewID,
                    customerName=r.User!.FullName,
                    r.CustomerReview,
                    r.Rating,
                    r.ReviewDate,
                    r.BranchID
                } ).ToList() ;

            if (reviews == null || !reviews.Any()) {
                return NotFound(new {message= "No reviews found for this branch." });
            }
            return Ok(reviews);
        }

        [HttpPost]
        [Route("api/branch/submitReview")]
        public IActionResult SubmitReview(Review review) {

           // int customerID = (int)HttpContext.Session.GetInt32("customerID")!;

                Review newReview = new Review
                {
                    CustomerReview = review.CustomerReview,
                    Rating = review.Rating,
                    ReviewDate = DateTime.Now,
                    UserID = review.UserID,
                    BranchID = review.BranchID
                };
                _context.Review.Add(newReview);
                _context.SaveChanges();

                return Ok(new { message = "review submitted successfully" });
            }
  

        
    }
}
