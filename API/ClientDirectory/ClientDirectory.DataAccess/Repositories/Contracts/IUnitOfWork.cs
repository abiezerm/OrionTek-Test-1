using Microsoft.EntityFrameworkCore.Storage;

namespace ClientDirectory.DataAccess.Repositories.Contracts
{
    interface IUnitOfWork
    {
        IDbContextTransaction CreateTransaction();
        int SaveChanges();
    }
}
