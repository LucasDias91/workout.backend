using Workout.Domain.Entities.Configuration.Repositories;
using Workout.Domain.Entities.Configuration.Services;
using Workout.Domain.Entities.Training.Repositories;
using Workout.Domain.Entities.User;
using Workout.Domain.Entities.User.Repositories;

namespace Workout.Service.Configuration.Services
{
    using Microsoft.Extensions.Logging;
    using Workout.Domain.Entities.Training;
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository _configureRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly ILogger<ConfigurationService> _logger;
        public ConfigurationService(IConfigurationRepository configureRepository,
                                    IUserRepository userRepository,
                                    ITrainingRepository trainingRepository,
                                    ILogger<ConfigurationService> logger)
        {
            _configureRepository = configureRepository;
            _userRepository = userRepository;
            _trainingRepository = trainingRepository;
            _logger = logger;
        }
        public void ImportUsers(string location)
        {
            try
            {
                var items = _configureRepository.GetUsers(location);
                var users = _userRepository.GetAll();
                var userNames = users.Select(x => x.Username).Distinct();

                items.Where(user => !userNames.Contains(user.Username))
                     .ToList()
                     .ForEach((item) => _userRepository.Add(new User(item.Name, item.Username, item.Password)));

                _userRepository.Save();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Faild do import users: Location{0}", location);
                throw;
            }

        }

        public void ImportTrainings(string location)
        {
            try
            {
                var items = _configureRepository.GetTrainings(location);
                var trainings = _trainingRepository.GetAll();
                var codes = trainings.Select(x => x.Code).Distinct();
   
                items.Where(item => !codes.Contains(Convert.ToInt32(item.Code)))
                     .ToList()
                     .ForEach((item) => _trainingRepository.Add(new Training(item.Code, item.Date, item.Goal, item.Exercise, item.SxR, item.Technique, item.Group, char.Parse(item.Type))));

                _trainingRepository.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Faild do import trainings: Location{0}", location);
                throw;
            }
        }
    }
}
