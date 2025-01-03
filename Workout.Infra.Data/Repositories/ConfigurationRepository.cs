using Workout.Domain.Entities.Configuration;
using Workout.Domain.Entities.Configuration.Repositories;
using Workout.Infra.CrossCutting.Excel;

namespace Workout.Infra.Data.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        public IEnumerable<UserImportation> GetUsers(string location) => Excel.ToList<UserImportation>(location, "Users");
        public IEnumerable<TrainingImportation> GetTrainings(string location) => Excel.ToList<TrainingImportation>(location, "Trainings");
    }
}
