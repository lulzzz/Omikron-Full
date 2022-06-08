using System;

namespace Omikron.SharedKernel.Orleans
{
    /// <summary>
    ///     A client interface for accessing to grains.
    /// </summary>
    public interface IGrainProvider
    {
        /// <summary>Gets a reference to a grain.</summary>
        /// <typeparam name="TGrain">The interface to get.</typeparam>
        /// <param name="key">The primary key of the grain.</param>
        /// <returns>A reference to the specified grain.</returns>
        TGrain GetGrain<TGrain>(string key) where TGrain : IBaseGrainWithStringKey;

        /// <summary>Gets a reference to a grain.</summary>
        /// <typeparam name="TGrain">The interface to get.</typeparam>
        /// <param name="key">The primary key of the grain.</param>
        /// <returns>A reference to the specified grain.</returns>
        TGrain GetGrain<TGrain>(Guid key) where TGrain : IBaseGrainWithGuidKey;
    }
}