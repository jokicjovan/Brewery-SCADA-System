using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class IOAnalogDataRepository : CrudRepository<IOAnalogData>, IIOAnalogDataRepository
    {
        public IOAnalogDataRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<IOAnalogData>> FindByTagId(Guid id)
        {
            return await _entities
                .Where(e => e.TagId == id).ToListAsync();
        }

        public async Task<List<IOAnalogData>> FindByIdByTime(Guid id, DateTime from, DateTime to)
        {
            return await _entities
                .Where(e => e.TagId == id && e.Timestamp >= from && e.Timestamp <= to)
                .ToListAsync();
        }

        public async Task<IOAnalogData> FindLatestById(Guid id)
        {
            return await _entities.OrderByDescending(e => e.Timestamp).Where(e => e.TagId == id).FirstOrDefaultAsync();
        }

        public async Task DeleteByTagId(Guid id)
        {
            var entities = await _entities.Where(e => e.TagId == id).ToListAsync();
            if (entities.Count > 0)
            {
                _entities.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }
        }
    }
}
