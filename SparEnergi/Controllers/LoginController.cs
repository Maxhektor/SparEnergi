using Microsoft.AspNetCore.Mvc;
using SparEnergi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SparEnergi.Services;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace SparEnergi.Controllers
{
    public class LoginController : Controller
    {
        UserService userService = new UserService();
        public IActionResult Index()
        {          
            return View();
        }

        public IActionResult ProcessLogin(UserModel userModel)
        {
            UserModel loggedUser = userService.FindUserByUsernameAndPassword(userModel.Username, userModel.Password);          
                      

            if (loggedUser == null)
            {
                if(HttpContext.Session.GetInt32("SessionApproval") > 0)
                {
                    loggedUser = userService.GetUserById((int)HttpContext.Session.GetInt32("SessionUserId"));
                    HttpContext.Session.SetInt32("SessionUserId", loggedUser.Id);
                    HttpContext.Session.SetInt32("SessionApproval", 1);
                    return View("UserPage", loggedUser);
                }
                return View("LoginFailure");
            }
            HttpContext.Session.SetInt32("SessionUserId", loggedUser.Id);
            HttpContext.Session.SetInt32("SessionApproval", 1);
            return View("UserPage", loggedUser);
            //return View("LoginFailure");


            //HttpContext.Session.SetInt32("SessionUserId", loggedUser.Id);
            //HttpContext.Session.SetInt32("SessionApproval", 1);

            //return View("UserPage", loggedUser);
        } 
    
        public IActionResult ProcessLogout()
        {
            HttpContext.Session.Clear();
            return View("Views/Home/Index.cshtml");
        }

        public IActionResult IFrame()
        {
            return View("IFrame");
        }
    
    }
}
