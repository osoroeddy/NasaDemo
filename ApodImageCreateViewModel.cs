using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NasaImagesDemo.ViewModels
{
    public class ApodImageCreateViewModel
    {
        public int Id { get; set; }
        public string copyright { get; set; }
        public string date { get; set; }
        public string explanation { get; set; }
        public string hdurl { get; set; }
        public string media_type { get; set; }
        public string service_version { get; set; }
        public string title { get; set; }
        [NotMapped]
        public IFormFile Imageurl { get; set; }        
        public string PhotoUrl { get; set; }
    }
}
