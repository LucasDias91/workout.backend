namespace Workout.Domain.Core.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        T? GetById(int id);
        List<T> GetAll();
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        void Dispose();
        void Save();
    }
}
