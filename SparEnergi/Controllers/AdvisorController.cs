using Microsoft.AspNetCore.Mvc;
using SparEnergi.Models;
using SparEnergi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparEnergi.Controllers
{
    public class AdvisorController : Controller
    {
        MeetingsService meetingService = new MeetingsService();

        public IActionResult ProcessRequestMeeting(MeetingModel meetingModel)
        {

            if(meetingService.RegisterNewMeeting(meetingModel) == 1)
            {
                return View("MeetingRegistered");
            }
            else
            {
                return View();
            }
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
