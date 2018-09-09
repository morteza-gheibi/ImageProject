using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ImageProject.DataLayout.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAllFilter(Expression<Func<TEntity, bool>> where,Paging paging, ref int RowCount);
        IEnumerable<TEntity> GetAll(ref int RowCount,Expression<Func<TEntity, bool>> where = null);
        TEntity GetById(object Id);
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Delete(object Id);
        Task DeleteAsync(object Id);
    }
}
