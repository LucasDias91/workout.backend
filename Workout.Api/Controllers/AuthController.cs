using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Notifications;
using Workout.Domain.Core.Responses;
using Workout.Domain.Entities.Auth.Commands;
using Workout.Domain.Entities.Auth;
using Workout.Infra.CrossCutting.ApiConfiguration;
using Workout.Api.Responses;
using Workout.Api.Requests;

namespace Workout.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        public AuthController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, IMapper mapper, ILogger<BaseController> logger) : base(notifications, mediator, mapper, logger)
        {
        }

        [HttpPost]
        [Route("sign-in")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<SignInResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest request)
        {

            var command = new SignInCommand(request.UserName,
                                            request.Password,
                                            GetUserAgent(),
                                            GetUserIP(),
                                            GetHost(),
                                            request.AuthType,
                                            request.RefreshToken);

            var result = await _mediator.SendCommand<SignInCommand, Auth>(command);
            var accessData = _mapper.Map<SignInResponse>(result);
            return Response(accessData);
        }

        [HttpPost]
        [Route("sign-out")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignOutAsync()
        {
            await _mediator.SendCommand(new SignOutCommand(GetSessionKey()));
            return Response("Usuário deslogado com sucesso.");
        }

    }
}
