using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Hubs
{
    public interface ITagClient
    {
        Task ReceiveAnalogData(IOAnalogData data);
        Task ReceiveDigitalData(IODigitalData data);
    }
}
