using System;
using System.Linq;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Infrastructure.Logging.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using var scope = context.RequestServices.CreateScope();

            var serviceProvider = scope.ServiceProvider;
            var logger = serviceProvider.GetService<LoggerContext>();
            var key = context.Request.Headers.Keys.FirstOrDefault(n =>
                n.Equals(LoggerConstants.CorrelationIdHeaderKey, StringComparison.CurrentCultureIgnoreCase));

            var correlationId = !string.IsNullOrWhiteSpace(key)
                ? context.Request.Headers[key].ToString()
                : Guid.NewGuid().ToString();

            var parentKey = context.Request.Headers.Keys.FirstOrDefault(n =>
               n.Equals(LoggerConstants.ParentIdHeaderKey, StringComparison.CurrentCultureIgnoreCase));

            var parentId = !string.IsNullOrWhiteSpace(parentKey)
                ? context.Request.Headers[parentKey].ToString()
                : string.Empty;

            context.Response.Headers.Append(LoggerConstants.CorrelationIdHeaderKey, correlationId);
            
            context.Items.Add(LoggerConstants.CorrelationIdHeaderKey, correlationId);
            
            logger.CorrelationId = correlationId;
            
            logger.ParentId = correlationId;
            
            await _next.Invoke(context);
        }
    }
}