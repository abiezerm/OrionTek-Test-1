using ClientDirectory.Common.Services.Contracts;
using ClientDirectory.Core;
using ClientDirectory.Core.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ClientDirectory.DataAccess
{
    public class ClientDirectoryContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public ClientDirectoryContext(DbContextOptions<ClientDirectoryContext> options,
                                        ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }

        #region Save Changes
        public override int SaveChanges()
        {
            var auditableEntitySet = ChangeTracker.Entries<IAuditableEntity>();

            if (auditableEntitySet != null)
            {
                foreach (var auditableEntity in auditableEntitySet.Where(c => c.State == EntityState.Added || c.State == EntityState.Modified))
                {
                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.CreatedAt = DateTime.Now;
                        auditableEntity.Entity.CreatedBy = _currentUserService.UserId ?? "";
                    }

                    if (auditableEntity.State == EntityState.Modified)
                    {
                        auditableEntity.Entity.UpdatedAt = DateTime.Now;
                        auditableEntity.Entity.UpdatedBy = _currentUserService.UserId ?? "";
                    }
                }
            }

            return base.SaveChanges();
        }

        #endregion
    }
}
