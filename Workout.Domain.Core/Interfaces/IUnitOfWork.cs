
namespace Workout.Domain.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        bool Save();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
