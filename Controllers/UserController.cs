using Gp.Data;
using Gp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Controllers
{
    public class UserController : Controller
    {
        private SystemDbContext _context;
        public UserController(SystemDbContext context)
        {
            this._context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login userLogin)
        {
            if (ModelState.IsValid)
            {
                User? user = _context.User
                    .Where(u => u.Email!.Equals(userLogin.Email) && u.Password!.Equals(userLogin.Password)).FirstOrDefault();

                Employee? employee = _context.Employee.Where(u => u.Email!.Equals(userLogin.Email) && u.Password!.Equals(userLogin.Password))
                    .FirstOrDefault();

                if (user != null)
                {
                    UserType? usertype = _context.UserType.Where(u => u.UserTypeID == user.UserTypeID).FirstOrDefault();
                    string? userType = usertype!.TypeName;


                    if (userType == "Restaurant Owner")
                    {
                        HttpContext.Session.SetInt32("restaurantOwnerID", user.UserID);
                        return RedirectToAction("Index", "Restaurants");
                    }
                    else if (userType == "App Owner")
                    {
                        HttpContext.Session.SetInt32("appOwnerID", user.UserID);
                        return RedirectToAction("Index", "Admin");
                    }

                }
                else if (employee != null)
                {
                    EmployeeType? employeetype = _context.EmployeeType
                        .Where(u => u.EmployeeTypeID == employee.EmployeeTypeID)
                        .FirstOrDefault();

                    string? employeeType = employeetype?.EmpType;

                    if (employeeType == "Receptionist")
                    {
                        HttpContext.Session.SetInt32("ReceptionistID", employee.EmployeeID);
                        HttpContext.Session.SetInt32("BranchID", employee.BranchID);

                        return RedirectToAction("Index", "Receptionist");
                    }
                    else if (employeeType == "Waiter")
                    {
                        HttpContext.Session.SetInt32("WaiterID", employee.EmployeeID);
                        HttpContext.Session.SetInt32("BranchID", employee.BranchID);

                        return RedirectToAction("Index", "Waiter");
                    }
                }
                else
                {
                    TempData["msg"] = "Incorrect info, try again please";
                }

            }
            return View();
        }

    }
}

