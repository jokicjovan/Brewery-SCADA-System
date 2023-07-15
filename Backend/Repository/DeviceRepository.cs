using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class DeviceRepository:CrudRepository<Device>,IDeviceRepository
    {
        public DeviceRepository(DatabaseContext context) : base(context) { }

        public async Task<Device> FindByAddress(String address)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                await Task.Delay(1);
                return await _entities.FirstOrDefaultAsync(e => e.Address == address);
            }
            finally
            {
                Global._semaphore.Release();
            }
        }
    }
}
