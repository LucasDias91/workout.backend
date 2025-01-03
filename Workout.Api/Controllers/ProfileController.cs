using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Notifications;
using Workout.Infra.CrossCutting.ApiConfiguration;
using Workout.Domain.Entities.User.Repositories;
using Workout.Api.Responses;

namespace Workout.Api.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : BaseController
    {
        public ProfileController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, IMapper mapper, ILogger<BaseController> logger) : base(notifications, mediator, mapper, logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromServices] IUserRepository repository)
                                      => Response(_mapper.Map<ProfileResponse>(repository.GetById(GetUserId())));
    }
}
