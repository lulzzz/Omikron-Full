using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Omikron.SharedKernel.Api;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Security;
using Omikron.SharedKernel.Utils;
using Omikron.Sync.Service.Business.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Omikron.Sync.Service.Controllers
{
    [AuthorizeByTenantAndCredentials]
    [Route(template: "api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion(version: "1.0")]
    public class SyncController : BaseMultiTenantController
    {
        public SyncController(IDispatcher dispatcher, IServiceProvider serviceProvider) : base(dispatcher: dispatcher, serviceProvider: serviceProvider)
        {
        }

        [HttpPost(template: "start-sync")]
        [SwaggerOperation(OperationId = "StartSync.Command")]
        [SwaggerResponse(statusCode: 200, description: SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(statusCode: 401, description: SwaggerConstants.ResponseHttp401)]
        public async Task<IActionResult> StartSync()
        {
            var result = await Dispatcher.DispatchAsync(command: new OrchestrateSingleSync.Command(Username));
            return result.ToActionResult();
        }
    }
}