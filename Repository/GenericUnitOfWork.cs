using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasaImagesDemo.Repository
{
    public class GenericUnitOfWork : IDisposable
    {

        private NasaImagesDBContext DbEntity = new NasaImagesDBContext();

        public IRepository<Tbl_EntityType> GetRepositoryInstance<Tbl_EntityType>() where Tbl_EntityType : class
        {
            return new GenericRepository<Tbl_EntityType>(DbEntity);
        }
        
        private bool Disposed;       

        public void Dispose()
        {

            if (!this.Disposed)
            {
                DbEntity.Dispose();

            }
        }
    }
}
