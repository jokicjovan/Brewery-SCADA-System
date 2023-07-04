using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class AnalogInputRepository : CrudRepository<AnalogInput>, IAnalogInputRepository
    {
        public AnalogInputRepository(DatabaseContext context) : base(context) { }

        public async Task<AnalogInput> FindByIdWithAlarms(Guid id)
        {
            return await _entities.Include(e => e.Alarms).FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
