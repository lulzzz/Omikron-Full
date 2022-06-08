using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Omikron.Sync.Infrastructure.ReadOnlyOmikronIdentityDatabase.Configuration;
using Omikron.Sync.Model;

namespace Omikron.Sync.Infrastructure.ReadOnlyOmikronIdentityDatabase
{
    public sealed class ReadOnlyOmikronIdentityDbContext : DbContext
    {
        public ReadOnlyOmikronIdentityDbContext(DbContextOptions<ReadOnlyOmikronIdentityDbContext> options) : base(options: options)
        {
        }

        /// <summary>
        ///     DefaultSchema: 'usm' (User Management)
        /// </summary>
        public string DefaultSchema { get; } = "usm";

        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            throw NotSupportedException();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw NotSupportedException();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw NotSupportedException();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            throw NotSupportedException();
        }

        private static NotSupportedException NotSupportedException()
        {
            return new NotSupportedException(message: "This is read only context. The write operation are not supported.");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(configuration: new UserEntityTypeConfiguration());
            builder.HasDefaultSchema(schema: DefaultSchema);
            base.OnModelCreating(modelBuilder: builder);
        }
    }
}