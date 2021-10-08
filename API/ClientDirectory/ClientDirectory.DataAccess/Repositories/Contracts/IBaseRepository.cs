using ClientDirectory.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ClientDirectory.DataAccess.Repositories.Contracts
{
    public interface IBaseRepository<T> where T : class, IAuditableEntity
    {
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
        T Get(int id);
        T Get(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null);
        bool Exists(Expression<Func<T, bool>> filter = null);
    }
}
