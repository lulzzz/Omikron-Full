using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Omikron.SharedKernel.Extensions;

namespace Omikron.SharedKernel.Security
{
    public class HttpContextSecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;

        public HttpContextSecurityHeadersMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _environment = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            context.Response.Headers.Add("Permissions-Policy", "camera=(), geolocation=(), microphone=(), usb=()");

            if (!_environment.IsLocal())
            {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self' data:; report-uri /idgreport");
            }

            await _next(context);
        }
    }
}