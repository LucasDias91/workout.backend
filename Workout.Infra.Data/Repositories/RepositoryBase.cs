using Workout.Domain.Core.Interfaces;
using Workout.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Workout.Infra.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly WorkoutDbContext _context;
        public RepositoryBase(WorkoutDbContext context)
        {
            _context = context;
        }
        public T? GetById(int id)
        {
            try
            {
               return _context.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                throw  ex; 
            }
        }
        public List<T> GetAll() 
        {
            try
            {
                return _context.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void Add(T item)
        {
            try
            {
                _context.Set<T>().Add(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Remove(T item)
        {
            try
            {
                _context.Set<T>().Remove(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update(T item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Save()
        {
            try
            {
                 _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Dispose() => _context.Dispose();
    }
}
