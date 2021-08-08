using Microsoft.AspNetCore.Mvc;
using SparEnergi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SparEnergi.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace SparEnergi.Controllers
{
    public class RegisterController : Controller
    {
        UserService userService = new UserService();

        public IActionResult ProcessRegister(UserModel userModel)
        {
            if (userService.RegisterNewUser(userModel) == 1)
            {
                return View("RegisterSuccess", userModel);

            } 
            else
            {
                return View("Index");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
