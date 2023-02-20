using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialNetwork.Data.Context;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.UnitOfWork.Abstractions;

namespace SocialNetwork.Data.UnitOfWork.Implementations
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        protected SocialNetworkContext _context { get; set; }
        public BaseUnitOfWork(SocialNetworkContext context)
        {
            _context = context;
        }

        public virtual async Task<int> SaveChanges()
        {
            DateTime now = DateTime.Now;

            if (_context.ChangeTracker.HasChanges())
            {
                IEnumerable<EntityEntry> entries = _context.ChangeTracker
                    .Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                    .ToList();

                foreach (EntityEntry entry in entries)
                {
                    BaseEntity? entity = entry.Entity as BaseEntity;

                    if (entity != null)
                    {
                        entity.UpdatedOn = now;

                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedOn = entity.UpdatedOn;
                        }
                    }
                }
            }

            return await _context.SaveChangesAsync();
        }

        public virtual void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public virtual void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public virtual void Commit()
        {
            _context.Database.CommitTransaction();
        }

    }
}
