using Workout.Domain.Core.Handlers;
using Workout.Domain.Core.Notifications;
using Workout.Domain.Core.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Workout.Infra.CrossCutting.ApiConfiguration
{
    using AutoMapper;
    using Workout.Domain.Core.Interfaces;
    using Workout.Infra.CrossCutting.Security.Extensions;
    using Microsoft.Extensions.Logging;

    [Route("api")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediator;
        protected readonly IMapper _mapper;
        protected readonly ILogger<BaseController> _logger;
        protected BaseController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, IMapper mapper, ILogger<BaseController> logger)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        protected IActionResult Response()
        {
            if (!IsValidOperation())
                return HandlerError();

            return Ok();
        }

        protected IActionResult Response(string message)
        {
            if (!IsValidOperation())
                return HandlerError();

            return Ok(new {message});
        }

        protected IActionResult Response<T>(T data)
        {
            if (!IsValidOperation())
                return HandlerError();

            return Ok(data);
        }

        protected IActionResult Success<T>(T data)
        {
            if (!IsValidOperation())
                return HandlerError();

            return Ok(data);
        }

        protected IActionResult Created<T>(T data)
        {
            if (!IsValidOperation())
                return HandlerError();

            return Ok(data);
        }
        protected IActionResult HandlerError()
        {
            var notifications = _notifications.GetNotifications().Select(n => n.Value).Distinct();
            if (!notifications.Any())
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse.Error(notifications.FirstOrDefault()));
        }

        protected IActionResult NotAuthorized<T>()
        {
            return Unauthorized();
        }
        protected bool IsValidOperation() => !_notifications.HasErrors();
        protected string? GetUserAgent() => Request.Headers["User-Agent"].FirstOrDefault();
        protected string GetUserIP() => Request.HttpContext.GetIPAddress();
        protected string GetHost() => Request.HttpContext.Request.Host.Host;
        protected string? GetSessionKey() => User.Identity?.GetSessionKey();
        protected int GetUserId() => User.Identity.GetUserId();

    }
}