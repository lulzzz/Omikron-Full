using System;
using System.IO;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Omikron.IdentityService.Infrastructure
{
    public class IdentityConfigurationDbContextFactory : IDesignTimeDbContextFactory<IdentityConfigurationDbContext>
    {
        private readonly IConfiguration _configuration;

        public IdentityConfigurationDbContextFactory()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IdentityConfigurationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("IdentityServerConfigurationDb"));
            return new IdentityConfigurationDbContext(optionsBuilder.Options, new ConfigurationStoreOptions());
        }
    }
}