using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NasaImagesDemo.Models;
using NasaImagesDemo.Repository;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;
using NasaImagesDemo.ViewModels;

namespace NasaImagesDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IFormFile _formfile;

        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        [Obsolete]
        private IHostingEnvironment _environment;

        [Obsolete]
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment Environment)
        {
            _logger = logger;
            _environment = Environment;
         
        }
        public IActionResult Index(ApodImageCreateViewModel file)
        {
                        
            var dateList = loadDatesfromFile();

            foreach (var item in dateList)
            {
                var result = formatDate(item);

                if (isExist(item))
                {
                    continue;
                }

                if (!IsValidDate(result))
                {
                    ViewBag.ErrorMessage = $"The date {result} that was supplied in not a valid date, Please make necessary changes to view the image";

                    continue;
                }
                else
                {                 
                        using (var webClient = new WebClient())
                        {
                            string url = webClient.DownloadString("https://api.nasa.gov/planetary/apod?api_key=PtXKNt00DLKSZ8XRjn9RkLt3QyYKknYtNFnlKvl4" + "&date=" + result);

                            //var imagecollections = JsonConvert.DeserializeObject<ApodImage>(url);
                            var imagecollection = JsonConvert.DeserializeObject<ApodImage>(url);
                            var imagecollections = JsonConvert.DeserializeObject<ApodImageCreateViewModel>(url);



                            var imageUrl = imagecollections.hdurl;

                            string path = @"C:\Users\User\source\repos\NasaImagesDemo\Images";

                            webClient.DownloadFile(new Uri(imageUrl), path);                            

                            string uniqueImageFileName = null;

                            if (file.Imageurl != null)

                            {
                                string uploadFolder = Path.Combine(_environment.WebRootPath, "~Images");
                                uniqueImageFileName = Guid.NewGuid().ToString() + "_" + file.Imageurl.FileName;
                                file.PhotoUrl = uniqueImageFileName;
                                string filepath = Path.Combine(uploadFolder, uniqueImageFileName);
                                file.Imageurl.CopyTo(new FileStream(filepath, FileMode.Create));
                            }
                            ApodImage newImage = new ApodImage
                            {
                                copyright = imagecollections.copyright,
                                date = imagecollections.date,
                                explanation = imagecollections.explanation,
                                hdurl = imagecollections.hdurl,
                                media_type = imagecollections.media_type,
                                service_version = imagecollections.service_version,
                                title = imagecollections.title,
                                url = uniqueImageFileName

                            };         
                      
                    };
                }
            }
         
            return View();
        }    

        protected bool IsValidDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                ViewBag.ErrorTitle = $"The date { date} is not a valid";
                ViewBag.ErrorMesage = $"The date { date} supplied is either not a valid day of the month or is not properly formatted. The image for this date {date} will not be displayed";
                
            }

            return false;

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

        public ActionResult GetApodImages()
        {
            var listOfImages = _unitOfWork.GetRepositoryInstance<ApodImage>().GetAllImages();
            ViewBag.Title = "Nasa Images";

            return View(listOfImages);
        }

        private bool isExist(string inputDate)
        {

            var listOfImages = _unitOfWork.GetRepositoryInstance<ApodImage>().GetAllImages().ToList();

            var list = listOfImages.Where(p => p.date == inputDate);

            if (list != null)
            {
                return true;

            }
            return false;
        }

    }
}
