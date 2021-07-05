using Microsoft.EntityFrameworkCore;


namespace NasaImagesDemo
{
    public class NasaImagesDBContext : DbContext
    {
        public NasaImagesDBContext()
        {
        }

        public NasaImagesDBContext(DbContextOptions<NasaImagesDBContext> options)
            : base(options)
        { 
                
        }


        DbSet<ApodImage> ApodImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=ApodImageDB;Integrated Security=True");

        }


    }
}
