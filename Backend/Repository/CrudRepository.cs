using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class CrudRepository<T> : ICrudRepository<T> where T : class, IBaseEntity
    {
        protected DatabaseContext _context;
        protected DbSet<T> _entities;

        public CrudRepository(DatabaseContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> ReadAll()
        {
            await Global._semaphore.WaitAsync();
            IEnumerable<T> data;
            try
            {
                data = _entities.ToList();
            }
            finally
            {
                Global._semaphore.Release();
            }
            return data;
        }

        public virtual async Task<T> Read(Guid id)
        {
            await Global._semaphore.WaitAsync();
            T data;
            try
            {
                data = _entities.FirstOrDefault(e => e.Id == id);
            }
            finally
            {
                Global._semaphore.Release();
            }
            return data;


        }

        public virtual async Task<T> Create(T entity)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                _entities.Add(entity);
                _context.SaveChanges();
                await Task.Delay(1);
            }
            finally
            {
                Global._semaphore.Release();
            }
            return entity;

        }

        public virtual async Task<T> Update(T entity)
        {
            T entityToUpdate;
            await Global._semaphore.WaitAsync();
            try
            {
                entityToUpdate = _entities.FirstOrDefault(e => e.Id == entity.Id);
                if (entityToUpdate != null)
                {
                    _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                    _context.SaveChanges();
                    await Task.Delay(1);
                }
            }
            finally
            {
                Global._semaphore.Release();
            }

            return entityToUpdate;
        }

        public virtual async Task<T> Delete(Guid id)
        {
            T entityToDelete;
            await Global._semaphore.WaitAsync();
            try
            {
                entityToDelete=_entities.FirstOrDefault(e => e.Id == id);
                if (entityToDelete != null)
                {
                    _context.Remove(entityToDelete);
                    _context.SaveChanges();
                    await Task.Delay(1);
                }
            }
            finally
            {
                Global._semaphore.Release();
            }
            return entityToDelete;
        }

    }
}
