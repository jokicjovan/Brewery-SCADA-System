using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Services
{
    public interface ITagService
    {
        Task<AnalogInput> addAnalogInputAsync(AnalogInput input,Guid userId);
        Task<DigitalInput> addDigitalInputAsync(DigitalInput input, Guid userId);
    }
}
