using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public class AlarmAlertRepository : CrudRepository<AlarmAlert>, IAlarmAlertRepository
    {
        public AlarmAlertRepository(DatabaseContext context) : base(context)
        {
        }

        public void DeleteByAlarmId(Guid id)
        {
            var entityToDelete = ReadAll();
            foreach (var alarmAlert in entityToDelete)
            {
                if (alarmAlert.AlarmId != id) continue;
                _context.Remove(alarmAlert);
                _context.SaveChanges();
            }
        }
    }
}
