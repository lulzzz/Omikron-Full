using Omikron.SharedKernel.Infrastructure.System;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Extensions
{
    public static class SystemInformationExtensions
    {
        public static IServiceCollection AddSystemInformationProvider(this IServiceCollection services)
        {
            services.AddScoped<ISystemInformationProvider, AafSystemInformationProvider>();
            return services;
        }
    }
}