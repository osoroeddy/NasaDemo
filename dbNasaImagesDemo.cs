using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasaImagesDemo
{
    public partial class dbNasaImagesDemo : DbContext
    {
        public virtual DbSet<ApodImage> ApodImages {get; set;}
    }
}
