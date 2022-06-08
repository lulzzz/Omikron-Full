using System.Collections.Generic;
using System.Reflection;
using Omikron.SharedKernel.ViewModels;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddMapper(this IServiceCollection services, Assembly[] assembly)
        {
            var assemblies = new List<Assembly> { typeof(BaseViewModel<>).Assembly };
            assemblies.AddRange(assembly);
            return services.AddAutoMapper(assemblies);
        }
    }
}