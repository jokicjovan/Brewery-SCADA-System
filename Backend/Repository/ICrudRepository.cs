using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public interface ICrudRepository<T> where T : class, IBaseEntity
    {
        IEnumerable<T> ReadAll();
        T Read(Guid id);
        T Create(T entity);
        T Update(T entity);
        T Delete(Guid id);
    }
}
