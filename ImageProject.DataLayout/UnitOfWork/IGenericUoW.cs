using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProject.DataLayout.Repositories;

namespace ImageProject.DataLayout.UnitOfWork
{ 
    public interface IGenericUoW
    {
        IGenericRepository<T> Repository<T>() where T : class;

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
