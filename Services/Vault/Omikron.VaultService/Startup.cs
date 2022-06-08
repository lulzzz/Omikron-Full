using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Omikron.SharedKernel;
using Omikron.SharedKernel.Convertors;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Json;
using Omikron.VaultService.Converters;
using Omikron.VaultService.Extensions;

namespace Omikron.VaultService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var assemblies = new[] { Assembly.GetExecutingAssembly(), typeof(SharedKernelAssembly).Assembly };

            services.AddLogger(configuration: Configuration);

            services
                .AddVaultServiceDatabaseContext(configuration: Configuration);

            services
                .AddDataRepository(assemblies: assemblies)
                .AddControllers()
                .AddJson(new AuthorizationStatusConverter(), new DateTimeConverter(), new AssetTypeConverter())
                .AddFluentValidation(configurationExpression: fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services
                .AddApiVersioning()
                .AddVersionedApiExplorer(setupAction: o => o.GroupNameFormat = "'v'VVV")
                .AddSwagger(serviceName: Constants.ServiceName, serviceDescription: Constants.ServiceDescription);

            services
                .AddAuthenticationWithJwtBearer(configuration: Configuration);

            services
                .AddAzureStorageProvider(configuration: Configuration)
                .AddCommonFeature(configuration: Configuration)
                .AddDomainEvents(assemblies: assemblies)
                .AddHttpTokenService(configuration: Configuration)
                .AddHttpIdentityServiceClient(configuration: Configuration)
                .AddCombinedCacheManager(configuration: Configuration)
                .AddMapper(assembly: assemblies)
                .AddCommandHandlers(assemblies: assemblies, configuration: Configuration)
                .AddAzureKeyVault()
                .AddBudApiClient(configuration: Configuration)
                .RegisterProviderIconConfiguration(Configuration);

            services
                .AddThirdPartyApiServiceConfigurations(configuration: Configuration)
                .AddServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandling();
            app.UseCommonFeature();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(configure: endpoints => endpoints.MapControllers());
            app.UseSwaggerWithUi(versionDescriptionProvider: versionDescriptionProvider);
            app.NotifyServiceSuccessfullyStarted(serviceName: Constants.ServiceName);
            app.ApplyVaultServiceDatabaseMigrations(applyMigration: true);
        }
    }
}