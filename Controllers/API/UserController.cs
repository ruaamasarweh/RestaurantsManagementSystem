using Gp.Data;
using Gp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Controllers.API
{
  
    [ApiController]
    public class UserController : ControllerBase
    {
        private SystemDbContext _context;
        public UserController(SystemDbContext context)
        {
            this._context = context;
        }



        [HttpPost]
        [Route("api/user/signup")]

        public IActionResult Signup(Signup userSignup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                User? userDuplicate = _context.User.Where(u => u.UserName == userSignup.UserName
                                                           && u.FullName == userSignup.FullName
                                                           && u.PhoneNum == userSignup.PhoneNum
                                                           && u.Email == userSignup.Email
                                                           && u.Password == userSignup.Password
                                                           && u.Address == userSignup.Address
                                                           && u.Password == userSignup.Password).FirstOrDefault();

                User? usernameDuplicate = _context.User.Where(u => u.UserName == userSignup.UserName).FirstOrDefault();
                User? emailDuplicate=_context.User.Where(u=>u.Email== userSignup.Email).FirstOrDefault();

                if (userDuplicate != null) {
                    return BadRequest(new { message = "you already have an account!, login please" });
                  }

                if (usernameDuplicate != null) {
                    return BadRequest(new { message = "please change your username"});
                }

                if (emailDuplicate != null)
                {
                    return BadRequest(new { message = "this email has been used, please change email address" });
                }

                int userTypeID = _context.UserType.Where(u => u.TypeName == "Customer").Select(u => u.UserTypeID).FirstOrDefault();
                User user = new User();
                user.FullName = userSignup.FullName;
                user.UserName = userSignup.UserName;
                user.PhoneNum = userSignup.PhoneNum;
                user.Email = userSignup.Email;
                user.Password = userSignup.Password;
                user.Address = userSignup.Address;
                user.UserTypeID = userTypeID;

                _context.User.Add(user);
                _context.SaveChanges();

                return Ok(new { message = "user registered successfully",customerID=user.UserID,customerName=user.FullName?? "No Name Provided" });
            }
        }

        [HttpPost]
        [Route("api/user/login")]
        public IActionResult Login(Login userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
               User? user=_context.User.Where(u=>u.Email== userLogin.Email&&u.Password==userLogin.Password).FirstOrDefault();
                if (user==null) { 
                    return Unauthorized(new {message= "Invalid email or password." }); 
                }


                return Ok(new { message = "user registered successfully", customerID = user.UserID, customerName = user.FullName });
            }
        }

    }
}
