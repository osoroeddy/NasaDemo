using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NasaImagesDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
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

            using (var webClient = new WebClient())
            {
                string imagesJson = webClient.DownloadString("https://api.nasa.gov/planetary/apod?api_key=PtXKNt00DLKSZ8XRjn9RkLt3QyYKknYtNFnlKvl4");
                var imagecollections = JsonConvert.DeserializeObject<ApodImage>(imagesJson);

                imagefile = webClient.DownloadData(imagecollections.url);
            }

                return View();
        }

        protected bool isValidDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string formatDate(string date)
        {           
            DateTime myDateTime = DateTime.Parse(date);

            var dateInfo = myDateTime.ToString(new CultureInfo("zh-cn"));

            var result = dateInfo.Replace("/", "-");   
            
            int lastAcceptedChar = result.IndexOf(" ");   
            
            string formatedDate = result.Substring(0, lastAcceptedChar);          

            return formatedDate;
        }

        //Load date input from a local text file
        public string[] loadDatesfromFile()
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
