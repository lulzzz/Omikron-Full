using Microsoft.Extensions.Hosting;

namespace Omikron.SharedKernel.Extensions
{
    public static class EnvironmentExtensions
    {
        public static bool IsLocal(this IHostEnvironment environment)
        {
            return environment.EnvironmentName.Equals("Local");
        }
    }
}
