using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class DigitalInputRepository : CrudRepository<DigitalInput>, IDigitalInputRepository
    {
        public DigitalInputRepository(DatabaseContext context) : base(context) { }

        public async Task<DigitalInput> FindByIdWithUsers(Guid id)
        {
            return await _entities.Include(u => u.Users).FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
