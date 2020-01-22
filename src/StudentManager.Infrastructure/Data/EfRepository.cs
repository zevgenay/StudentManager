using StudentManager.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StudentManager.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _appDbContext;

        public EfRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            _appDbContext.SaveChanges();
        }

        public virtual T GetById(int id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> List()
        {
            return _appDbContext.Set<T>().AsEnumerable();            
        }

        public virtual IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>()
                    .Where(predicate)
                    .AsEnumerable();
        }

        public void Update(T entity)
        {
            _appDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Add(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            _appDbContext.SaveChanges();
        }
    }
}
