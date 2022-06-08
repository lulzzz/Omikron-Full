using System.Reflection;
using Coravel;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Omikron.SharedKernel.Extensions;
using Omikron.Sync.Schedule.Service.Extensions;
using Omikron.Sync.Schedule.Service.Workers;

namespace Omikron.Sync.Schedule.Service
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
            var assemblies = new[] { Assembly.GetExecutingAssembly() };


            services.AddLogger(configuration: Configuration);

            services
                .AddDataRepository(assemblies: assemblies)
                .AddControllers()
                .AddFluentValidation(configurationExpression: fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services
                .AddApiVersioning()
                .AddVersionedApiExplorer(setupAction: o => o.GroupNameFormat = "'v'VVV")
                .AddSwagger(serviceName: Constants.ServiceName, serviceDescription: Constants.ServiceDescription);

            services
                .AddAuthenticationWithJwtBearer(configuration: Configuration);

            services
                .AddCommonFeature(configuration: Configuration)
                .AddDomainEvents(assemblies: assemblies)
                .AddHttpTokenService(configuration: Configuration)
                .AddHttpIdentityServiceClient(configuration: Configuration)
                .AddMapper(assembly: assemblies)
                .AddCommandHandlers(assemblies: assemblies, configuration: Configuration);

            services
                .AddTransient<OrchestrateSyncStartWorker>()
                .AddTransient<OrchestrateVehicleSyncStartWorker>()
                .AddTransient<OrchestratePropertySyncStartWorker>()
                .AddScheduler();
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
            app.UseOrchestrateSyncWorker();
            app.UseOrchestrateVehicleSyncWorker();
            app.UseOrchestratePropertySyncWorker();
            app.UseEndpoints(configure: endpoints => endpoints.MapControllers());
            app.UseSwaggerWithUi(versionDescriptionProvider: versionDescriptionProvider);
            app.NotifyServiceSuccessfullyStarted(serviceName: Constants.ServiceName);
        }
    }
}