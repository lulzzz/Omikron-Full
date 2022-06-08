using System;
using EasyCaching.Core.Configurations;
using EasyCaching.Serialization.SystemTextJson.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Infrastructure.Cache;

namespace Omikron.SharedKernel.Extensions
{
    public static class CacheExtensions
    {
        private const string DistributedCacheKey = "distributed-cache";

        public static IServiceCollection AddNullCacheManager(this IServiceCollection services)
        {
            return services.AddSingleton<ICacheManager, NullCacheManager>();
        }

        public static IServiceCollection AddCombinedCacheManager(this IServiceCollection services, IConfiguration configuration)
        {
            var password = configuration.GetValue<string>(key: "Azure:Redis:Password") ??
                           throw new ArgumentNullException(paramName: "Azure:Redis:Password");
            var endpoint = configuration.GetValue<string>(key: "Azure:Redis:Endpoint") ??
                           throw new ArgumentNullException(paramName: "Azure:Redis:Endpoint");

            var keyPrefix = configuration.GetValue<string>(key: "Azure:Redis:KeyPrefix") ??
                            throw new ArgumentNullException(paramName: "Azure:Redis:KeyPrefix");

            var segments = endpoint.Split(separator: ":");
            var serverEndPoint = new ServerEndPoint(host: segments[0], port: int.Parse(s: segments[1]));

            services.AddEasyCaching(setupAction: options =>
                options
                    .UseRedis(configure: config =>
                    {
                        config.DBConfig.Endpoints.Add(item: serverEndPoint);
                        config.DBConfig.IsSsl = !keyPrefix.Equals(value: "local");
                        config.DBConfig.Password = password;
                        config.SerializerName = "json";
                        config.MaxRdSecond = 0;
                        config.EnableLogging = true;
                    }, name: DistributedCacheKey)
                    .WithSystemTextJson());

            services.AddSingleton<ICacheManager, CombinedCacheManager>();
            return services;
        }
    }
}