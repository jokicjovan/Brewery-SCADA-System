using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Services
{
    public interface IDeviceService
    {
        Task StartSimulation();
        List<String> GetAllAddresses();
        Task<Device> Add(Device device);
    }
}
