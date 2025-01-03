using MediatR;
using Workout.Domain.Core.Handlers;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Notifications;
using Workout.Domain.Entities.Training.Commands;
using Workout.Domain.Entities.Training.Repositories;

namespace Workout.Service.Training.Commands
{
    public class TrainingEndCommandHandler : CommandHandler, IRequestHandler<TrainingEndCommand>
    {
        private readonly ITimelineRepository _timelineRepository;
        private readonly ITrainingRepository _trainingRepository;
        public TrainingEndCommandHandler(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications, ITimelineRepository timelineRepository, ITrainingRepository trainingRepository) : base(uow, mediator, notifications)
        {
            _timelineRepository = timelineRepository;
            _trainingRepository = trainingRepository;
        }
        public Task Handle(TrainingEndCommand request, CancellationToken cancellationToken)
        {
            var types = _trainingRepository.GetAll()
                                           .Where(item => item.Code == request.Code)
                                           .Select(item => item.Type)
                                           .Distinct();

            if (!types.Any())
            {
                ErrorNotification("Código não existe!");
                return Completed();
            }

            if (!types.Where(type => type == request.Type).Any())
            {
                ErrorNotification("Type não existe!");
                return Completed();
            }

            var timeline = _timelineRepository.GetAll()
                                              .Where(item => item.Code == request.Code && item.UserId == request.UserId && item.Type == request.Type)
                                              .FirstOrDefault();

            if (timeline == default)
            {
                ErrorNotification("Treino ainda não iniciado!");
                return Completed();
            }

            if(timeline.End != default)
            {

                ErrorNotification("Treino já finalizado iniciado!");
                return Completed();
            }

            timeline.TimelineEnd();

            _timelineRepository.Update(timeline);
            _timelineRepository.Save();

            return Completed();
        }
    }
}
