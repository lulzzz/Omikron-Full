using System;
using System.IO;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Omikron.IdentityService.Infrastructure
{
    public class IdentityPersistedGrantDbContextFactory : IDesignTimeDbContextFactory<IdentityPersistedGrantDbContext>
    {
        private readonly IConfiguration _configuration;

        public IdentityPersistedGrantDbContextFactory()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IdentityPersistedGrantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("IdentityServerConfigurationDb"));
            return new IdentityPersistedGrantDbContext(optionsBuilder.Options, new OperationalStoreOptions());
        }
    }
}