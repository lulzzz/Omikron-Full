using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Omikron.SharedKernel.Api.Swagger
{
    public class SwaggerTenantHeaderValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = Constants.TenantIdHeaderKey,
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema()
                {
                    Type = "string"
                }
            });
        }
    }
}