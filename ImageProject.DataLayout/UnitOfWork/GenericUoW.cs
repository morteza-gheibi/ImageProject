using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ImageProject.DataLayout.Modal;
using ImageProject.DataLayout.Repositories;

namespace ImageProject.DataLayout.UnitOfWork
{
        public class GenericUoW : IGenericUoW , IDisposable
        {
            private readonly ImageDBEntities1 entities = null;
            public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

            public GenericUoW()
            {
                this.entities = new ImageDBEntities1();
            }

        public void Dispose()
        {
            entities.Dispose();
        }


        

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IGenericRepository<T>;
            }

            IGenericRepository<T> repo = new GenericRepository<T>(entities);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public void SaveChanges() => entities.SaveChanges();
        public async Task SaveChangesAsync() => await entities.SaveChangesAsync();
    }
}