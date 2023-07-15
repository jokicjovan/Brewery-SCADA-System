using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public interface ICrudRepository<T> where T : class, IBaseEntity
    {
        Task<IEnumerable<T>> ReadAll();
        Task<T> Read(Guid id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(Guid id);
    }
}
