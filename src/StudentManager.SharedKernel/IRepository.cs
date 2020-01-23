using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StudentManager.SharedKernel
{
    public interface IRepository<T> where T : BaseEntity
    {
        #region Get count entities
        int Count();
        int Count(Expression<Func<T, bool>> where);
        #endregion

        #region Create entity
        void Add(T entity);
        T Create(T entity);
        #endregion

        #region Update entity
        void Update(T entity);
        #endregion

        #region Delete entity
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        #endregion

        #region Get entity
        T GetById(int id);
        T Get(Expression<Func<T, bool>> where);
        #endregion

        #region Get entities
        List<T> GetAll();
        List<T> GetMany(Expression<Func<T, bool>> where);
        #endregion

        #region Get entities per page
        List<T> PerPage(Expression<Func<T, object>> orderBy, bool orderDesc = true, int page = 1, int onPage = 10);
        List<T> PerPage(Expression<Func<T, object>> orderBy, bool orderDesc = true, int page = 1, int onPage = 10, params Expression<Func<T, object>>[] includeProperties);
        List<T> PerPage(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy, bool orderDesc = true, int page = 1, int onPage = 10);
        List<T> PerPage(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy, bool orderDesc = true, int page = 1, int onPage = 10, params Expression<Func<T, object>>[] includeProperties);
        #endregion

        #region Gets entities with Include
        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetWithInclude(Func<T, bool> where, params Expression<Func<T, object>>[] includeProperties);
        #endregion
    }
}
