using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Omikron.SharedKernel.Infrastructure.Data
{
    public class TenantStoreDbContextFactory : IDesignTimeDbContextFactory<TenantStoreDbContext>
    {
        private readonly IConfiguration _configuration;

        public TenantStoreDbContextFactory()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public TenantStoreDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TenantStoreDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TenantStoreDb"), builder => builder.MigrationsAssembly("Omikron.TenantService"));
            return new TenantStoreDbContext(optionsBuilder.Options, null);
        }
    }
}