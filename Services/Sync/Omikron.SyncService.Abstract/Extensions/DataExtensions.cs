using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omikron.Sync.Infrastructure.ReadOnlyOmikronIdentityDatabase;

namespace Omikron.Sync.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddReadOnlyOmikronIdentityDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection
                .AddDbContext<ReadOnlyOmikronIdentityDbContext>(optionsAction: builder => builder
                    .UseSqlServer(connectionString: configuration.GetConnectionString(name: "IdentityServiceDatabase")));
        }
    }
}