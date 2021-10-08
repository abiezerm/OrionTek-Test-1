using ClientDirectory.Common.Services.Contracts;
using ClientDirectory.Core.Base;
using ClientDirectory.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ClientDirectory.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IAuditableEntity
    {
        private readonly ClientDirectoryContext context;
        private readonly DbSet<T> db;
        private readonly ICurrentUserService _currentUserService;

        public BaseRepository(ClientDirectoryContext _context, 
                              ICurrentUserService currentUserService = null)
        {
            context = _context;
            db = _context.Set<T>();
            _currentUserService = currentUserService;
        }

        public T Create(T entity)
        {
            db.Add(entity);

            context.SaveChanges();

            return entity;
        }

        public bool Exists(Expression<Func<T, bool>> filter = null)
        {
            return db.Any(filter);
        }

        public T Get(int id)
        {
            return db.Find(id);
        }

        public T Get(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null)
        {
            var query = filter == null ? db : db.Where(filter);

            var results = transform(query);

            return results.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return db.Where(x => !x.IsDeleted).ToList();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null)
        {
            var query = filter == null ? db.AsNoTracking() : db.AsNoTracking().Where(filter);

            var results = transform(query);

            return results.ToArray().ToList();
        }

        public T Update(T entity)
        {
            db.Update(entity);

            context.SaveChanges();

            return entity;
        }

        public T Delete(T entity)
        {
            entity.DeletedAt = DateTime.Now;
            entity.DeletedBy = _currentUserService.UserId;
            entity.IsDeleted = true;

            context.Update(entity);

            context.SaveChanges();

            return entity;
        }
    }
}
