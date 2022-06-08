using Microsoft.EntityFrameworkCore;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Utils;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data
{
    public class VaultServiceDatabaseContext : DbContext
    {
        public VaultServiceDatabaseContext(DbContextOptions<VaultServiceDatabaseContext> options) : base(options: options)
        {
        }

        public string DefaultSchema { get; } = "vault";

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountBalance> AccountBalances { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleValue> VehicleValues { get; set; }
        public DbSet<PersonalItem> PersonalItems { get; set; }
        public DbSet<PersonalItemValue> PersonalItemValues { get; set; }
        public DbSet<VaultItem> VaultItems { get; set; }
        public DbSet<RefreshHistory> RefreshHistories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<InvestmentValue> InvestmentValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: typeof(SharedKernelAssembly).Assembly);

            modelBuilder.HasDefaultSchema(schema: DefaultSchema);
            base.OnModelCreating(modelBuilder: modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateModifiedAt();
            return base.SaveChangesAsync(cancellationToken: cancellationToken);
        }

        private void UpdateModifiedAt()
        {
            ChangeTracker
                .Entries()
                .Where(predicate: e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified))
                .Select(selector: entry => entry.Entity as BaseEntity)
                .Where(predicate: e => e != null)
                .ForEach(action: e => e.ModifiedAt = Clock.GetTime());
        }
    }
}