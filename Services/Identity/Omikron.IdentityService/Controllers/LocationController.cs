using Microsoft.AspNetCore.Mvc;
using Omikron.IdentityService.Domain.Queries;
using Omikron.SharedKernel.Api;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Utils;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController, ApiVersion("1.0")]
    public class LocationController : BaseMultiTenantController
    {
        public LocationController(IDispatcher dispatcher, IServiceProvider serviceProvider) : base(dispatcher, serviceProvider)
        {
        }

        [HttpGet, SwaggerOperation(OperationId = "GetLocationsByPostcode.Query")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> GetLocationsByPostcode([FromQuery] string postcode)
        {
            var result = await Dispatcher.DispatchAsync(new GetLocationsByPostcode.Query(postcode));
            return result.ToActionResult();
        }
    }
}