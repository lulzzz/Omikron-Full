using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Infrastructure.Vault.Services;

namespace Omikron.SharedKernel.Extensions
{
	public static class ServicesExtensions
	{
		public static IServiceCollection AddServicesAsSelf(this IServiceCollection services, string marker, ServiceLifetime lifetime, Assembly[] assemblies)
		{
			var assemblyServices = assemblies
				.SelectMany(assembly => assembly.GetTypes().Where(type => type.GetInterfaces().Any(i => i.Name.Contains(marker))))
				.Select(t => t).ToList();

			foreach (var service in assemblyServices)
			{
				switch (lifetime)
				{
					case ServiceLifetime.Singleton:
						services.AddSingleton(service); continue;
					case ServiceLifetime.Scoped:
						services.AddScoped(service); continue;
					case ServiceLifetime.Transient:
						services.AddTransient(service); continue;
				}
			}

			return services;
		}

		public static IServiceCollection AddServicesAsImplementedInterface(this IServiceCollection services, string marker, ServiceLifetime lifetime, Assembly[] assemblies, Func<Type, bool> filter = null)
		{
			if (filter == null)
			{
				filter = type => type.Name.Contains(marker);
			}

			var assemblyServices = assemblies
				.SelectMany(assembly => assembly.GetTypes().Where(type => type.GetInterfaces().Any(filter)))
				.Select(t => new Tuple<Type, Type>(t, t.GetInterfaces().FirstOrDefault(filter)));

			foreach (var (assemblyServiceAbstraction, assemblyServicesImplementation) in assemblyServices)
			{
				switch (lifetime)
				{
					case ServiceLifetime.Singleton:
						services.AddSingleton(assemblyServicesImplementation, assemblyServiceAbstraction); continue;
					case ServiceLifetime.Scoped:
						services.AddScoped(assemblyServicesImplementation, assemblyServiceAbstraction); continue;
					case ServiceLifetime.Transient:
						services.AddTransient(assemblyServicesImplementation, assemblyServiceAbstraction); continue;
				}
			}

			return services;
		}

		public static IServiceCollection AddAccountService(this IServiceCollection services)
		{
			return services.AddScoped<IAccountService, AccountService>();
		}
	}
}