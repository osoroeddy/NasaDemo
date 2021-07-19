using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasaImagesDemo.Repository
{
   public class GenericRepositoryFake : IRepository<ApodImage>
    {
        public  IList<ApodImage> _apodImages;
       

        public GenericRepositoryFake()
        {           
            _apodImages = new List<ApodImage>();
            _apodImages.Add(new ApodImage() { ApodImageID = 1, copyright = "Jimmy WestlakerColorado college", date = "2017-02-27", explanation = "An odd thing about the group of lights..", url = "https://apod.nasa.gov/apod/image/1702/QuadQuasarLens_Hubble_2020.jpg" });
            _apodImages.Add(new ApodImage() { ApodImageID = 2, copyright = "SMU collge", date = "2018-061-02", explanation = "Volcanic activity on the Big Island of Hawaii has...", url = "https://apod.nasa.gov/apod/image/1806/KilaueaSkyPanTezel.jpg" });
            _apodImages.Add(new ApodImage() { ApodImageID = 3, copyright = "st paul collegee", date = "2016-07-13", explanation = "M7 is one of the most prominent open clusters of stars on..", url = "https://apod.nasa.gov/apod/image/1607/m7_colombari_1824.jpg" });
            _apodImages.Add(new ApodImage() { ApodImageID = 5, copyright = "petr foundation", date = "2018-04-30", explanation = "How great was the Great American Eclipse? The featured HDR image shows ", url = "https://apod.nasa.gov/apod/image/1804/AmericanEclipseHDR_Lefaudeux_3474.jpg" });

        }
        public void AddImages(ApodImage entity)
        {
            _apodImages.Add(entity);
        }

        public IEnumerable<ApodImage> GetAllImages()
        {
            return _apodImages;
        }

        public IQueryable<ApodImage> GetAllImagesIQuerable()
        {
            return (IQueryable<ApodImage>)(_apodImages as IQueryable);
        }
    }
}
