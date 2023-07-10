using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Brewery_SCADA_System.Repository
{
    public class AnalogInputRepository : CrudRepository<AnalogInput>, IAnalogInputRepository
    {
        public AnalogInputRepository(DatabaseContext context) : base(context) { }

        public async Task<AnalogInput> FindByIdWithAlarmsAndUsers(Guid id)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                await Task.Delay(1);
                return await _entities.Include(e => e.Alarms).Include(u => u.Users).FirstOrDefaultAsync(e => e.Id == id);
            }
            finally
            {
                Global._semaphore.Release();
            }
        }

        public async Task DeleteAlarms(Guid tagId)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                var tag = await _context.AnalogInput.Include(t => t.Alarms).FirstOrDefaultAsync(t => t.Id == tagId);
                if (tag != null)
                {
                    tag.Alarms.Clear();
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
