using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasaImagesDemo.Repository
{
    public interface IRepository<Tbl_Entity> where Tbl_Entity:class
    {
        //Tbl_Entity Images;               
        IEnumerable<Tbl_Entity> GetAllImages();
        IQueryable<Tbl_Entity> GetAllImagesIQuerable();        
        void AddImages(Tbl_Entity entity);

    }
}
