using MediatR;
using Workout.Domain.Core.Handlers;

namespace Workout.Service.Training.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Workout.Domain.Core.Interfaces;
    using Workout.Domain.Core.Notifications;
    using Workout.Domain.Entities.Configuration.Repositories;
    using Workout.Domain.Entities.Training;
    using Workout.Domain.Entities.Training.Commands;
    using Workout.Domain.Entities.Training.Repositories;
    using Workout.Domain.Entities.User.Repositories;
    public class TrainingCommandHandler : CommandHandler, IRequestHandler<TrainingCommand, IEnumerable<Training>>
    {
        private readonly ITrainingRepository _trainingRepository;
        public TrainingCommandHandler(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications, IConfigurationRepository configureRepository, IUserRepository userRepository, ITrainingRepository trainingRepository) : base(uow, mediator, notifications)
        {
            _trainingRepository = trainingRepository;
        }

        async Task<IEnumerable<Training>> IRequestHandler<TrainingCommand, IEnumerable<Training>>.Handle(TrainingCommand request, CancellationToken cancellationToken)
        {
            var trainings = await Task.Run(() => _trainingRepository.GetAll());

            return trainings.Where(item=> item.Code == request.Code);
        }
    }
}
