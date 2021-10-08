using ClientDirectory.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace ClientDirectory.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ClientDirectoryContext _DataContext;

        public UnitOfWork(ClientDirectoryContext dataContext)
        {
            _DataContext = dataContext;
        }

        public IDbContextTransaction CreateTransaction()
        {
            return this._DataContext.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return _DataContext.SaveChanges();
        }

        public void Dispose()
        {
            if (_DataContext != null)
            {
                _DataContext.Dispose();
            }
        }
    }
}
