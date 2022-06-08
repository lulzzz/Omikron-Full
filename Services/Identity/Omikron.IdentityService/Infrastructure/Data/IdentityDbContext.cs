using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Omikron.IdentityService.Infrastructure.Data.Configuration;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.Infrastructure.Data
{
    public sealed class OmikronIdentityDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public OmikronIdentityDbContext(DbContextOptions<OmikronIdentityDbContext> options) : base(options: options)
        {
        }

        /// <summary>
        ///     DefaultSchema: 'usm' (User Management)
        /// </summary>
        public string DefaultSchema { get; } = "usm";

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<ConfirmationToken> ConfirmationTokens { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(configuration: new RoleEntityTypeConfiguration());
            builder.ApplyConfiguration(configuration: new UserEntityTypeConfiguration());
            builder.ApplyConfiguration(configuration: new PermissionEntityTypeConfiguration());
            builder.ApplyConfiguration(configuration: new PhoneNumberEntityTypeConfiguration());
            builder.ApplyConfiguration(configuration: new RolePermissionEntityTypeConfiguration());

            builder.HasDefaultSchema(schema: DefaultSchema);
            base.OnModelCreating(builder: builder);
        }
    }
}