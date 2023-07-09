using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Exceptions;
using Brewery_SCADA_System.Models;
using Brewery_SCADA_System.Repository;

namespace Brewery_SCADA_System.Services
{
    public class DeviceService:IDeviceService
    {

        private readonly IDeviceRepository _deviceRepository;
        public static readonly object dbContextLock = new object();

        // private readonly DatabaseContext _databaseContext;
        //
        // public DeviceService(IDeviceRepository deviceRepository, IConfiguration configuration)
        // {
        //     _databaseContext = new DatabaseContext(configuration);
        //     _deviceRepository = new DeviceRepository(_databaseContext);
        // }

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public void StartSimulation()
        {
            new Thread(async() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Random r = new Random();

                while (true)
                {
                    List<Device> devices = _deviceRepository.ReadAll().ToList();
                    foreach (Device device in devices)
                    {
                        lock (dbContextLock)
                        {
                            if (device.Value == 0)
                                device.Value = 7;

                            device.Value = device.Value * (r.NextDouble() * 0.4 + 0.8);
                            _deviceRepository.Update(device);
                        }
                    }
                    Thread.Sleep(2000);
                }
            }).Start();
            
        }

        public List<String> GetAllAddresses()
        {
            return _deviceRepository.ReadAll().Select(device => device.Address).ToList();
        }
    }
}
