using Workout.Domain.Core.Interfaces;
using Workout.Infra.Data.Context;

namespace Workout.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkoutDbContext _context;

        public UnitOfWork(WorkoutDbContext context)
        {
            _context= context;  
        }
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
              //  _log.Save(ex);
            }
            return false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

