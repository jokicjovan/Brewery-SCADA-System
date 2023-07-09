using Brewery_SCADA_System.DTO;

namespace Brewery_SCADA_System.Services
{
    public interface IDeviceService
    {
        Task StartSimulation();
        List<String> GetAllAddresses();
    }
}
