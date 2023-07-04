using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public interface IDeviceRepository: ICrudRepository<Device>
    {
        public Task<Device> FindByAddress(String address);
    }
}
