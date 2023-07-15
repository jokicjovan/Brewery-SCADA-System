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
            await Global._semaphore.WaitAsync();

            try
            {
                await Task.Delay(1);
                return await _entities
                    .Where(e => e.TagId == id).ToListAsync();
            }
            finally
            {
                Global._semaphore.Release();
            }
        }

        public async Task<List<IODigitalData>> FindByIdByTime(Guid id, DateTime from, DateTime to)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                await Task.Delay(1);
                return await _entities
                    .Where(e => e.TagId == id && e.Timestamp >= from && e.Timestamp <= to)
                    .ToListAsync();
            }
            finally
            {
                Global._semaphore.Release();
            }
        }

        public async Task<IODigitalData> FindLatestById(Guid id)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                await Task.Delay(1);
                return await _entities.OrderByDescending(e => e.Timestamp).Where(e => e.TagId == id).FirstOrDefaultAsync();
            }
            finally
            {
                Global._semaphore.Release();
            }
        }
        public async Task DeleteByTag(Guid id)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                var entities = await _entities.Where(e => e.TagId == id).ToListAsync();
                if (entities.Count > 0)
                {
                    _entities.RemoveRange(entities);
                    await _context.SaveChangesAsync();
                }
                await Task.Delay(1);
            }
            finally
            {
                Global._semaphore.Release();
            }
        }
    }
}
