using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NasaImagesDemo.Repository
{
    public class GenericUnitOfWork<Tbl_Entity> : IRepository<Tbl_Entity> where Tbl_Entity : class
    {

        private DbSet<Tbl_Entity> _dbSet;

        private NasaImagesDBContext _DbEntity;

        private IRepository<ApodImage> _nasaImagesrepo { get; set; }

        public GenericUnitOfWork(IRepository<ApodImage> nasaImageRepo)
        {
            _nasaImagesrepo = nasaImageRepo;

        }

        public GenericUnitOfWork(NasaImagesDBContext DBEntity)
        {
            _DbEntity = DBEntity;
            
            _dbSet = _DbEntity.Set<Tbl_Entity>();
        }
        
        public void AddImages(Tbl_Entity entity)
        {
            _dbSet.Add(entity);
            _DbEntity.SaveChanges();
        }
       

        public IEnumerable<Tbl_Entity> GetAllImages()
        {
            return _dbSet.ToList();
        }

        public IQueryable<Tbl_Entity> GetAllImagesIQuerable()
        {
            return _dbSet;
        }       
    
    }
}
