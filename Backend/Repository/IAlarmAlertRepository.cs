using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public interface IAlarmAlertRepository : ICrudRepository<AlarmAlert>
    {
        void DeleteByAlarmId(Guid id);
        Task<IEnumerable<AlarmAlert>> FindByIdByTime(Guid id, DateTime from, DateTime to);
        Task<List<AlarmAlert>> FindByAlarmId(Guid id);
    }
}
