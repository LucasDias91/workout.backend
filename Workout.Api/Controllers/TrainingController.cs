using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workout.Api.AutoMapper.Extensions;
using Workout.Api.Requests;
using Workout.Api.Responses;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Notifications;
using Workout.Domain.Entities.Training;
using Workout.Domain.Entities.Training.Commands;
using Workout.Infra.CrossCutting.ApiConfiguration;
using Workout.Infra.CrossCutting.Attributes.Validators.ModelState;

namespace Workout.Api.Controllers
{
    [Route("api/training")]
    [ApiController]
    public class TrainingController : BaseController
    {
        public TrainingController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, IMapper mapper, ILogger<BaseController> logger) : base(notifications, mediator, mapper, logger)
        {
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetAsync(int code)
                     => Response((await _mediator.SendCommand<TrainingCommand, IEnumerable<Training>>(new TrainingCommand(code))).ToTrainingResponse());

        [Validate]
        [HttpPost("start")]
        public async Task<IActionResult> StartAsync([FromBody] TrainingStartRequest request)
        {
            await _mediator.SendCommand(new TrainingStartCommand(GetUserId(), request.Code, request.Type));
            return Response("Treino iniciado com sucesso!");
        }

        [Validate]
        [HttpPost("end")]
        public async Task<IActionResult> EndAsync([FromBody] TrainingEndRequest request)
        {
            await _mediator.SendCommand(new TrainingEndCommand(GetUserId(), request.Code, request.Type));
            return Response("Treino finalizado com sucesso!");
        }

        [HttpGet("timeline")]
        public async Task<IActionResult> GetTimelineAsync([FromQuery] TimelineRequest request)
                         => Response(_mapper.Map<List<TimelineResponse>>(await _mediator.SendCommand<TrainingTimelineCommand, List<Timeline?>>(new TrainingTimelineCommand(GetUserId(), request.Mode))));

    }
}
