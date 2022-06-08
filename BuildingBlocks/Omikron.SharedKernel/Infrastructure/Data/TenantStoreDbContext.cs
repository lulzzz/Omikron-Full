using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Data.Configurations;
using Omikron.SharedKernel.Infrastructure.Data.Model;

namespace Omikron.SharedKernel.Infrastructure.Data
{
    public class TenantStoreDbContext : DbContext
    {
        private readonly IServiceProvider _provider;

        public TenantStoreDbContext(DbContextOptions<TenantStoreDbContext> options, IServiceProvider provider) : base(options: options)
        {
            _provider = provider;
        }

        public DbSet<Tenant> TenantInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(configuration: new TenantTypeConfiguration());
            base.OnModelCreating(modelBuilder: modelBuilder);
        }

        public override int SaveChanges()
        {
            var entityEntries = ChangeTracker.Entries<IHasDomainEvents>().ToList();
            var rows = base.SaveChanges();
            DispatchDomainEvents(entityEntries: entityEntries);
            return rows;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var entityEntries = ChangeTracker.Entries<IHasDomainEvents>().ToList();
            var rows = base.SaveChanges(acceptAllChangesOnSuccess: acceptAllChangesOnSuccess);
            DispatchDomainEvents(entityEntries: entityEntries);
            return rows;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entityEntries = ChangeTracker.Entries<IHasDomainEvents>().ToList();
            var rows = await base.SaveChangesAsync(cancellationToken: cancellationToken);
            DispatchDomainEvents(entityEntries: entityEntries);
            return rows;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var entityEntries = ChangeTracker.Entries<IHasDomainEvents>().ToList();
            var rows = await base.SaveChangesAsync(acceptAllChangesOnSuccess: acceptAllChangesOnSuccess, cancellationToken: cancellationToken);
            DispatchDomainEvents(entityEntries: entityEntries);
            return rows;
        }

        private void DispatchDomainEvents(IEnumerable<EntityEntry<IHasDomainEvents>> entityEntries)
        {
            foreach (var entry in entityEntries)
            {
                DomainEvents.Raise(events: entry.Entity, serviceProvider: _provider);
                entry.Entity.ClearDomainEvents();
            }
        }
    }
}