using MediatR;
using Workout.Domain.Core.Handlers;
using Workout.Domain.Core.Interfaces;
using Workout.Domain.Core.Notifications;

namespace Workout.Service.Training.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Workout.Domain.Emums;
    using Workout.Domain.Entities.Training;
    using Workout.Domain.Entities.Training.Commands;
    using Workout.Domain.Entities.Training.Repositories;
    public class TrainingTimelineCommandHandler : CommandHandler, IRequestHandler<TrainingTimelineCommand, List<Timeline?>>
    {
        private readonly ITimelineRepository _timelineRepository;
        private readonly ITrainingRepository _trainingRepository;
        public TrainingTimelineCommandHandler(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications, ITimelineRepository timelineRepository, ITrainingRepository trainingRepository) : base(uow, mediator, notifications)
        {
            _timelineRepository = timelineRepository;
            _trainingRepository = trainingRepository;
        }

        public async Task<List<Timeline?>> Handle(TrainingTimelineCommand request, CancellationToken cancellationToken)
        {
            var trainings = _trainingRepository.GetAll();
            var timelines = _timelineRepository.GetAll()
                                               .Where(item => item.UserId == request.UserId);

            var query = (from left in trainings
                         join right in timelines on left.Code equals right.Code into joinedList
                         from sub in joinedList.DefaultIfEmpty()
                         select new Timeline(request.UserId, left.Code, left.Type, sub == null ? default : sub.Start, sub == null ? default : sub.End, left.Goal, trainings, timelines))
                         .DistinctBy(item => item.Code)
                         .OrderBy(item => item.Code)
                         .ToList();

            switch (request.Mode)
            {
                    case TimelineTypes.Previous:
                        query = query.Where(item => item.End != default)
                                     .ToList();
                          break;
                    case TimelineTypes.Next:
                        query = query.Where(item=> item.End == default)
                                     .ToList();
                         break;
            }

            return query;
        }
    }
}
