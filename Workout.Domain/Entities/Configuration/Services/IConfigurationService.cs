
namespace Workout.Domain.Entities.Configuration.Services
{
    public interface IConfigurationService
    {
        void ImportUsers(string location);
        void ImportTrainings(string location);
    }
}
