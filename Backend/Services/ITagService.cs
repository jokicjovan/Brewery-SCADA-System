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
s    }
}
