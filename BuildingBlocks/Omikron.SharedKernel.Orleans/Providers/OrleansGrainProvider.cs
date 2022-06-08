using System;
using Orleans;

namespace Omikron.SharedKernel.Orleans.Providers
{
    public sealed class OrleansGrainProvider : IGrainProvider
    {
        private readonly IClusterClient _clusterClient;

        public OrleansGrainProvider(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        /// <summary>Gets a reference to a grain.</summary>
        /// <typeparam name="TGrain">The interface to get.</typeparam>
        /// <param name="key">The primary key of the grain.</param>
        /// <returns>A reference to the specified grain.</returns>
        public TGrain GetGrain<TGrain>(string key) where TGrain : IBaseGrainWithStringKey
        {
            return _clusterClient.GetGrain<TGrain>(primaryKey: key);
        }

        /// <summary>Gets a reference to a grain.</summary>
        /// <typeparam name="TGrain">The interface to get.</typeparam>
        /// <param name="key">The primary key of the grain.</param>
        /// <returns>A reference to the specified grain.</returns>
        public TGrain GetGrain<TGrain>(Guid key) where TGrain : IBaseGrainWithGuidKey
        {
            return _clusterClient.GetGrain<TGrain>(primaryKey: key);
        }
    }
}