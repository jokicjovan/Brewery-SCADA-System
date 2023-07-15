using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public interface IIOAnalogDataRepository : ICrudRepository<IOAnalogData>
    {
        Task<List<IOAnalogData>> FindByTagId(Guid id);
        Task<List<IOAnalogData>> FindByIdByTime(Guid id, DateTime from, DateTime to);
        Task<IOAnalogData> FindLatestById(Guid id);
        Task DeleteByTagId(Guid id);
    }
}
