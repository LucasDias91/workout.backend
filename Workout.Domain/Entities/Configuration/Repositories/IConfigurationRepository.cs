namespace Workout.Domain.Entities.Configuration.Repositories
{
    public interface IConfigurationRepository
    {
        public IEnumerable<UserImportation> GetUsers(string location);
        public IEnumerable<TrainingImportation> GetTrainings(string location);
    }
}
