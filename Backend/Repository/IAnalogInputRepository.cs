using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public interface IAnalogInputRepository:ICrudRepository<AnalogInput>
    {
        public Task<AnalogInput> FindByIdWithAlarmsAndUsers(Guid id);
    }
}
