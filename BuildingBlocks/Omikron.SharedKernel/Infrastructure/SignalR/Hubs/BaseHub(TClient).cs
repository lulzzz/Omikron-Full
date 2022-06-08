using System;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Microsoft.AspNetCore.SignalR;

namespace Omikron.SharedKernel.Infrastructure.SignalR.Hubs
{
    /// <summary>
    ///     Represent the base class for a strongly typed SignalR hub.
    /// </summary>
    /// <typeparam name="TClient">The type of client.</typeparam>
    public abstract class BaseHub<TClient> : Hub<TClient> where TClient : class
    {
        protected readonly LoggerContext Logger;
        protected readonly IServiceProvider ServiceProvider;

        protected BaseHub(LoggerContext logger, IServiceProvider serviceProvider)
        {
            Logger = logger;
            ServiceProvider = serviceProvider;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Logger.UsageLogger.Information($"The new connection '{Context.ConnectionId}' has been accepted on Hub '{GetType().FullName}'.");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            Logger.UsageLogger.Information($"The connection '{Context.ConnectionId}' has been terminated on Hub '{GetType().FullName}'.");
        }
    }
}