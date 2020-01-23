using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StudentManager.Infrastructure.Data
{
    public abstract class BaseRepository<T> : Disposable where T : class
    {
        public AppDbContext _appDbContext { get; private set; }
        private DbSet<T> _dbSet;

        protected BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }

        #region Get count entities
        public virtual int Count()
        {
            return _dbSet.Count();
        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).Count();
        }
        #endregion

        #region Create entity
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
            _appDbContext.SaveChanges();
        }

        public virtual T Create(T entity)
        {
            entity = _dbSet.Add(entity).Entity;
            _appDbContext.SaveChanges();

            return entity;
        }
        #endregion

        #region Update entity
        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _appDbContext.Entry(entity).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }
        #endregion

        #region Delete entity
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                _dbSet.Remove(obj);
            }
            _appDbContext.SaveChanges();
        }
        #endregion

        #region Get entity
        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault<T>();
        }
        #endregion

        #region Get entities
        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual List<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }
        #endregion

        #region Get entities per page
        public List<T> PerPage(Expression<Func<T, object>> orderBy, bool orderDesc = true, int page = 1, int onPage = 10)
        {
            int skip = 0;
            skip = onPage * (page - 1);

            return orderDesc ?

                _dbSet.AsNoTracking().OrderByDescending(orderBy)
                .Skip(skip)
                .Take(onPage)
                .ToList() :

                _dbSet.AsNoTracking().OrderBy(orderBy)
                .Skip(skip)
                .Take(onPage)
                .ToList();
        }

        public List<T> PerPage(Expression<Func<T, object>> orderBy, bool orderDesc = true, int page = 1, int onPage = 10, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            int skip = 0;

            skip = onPage * (page - 1);

            return orderDesc ?

                query.OrderByDescending(orderBy)
                .Skip(skip)
                .Take(onPage)
                .ToList() :

                query.OrderBy(orderBy)
                .Skip(skip)
                .Take(onPage)
                .ToList();
        }

        public List<T> PerPage(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy, bool orderDesc = true, int page = 1, int onPage = 10, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            int skip = 0;

            skip = onPage * (page - 1);

            return orderDesc ?

                query.Where(where).OrderByDescending(orderBy)
                .Skip(skip)
                .Take(onPage)
                .ToList() :

                query.Where(where).OrderBy(orderBy)
                .Skip(skip)
                .Take(onPage)
                .ToList();
        }

        public List<T> PerPage(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy, bool orderDesc = true, int page = 1, int onPage = 10)
        {
            int skip = 0;
            skip = onPage * (page - 1);

            return orderDesc ?

                _dbSet.AsNoTracking()
                .Where(where).OrderByDescending(orderBy)
                .Skip(skip)
                .Take(onPage)
                .ToList() :

                _dbSet.AsNoTracking()
                .Where(where).OrderBy(orderBy)
                .Skip(skip)
                .Take(onPage)
                .ToList();
        }
        #endregion

        #region Gets entities with Include
        public IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }
        #endregion

        #region Helper
        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        #endregion

        protected override void DisposeCore()
        {
            if (_appDbContext != null)
            {
                _appDbContext.Dispose();
            }
        }
    }

    public class Disposable : IDisposable
    {
        private bool isDisposed;

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }

            isDisposed = true;
        }

        // Override this to dispose custom objects
        protected virtual void DisposeCore() { }
    }
}
