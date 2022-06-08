using System;
using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Omikron.SharedKernel.Infrastructure.Logging.Context;

namespace Omikron.SharedKernel.Infrastructure.Cache
{
    public class CombinedCacheManager : ICacheManager
    {
        private readonly string _cacheEndpoint;
        private readonly IEasyCachingProvider _cachingProvider;
        private readonly LoggerContext _loggerContext;
        private readonly string _prefixKey;
        private readonly bool _trackCacheCalls;

        public CombinedCacheManager(IEasyCachingProvider cachingProvider, IConfiguration configuration, LoggerContext loggerContext)
        {
            _cachingProvider = cachingProvider;
            _prefixKey = configuration.GetValue<string>(key: "Azure:Redis:KeyPrefix");
            _cacheEndpoint = configuration.GetValue<string>(key: "Azure:Redis:Endpoint");
            _trackCacheCalls = configuration.GetValue(key: "Azure:Redis:TrackCacheCalls", defaultValue: true);
            _loggerContext = loggerContext;
        }

        public void Set<TObject>(string key, TObject @object)
        {
            TrackAction(cacheAction: () => _cachingProvider.Set(cacheKey: GetComputedKey(key: key), cacheValue: @object, expiration: CacheExpirationTime.DefaultExpiration), cacheType: "Set", key: key);
        }

        public void Set<TObject>(string key, TObject @object, CacheExpirationTime expired)
        {
            if (expired == null)
            {
                throw new ArgumentNullException(paramName: nameof(expired));
            }

            TrackAction(cacheAction: () => _cachingProvider.Set(cacheKey: GetComputedKey(key: key), cacheValue: @object, expiration: expired), cacheType: "Set", key: key);
        }

        public TObject Get<TObject>(string key)
        {
            Func<TObject> cacheAction = () => _cachingProvider.Get<TObject>(cacheKey: GetComputedKey(key: key)).Value;

            return TrackActionAndReturn(cacheAction: cacheAction, cacheType: "Get", key: key);
        }

        public void Remove(string key)
        {
            TrackAction(cacheAction: () => _cachingProvider.Remove(cacheKey: GetComputedKey(key: key)), cacheType: "Remove", key: key);
        }

        public bool Exists(string key)
        {
            return TrackActionAndReturn(cacheAction: () => _cachingProvider.Exists(cacheKey: GetComputedKey(key: key)), cacheType: "Exists", key: key);
        }

        private string GetComputedKey(string key)
        {
            return $"{_prefixKey}.{key}";
        }

        private TReturn TrackActionAndReturn<TReturn>(Func<TReturn> cacheAction, string cacheType, string key)
        {
            if (!_trackCacheCalls)
            {
                return cacheAction();
            }

            return _loggerContext.PerformanceLogger.TrackDependency(operation: cacheAction, dependencyType: "Redis", dependencyName: $"{cacheType} {_cacheEndpoint}", dependencyCommand: $"{cacheType} {key}");
        }

        private void TrackAction(Action cacheAction, string cacheType, string key)
        {
            if (!_trackCacheCalls)
            {
                cacheAction();
            }
            else
            {
                _loggerContext.PerformanceLogger.TrackDependency(operation: cacheAction, dependencyType: "Redis", dependencyName: $"{cacheType} {_cacheEndpoint}", dependencyCommand: $"{cacheType} {key}");
            }
        }
    }
}