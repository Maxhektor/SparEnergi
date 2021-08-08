using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using SparEnergi.Services;
using SparEnergi.Models;

namespace SparEnergi.Controllers
{
    public class ReadingController : Controller
    {
        ReadingService readingService = new ReadingService();  
       
        public IActionResult ProcessReading(ReadingModel readingModel)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            if (readingService.RegisterNewReading(readingModel, userId) == 1)
            {
                return View("ReadingRegistered");
            }
            else
            {
                return View();
            }
        }


        public IActionResult ShowAllReadings(int UserId)
        {
            if(HttpContext.Session.GetInt32("SessionUserId") != null)
            {
                List<ReadingModel> readingModelsArray = readingService.FetchReadings((int)HttpContext.Session.GetInt32("SessionUserId"));
                ViewBag.readingModelsArray = readingModelsArray;
                ViewBag.UserLoggedIn = HttpContext.Session.GetInt32("SessionUserId");
                return View();
            } else
            {
                return View("Index", "Home");
            }           
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
