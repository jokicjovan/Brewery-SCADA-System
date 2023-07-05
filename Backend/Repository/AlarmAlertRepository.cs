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

        public Task DeleteByAlarmId(Guid id)
        {
            var entityToDelete = ReadAll();
            foreach (var alarmAlert in entityToDelete)
            {
                if (alarmAlert.AlarmId != id) continue;
                _context.Remove(alarmAlert);
                _context.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<AlarmAlert>> FindByIdByTime(Guid id, DateTime from, DateTime to)
        {
            return await _entities
                .Where(e => e.AlarmId == id && e.Timestamp >= from && e.Timestamp <= to)
                .ToListAsync();
        }

        public async Task<List<AlarmAlert>> FindByAlarmId(Guid id)
        {
            return await _entities
                .Where(e => e.AlarmId == id).ToListAsync();
        }
    }
}
