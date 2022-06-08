using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Omikron.IdentityService.Infrastructure.Extensions;
using Omikron.IdentityService.Infrastructure.SmsProvider.Twilio;
using Omikron.SharedKernel.Extensions;

namespace Omikron.IdentityService
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

            services.AddDataTraceCommandHandlers(assemblies: assemblies, configuration: Configuration);

            services.AddLogger(configuration: Configuration);

            services
                .AddDataRepository(assemblies: assemblies)
                .AddControllers()
                .AddJson()
                .AddFluentValidation(configurationExpression: fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services
                .AddApiVersioning()
                .AddVersionedApiExplorer(setupAction: o => o.GroupNameFormat = "'v'VVV")
                .AddSwagger(serviceName: Constants.ServiceName, serviceDescription: Constants.ServiceDescription);

            services
                .AddAuthenticationWithJwtBearer(configuration: Configuration);

            services
                .AddAzureStorageProvider(configuration: Configuration)
                .AddAzureKeyVault()
                .AddIdentityServerWithConfigure(configuration: Configuration)
                .AddCommonFeature(configuration: Configuration)
                .AddDefaultAzureAssetManager(configuration: Configuration)
                .AddDomainEvents(assemblies: assemblies)
                .AddEmailService(configuration: Configuration)
                .AddEmailContentFactories(assemblies: assemblies)
                .AddCombinedCacheManager(configuration: Configuration)
                .AddMapper(assembly: assemblies)
                .AddBudApiClient(configuration: Configuration)
                .AddTwilioSmsProvider(configuration: Configuration)
                .AddHttpTokenService(configuration: Configuration)
                .AddHttpVaultServiceClient(configuration: Configuration);

            services
                .AddFactoriesAndServices();
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
            app.UseIdentityServerAndApplyMigrations();
            app.NotifyServiceSuccessfullyStarted(serviceName: Constants.ServiceName);
        }
    }
}