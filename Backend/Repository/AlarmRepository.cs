using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public class AlarmRepository : CrudRepository<Alarm>,  IAlarmRepository 
    {
        public AlarmRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
