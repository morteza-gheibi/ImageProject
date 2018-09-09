using ImageProject.DataLayout.Modal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace ImageProject.DataLayout.Repositories
{
    class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ImageDBEntities1 entities = null;
        
        private DbSet<TEntity> _dbSet;

        public GenericRepository(ImageDBEntities1 _entities)
        {
            entities = _entities;
            _dbSet = entities.Set<TEntity>();
        }

        //public IEnumerable<Book> GetRange(int index, int count, Expression<Func<Book, int>> orderLambda, Expression<Func<Book, bool>> whereLambda)
        //{
        //    return _context.Books.Where(whereLambda).OrderBy(orderLambda).Skip((index - 1) * count).Take(count).ToList();
        //}


        public virtual IEnumerable<TEntity> GetAllFilter(Expression<Func<TEntity, bool>> where,Paging paging, ref int RowCount)
        {
            IQueryable<TEntity> query = _dbSet;
            if (where != null)
            {
                query = query.Where(where);
            }
            
            RowCount = query.Count();
            return query.OrderBy(paging.sortBy+ " "+ paging.direction ,false)
                            .Skip((paging.page- 1) * paging.limit)
                            .Take(paging.limit).ToList();
        }
        public virtual IEnumerable<TEntity> GetAll(ref int RowCount,Expression<Func<TEntity, bool>> where = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (where != null)
            {
                query = query.Where(where);
            }
            RowCount = query.Count();
            return query.ToList();
        }
        
        public virtual TEntity GetById(object Id)
        {
            return _dbSet.Find(Id);
        }
        public async Task<TEntity> GetByIdAsync(object Id)
        {
            return await  _dbSet.FindAsync(Id);
        }
        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public async Task  InsertAsync(TEntity entity)
        {
             _dbSet.Add(entity);
        }


        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            entities.Entry(entity).State = EntityState.Modified;
        }
        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (entities.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }
        public async Task DeleteAsync(TEntity entity)
        {
            if (entities.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }
        public virtual void Delete(object Id)
        {
            var entity = GetById(Id);
            Delete(entity);
        }
        public async Task DeleteAsync(object Id)
        {
            var entity =await GetByIdAsync(Id);
           await DeleteAsync(entity);
        }


        ///// <summary>
        ///// Get all the list
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //public IEnumerable<T> GetAll(Func<T, bool> predicate = null)
        //{
        //    if (predicate != null)
        //    {
        //        return _objectSet.Where(predicate);
        //    }

        //    return _objectSet.AsEnumerable();
        //}
        ///// <summary>
        ///// Get a certain element of the list
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //public T Get(Func<T, bool> predicate)
        //{
        //    return _objectSet.First(predicate);
        //}
        ///// <summary>
        ///// Add a new element to the list
        ///// </summary>
        ///// <param name="entity"></param>
        //public void Add(T entity)
        //{
        //    _objectSet.Add(entity);
        //}
        ///// <summary>
        ///// Update a certain element
        ///// </summary>
        ///// <param name="entity"></param>
        //public void Attach(T entity)
        //{
        //  //  _objectSet.AddOrUpdate(entity);
        //}
        ///// <summary>
        ///// Delete an element from the list
        ///// </summary>
        ///// <param name="entity"></param>
        //public void Delete(T entity)
        //{
        //    _objectSet.Remove(entity);
        //}
    }
}