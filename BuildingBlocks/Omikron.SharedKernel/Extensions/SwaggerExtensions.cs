using System.Collections.Generic;
using Omikron.SharedKernel.Api.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Omikron.SharedKernel.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, string serviceName, string serviceDescription)
        {
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.CustomSchemaIds(x => x.FullName);

                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = $"{serviceName} API V{description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = serviceDescription,
                    });

                    options.AddSecurityDefinition("Bearer",
                        new OpenApiSecurityScheme()
                        {
                            In = ParameterLocation.Header,
                            Description = "Please enter into field the word 'Bearer' following by space and JWT",
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey
                        });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {new OpenApiSecurityScheme() { Name = "Bearer"} , new List<string>() }
                    });
                }

                options.OperationFilter<SwaggerDefaultValues>();
                options.OperationFilter<SwaggerTenantHeaderValues>();
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerWithUi(this IApplicationBuilder app, IApiVersionDescriptionProvider versionDescriptionProvider)
        {
            app.UseSwagger().UseSwaggerUI(options =>
            {
                foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpper());
                }
            });

            return app;
        }
    }
}
