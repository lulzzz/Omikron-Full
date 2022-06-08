using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Data;
using Omikron.SharedKernel.Infrastructure.Data.Model;
using Omikron.SharedKernel.Infrastructure.Tenants.Accessors;

namespace Omikron.SharedKernel.Extensions
{
    public static class TenantExtensions
    {
        public static IServiceCollection AddTenantStoreContext(this IServiceCollection services, IConfiguration configuration)
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<TenantStoreDbContext>()
                .UseSqlServer(connectionString: configuration.GetConnectionString(name: "TenantStoreDb"),
                    sqlServerOptionsAction: builder => builder.MigrationsAssembly(assemblyName: "Omikron.TenantService"));

            services
                .AddScoped(implementationFactory: provider => new TenantStoreDbContext(options: contextOptionsBuilder.Options, provider: provider))
                .AddSingleton<Func<TenantStoreDbContext>>(implementationFactory: provider => () => new TenantStoreDbContext(options: contextOptionsBuilder.Options, provider: provider));

            return services
                .AddTransient<ITenantAccessor, TenantAccessorByStaticStrategy>();
        }

        public static IApplicationBuilder ApplyTenantStoreDatabaseMigrations(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<TenantStoreDbContext>();
            context.Database.Migrate();

            return app;
        }

        public static Task<Tenant> FindTenantById(this IQueryable<Tenant> queryable, string id)
        {
            return queryable.FirstOrDefaultAsync(predicate: x =>
                x.Id == id || x.Identifier.ToLower() == id.ToLower());
        }
    }
}