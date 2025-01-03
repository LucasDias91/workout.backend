
namespace Workout.Domain.Entities
{
    public class Entity
    {
        public int Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime LastEditDate { get; private set; }
        public bool IsActive { get; private set; }
        public void Inactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}
