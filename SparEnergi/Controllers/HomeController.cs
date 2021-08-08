using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SparEnergi.Data;
using SparEnergi.Models;
using SparEnergi.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SparEnergi.Controllers
{
    public class HomeController : Controller
    {
        ReadingService readingService = new ReadingService();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.UserLogged = HttpContext.Session.GetInt32("SessionUserId");
            ViewBag.SessionSuccess = HttpContext.Session.GetInt32("LoggedInSession");
            //ReadingService readingService = HttpContext.RequestServices.GetService(typeof(SparEnergi.Services.ReadingService)) as ReadingS;
            if (HttpContext.Session.GetInt32("SessionUserId") == null)
            {
                CSVReader();
            }
            return View();
        }

        public void CSVReader(/*ReadingService readingService*/)
        {
            String Folder = @"New CSV Files";
            var fileSystemWatcher = new FileSystemWatcher(Folder)
            {
                Filter = "*.csv",
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.Attributes,
                EnableRaisingEvents = true
            };

            fileSystemWatcher.Created += (object sender, FileSystemEventArgs e) =>
            {
                string NewFile = e.Name;
                string NewFilePath = Path.GetFullPath(NewFile);

                Debug.WriteLine("we made it");
                if (readingService.RegisterReadingsFromFile(NewFilePath))
                {
                    Debug.WriteLine("CSVReader wrote to the database");
                }
            };           
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
