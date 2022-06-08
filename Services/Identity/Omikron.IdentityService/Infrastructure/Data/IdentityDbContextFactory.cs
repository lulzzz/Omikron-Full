using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Omikron.IdentityService.Infrastructure.Data;

namespace Omikron.IdentityService.Infrastructure
{
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<OmikronIdentityDbContext>
    {
        private readonly IConfiguration _configuration;

        public IdentityDbContextFactory()
        {
            var environment = Environment.GetEnvironmentVariable(variable: "ASPNETCORE_ENVIRONMENT");
            _configuration = new ConfigurationBuilder()
                .SetBasePath(basePath: Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(path: $"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public OmikronIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OmikronIdentityDbContext>();
            optionsBuilder.UseSqlServer(connectionString: _configuration.GetConnectionString(name: "IdentityServiceDatabase")); //only for design time
            return new OmikronIdentityDbContext(options: optionsBuilder.Options);
        }
    }
}