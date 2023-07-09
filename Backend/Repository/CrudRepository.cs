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

        public virtual IEnumerable<T> ReadAll()
        {
            return _entities.ToList();
        }

        public virtual T Read(Guid id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        public virtual T Create(T entity)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _entities.Add(entity);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return entity;

        }

        public virtual T Update(T entity)
        {
            var entityToUpdate = Read(entity.Id);
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (entityToUpdate != null)
                    {
                        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                        _context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Handle exceptions and roll back the transaction if needed
                    transaction.Rollback();
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return entityToUpdate;
        }

        public virtual T Delete(Guid id)
        {
            var entityToDelete = Read(id);
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (entityToDelete != null)
                    {
                        _context.Remove(entityToDelete);
                        _context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Handle exceptions and roll back the transaction if needed
                    transaction.Rollback();
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            

            return entityToDelete;
        }

    }
}
