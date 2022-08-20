using Aggregetter.Aggre.Domain.Common;
using Aggregetter.Aggre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Internal;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Persistence
{
    public sealed class AggreDbContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;
        private readonly ISystemClock _systemClock;

        public AggreDbContext(DbContextOptions<AggreDbContext> options, ISystemClock systemClock)
           : base(options)
        {
            _systemClock = systemClock;
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDateUtc = _systemClock.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDateUtc = _systemClock.UtcNow;
                        break;
                }
            }
            
            return base.SaveChangesAsync(cancellationToken);
        }

        public void BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            if (!Database.IsInMemory())
            {
                _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
            }            
        }

        public void CommitTransaction()
        {
            try
            {
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
