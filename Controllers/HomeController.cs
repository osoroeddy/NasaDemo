using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NasaImagesDemo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NasaImagesDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static byte[] imagefile;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
                      


            return View();
        }

        //public bool isValideDate(string date)
        //{


        //}

        //Load date input from a local text file
        public string[] loaddatesfromFile()
        {
            //Read dates from text file and Save the inputs to a string array
            string[] datesText = System.IO.File.ReadAllLines(@"C:\Users\User\testFolder\dates.txt");

            return datesText;

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
