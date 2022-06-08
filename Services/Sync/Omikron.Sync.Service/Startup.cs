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
using Omikron.Sync.Bud;
using Omikron.Sync.Bud.Extensions;
using Omikron.Sync.Extensions;
using Omikron.Sync.Service.Extensions;

namespace Omikron.Sync.Service
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
            var assemblies = new[] { Assembly.GetExecutingAssembly(), typeof(BudSyncAgent).Assembly, typeof(SharedKernelAssembly).Assembly };


            services.AddLogger(configuration: Configuration);

            services
                .AddVaultServiceDatabaseContext(configuration: Configuration);

            services
                .AddDataRepository(assemblies: assemblies)
                .AddControllers()
                .AddJson(new DateTimeConverter())
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
                .AddHttpVaultServiceClient(configuration: Configuration)
                .AddCombinedCacheManager(configuration: Configuration)
                .AddMapper(assembly: assemblies)
                .AddCommandHandlers(assemblies: assemblies, configuration: Configuration)
                .AddBudApiClient(configuration: Configuration)
                .AddActorAsOrleans()
                .AddAccountService()
                .AddBudSync()
                .AddReadOnlyOmikronIdentityDbContext(configuration: Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime, IApiVersionDescriptionProvider versionDescriptionProvider)
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
        }
    }
}