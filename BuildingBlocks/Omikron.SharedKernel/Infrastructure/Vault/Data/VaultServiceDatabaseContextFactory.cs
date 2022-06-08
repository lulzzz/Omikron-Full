using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data
{
    public class VaultServiceDatabaseContextFactory : IDesignTimeDbContextFactory<VaultServiceDatabaseContext>
    {
        private readonly IConfiguration _configuration;

        public VaultServiceDatabaseContextFactory()
        {
            var environment = Environment.GetEnvironmentVariable(variable: "ASPNETCORE_ENVIRONMENT");
            _configuration = new ConfigurationBuilder()
                .SetBasePath(basePath: Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(path: $"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public VaultServiceDatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VaultServiceDatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString: _configuration.GetConnectionString(name: "VaultServiceDatabase")); //only for design time
            return new VaultServiceDatabaseContext(options: optionsBuilder.Options);
        }
    }
}