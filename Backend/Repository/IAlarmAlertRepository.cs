using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public interface IAlarmAlertRepository : ICrudRepository<AlarmAlert>
    {
        void DeleteByAlarmId(Guid id);
    }
}
