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
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using NasaImagesDemo.ViewModels;

namespace NasaImagesDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        [Obsolete]
        private IHostingEnvironment _environment;

        [Obsolete]
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment Environment)
        {
            _logger = logger;
            this._environment = Environment;
        }
        public IActionResult Index()
        {       

                return View();
        }

        [HttpPost]
        [Obsolete]
        public void AddImage(ApodImageCreateViewModel file)
        {
            var dateList = loadDatesfromFile();

            foreach (var item in dateList)
            {
               var result = formatDate(item);                

                if (isValidDate(result)) {

                    using (var webClient = new WebClient())
                    {
                        string url = "https://api.nasa.gov/planetary/apod?api_key=PtXKNt00DLKSZ8XRjn9RkLt3QyYKknYtNFnlKvl4" + "&date=" + result;

                        var imagecollections = JsonConvert.DeserializeObject<ApodImage>(url);

                        //imagefile = webClient.DownloadData(imagecollections.url);

                        string uniqueImageFileName = null;
                        if (file.Imageurl != null)
                        {

                            string uploadFolder = Path.Combine(_environment.WebRootPath, "Images");
                            uniqueImageFileName = Guid.NewGuid().ToString() + "_" + file.Imageurl.FileName;
                            string filepath = Path.Combine(uploadFolder, uniqueImageFileName);
                            file.Imageurl.CopyTo(new FileStream(filepath, FileMode.Create));
                        }
                        ApodImage newImage = new ApodImage
                        {
                            copyright = file.copyright,
                            date = file.date,
                            explanation = file.explanation,
                            hdurl = file.hdurl,
                            media_type = file.media_type,
                            service_version = file.service_version,
                            title = file.title,
                            url = uniqueImageFileName

                        };

                        _unitOfWork.GetRepositoryInstance<ApodImage>().AddImages(newImage);                    

                    }
                };
            
            }           

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
        public ActionResult Images(ApodImage image)
        {
            return View(_unitOfWork.GetRepositoryInstance<ApodImage>().GetAllImages());
        }     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
