using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public interface IIODigitalDataRepository : ICrudRepository<IODigitalData>
    {
        Task<List<IODigitalData>> FindByTagId(Guid id);
        Task<List<IODigitalData>> FindByIdByTime(Guid id, DateTime from, DateTime to);
        Task<IODigitalData> FindLatestById(Guid id);
        Task DeleteByTag(Guid id);


    }
}
