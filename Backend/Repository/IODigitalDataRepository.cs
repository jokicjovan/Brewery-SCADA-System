using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class IODigitalDataRepository : CrudRepository<IODigitalData>, IIODigitalDataRepository
    {
        public IODigitalDataRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<IODigitalData>> FindByTagId(Guid id)
        {
            return await _entities
                .Where(e => e.TagId == id).ToListAsync();
        }

        public async Task<List<IODigitalData>> FindByIdByTime(Guid id, DateTime from, DateTime to)
        {
            return await _entities
                .Where(e => e.TagId == id && e.Timestamp >= from && e.Timestamp <= to)
                .ToListAsync();
        }

        public async Task<IODigitalData> FindLatestById(Guid id)
        {
            return await _entities.OrderByDescending(e => e.Timestamp).Where(e => e.TagId == id).FirstOrDefaultAsync();
        }
    }
}
