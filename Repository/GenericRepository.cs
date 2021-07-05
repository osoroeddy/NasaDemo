using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasaImagesDemo.Repository
{
    public class GenericRepository<Tbl_Entity> : IRepository<Tbl_Entity> where Tbl_Entity : class
    {

        private DbSet<Tbl_Entity> _dbSet;

        private NasaImagesDBContext _DbEntity;


        public GenericRepository(NasaImagesDBContext DBEntity)
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
