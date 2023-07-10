using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class AlarmAlertRepository : CrudRepository<AlarmAlert>, IAlarmAlertRepository
    {
        public AlarmAlertRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task DeleteByAlarmId(Guid id)
        {
            var entityToDelete = await ReadAll();
            await Global._semaphore.WaitAsync();
            try
            {
                foreach (var alarmAlert in entityToDelete)
                {
                    if (alarmAlert.AlarmId != id) continue;
                    _context.Remove(alarmAlert);
                    _context.SaveChanges();
                }
                await Task.Delay(1);
            }
            finally
            {
                Global._semaphore.Release();
            }
        }

        public async Task<IEnumerable<AlarmAlert>> FindByIdByTime(Guid id, DateTime from, DateTime to)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                await Task.Delay(1);
                return await _entities
                    .Where(e => e.AlarmId == id && e.Timestamp >= from && e.Timestamp <= to)
                    .ToListAsync();
            }
            finally
            {
                Global._semaphore.Release();
            }
        }

        public async Task<List<AlarmAlert>> FindByAlarmId(Guid id)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                await Task.Delay(1);
                return await _entities
                    .Where(e => e.AlarmId == id).ToListAsync();
            }
            finally
            {
                Global._semaphore.Release();
            }
        }
    }
}
