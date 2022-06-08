using System.Text;
using System.Text.Json;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Extensions
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder NotifyServiceSuccessfullyStarted(this IApplicationBuilder app, string serviceName)
        {
            var loggerContext = app.ApplicationServices.GetService<LoggerContext>();
            loggerContext.UsageLogger.Information($"The service '{serviceName}' has been successfully started.");
            return app;
        }

        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                var logger = app.ApplicationServices.GetService<LoggerContext>();
                builder.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;
                        logger.ErrorLogger.Error(ex, ex.Message);
                        await context.Response.WriteAsync(
                            JsonSerializer.Serialize(new { ex.Message, StatusCode = 500 }), Encoding.UTF8);
                    }
                });
            });

            return app;
        }
    }
}