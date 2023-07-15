using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Services
{
    public interface ITagService
    {
        Task<AnalogInput> addAnalogInputAsync(AnalogInput input,Guid userId);
        Task<DigitalInput> addDigitalInputAsync(DigitalInput input, Guid userId);
        Task switchAnalogTag(Guid tagId, Guid userId);
        Task switchDigitalTag(Guid tagId, Guid userId);
        Task deleteAnalogInputAsync(Guid tagId, Guid userId);
        Task deleteDigitalInputAsync(Guid tagId, Guid userId);
        Task<TagReportsDto> getAllTagValuesByTime(Guid userId, DateTime timeFrom, DateTime timeTo);
        Task<List<IOAnalogData>> getLatestAnalogTagsValues(Guid userId);
        Task<List<IODigitalData>> getLatestDigitalTagsValues(Guid userId);
        Task<List<IOAnalogData>> getAllAnalogTagValues(Guid userId, Guid tagId);
        Task<List<IODigitalData>> getAllDigitalTagValues(Guid userId, Guid tagId);
        Task<DigitalInput> getDigitalInput(Guid tagId, Guid userId);
        Task<AnalogInput> getAnalogInput(Guid tagId, Guid userId);
        Task<IOAnalogData> getLatestAnalogTagValue(Guid tagId, Guid userId);
        Task<IODigitalData> getLatestDigitalTagValue(Guid tagId, Guid userId);
        Task updateAnalog(Guid id, double value, Guid userId);
        Task updateDigital(Guid id, double value, Guid userId);
        Task startupCheck();
    }
}
