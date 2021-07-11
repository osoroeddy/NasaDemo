using Microsoft.EntityFrameworkCore;
using NasaImagesDemo.ViewModels;

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
        DbSet<ApodImageCreateViewModel> ApodImageCreateViewModel { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApodImage>().HasData(
                new ApodImage
                {
                    ApodImageID = 2,
                    date = "2001-6-6",
                    explanation = "tesmessage",
                    title = "Test"

                }); ;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=ApodImageDB;Integrated Security=True");

        }


    }
}
