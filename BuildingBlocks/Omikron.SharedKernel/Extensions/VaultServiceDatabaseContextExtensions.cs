using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Infrastructure.Vault.Data;

namespace Omikron.SharedKernel.Extensions
{
    public static class VaultServiceDatabaseContextExtensions
    {
        public static IServiceCollection AddVaultServiceDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<VaultServiceDatabaseContext>(optionsAction: builder => builder
                    .UseSqlServer(connectionString: configuration.GetConnectionString(name: "VaultServiceDatabase")))
                .AddScoped<Func<VaultServiceDatabaseContext>>(implementationFactory: provider => provider.GetService<VaultServiceDatabaseContext>);
        }

        public static IApplicationBuilder ApplyVaultServiceDatabaseMigrations(this IApplicationBuilder app, bool applyMigration = false)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<VaultServiceDatabaseContext>();

            if (applyMigration)
            {
                context.Database.Migrate();
            }

            return app;
        }
    }
}