using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Notifications;
using Workout.Infra.CrossCutting.ApiConfiguration;
using Workout.Domain.Entities.Configuration.Commands;
namespace Workout.Api.Controllers
{
    [Route("api/configuration")]
    [ApiController]
    public class ConfigurationController : BaseController
    {
        public ConfigurationController(INotificationHandler<DomainNotification> notifications, 
                                       IMediatorHandler mediator, 
                                       IMapper mapper, 
                                       ILogger<BaseController> logger) : base(notifications, mediator, mapper, logger)
        {
        }

        [HttpPost]
        public async Task<IActionResult> ConfigurationAsync()
        {
            string location = Path.Combine("C:\\inetpub\\wwwroot\\fileManager", "excel", "configuration.xlsx");
            await _mediator.SendCommand(new ConfigurationCommand(location));
            return Response("Dados importados com sucesso!");
        }
    }
}
