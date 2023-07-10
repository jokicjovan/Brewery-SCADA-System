using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Exceptions;
using Brewery_SCADA_System.Models;
using Brewery_SCADA_System.Repository;

namespace Brewery_SCADA_System.Services
{
    public class DeviceService : IDeviceService
    {

        private readonly IDeviceRepository _deviceRepository;
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IDigitalInputRepository _digitalInputRepository;



        public DeviceService(IDeviceRepository deviceRepository, IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository)
        {
            _deviceRepository = deviceRepository;
            _analogInputRepository = analogInputRepository;
            _digitalInputRepository = digitalInputRepository;

        }

        public async Task StartSimulation()
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;
                Random r = new Random();

                while (true)
                {
                    List<Device> devices =(await _deviceRepository.ReadAll()).ToList();
                    foreach (Device device in devices)
                    {
                        {
                            string driverType = "";
                            bool isDigital = false;
                            DigitalInput digitalInput = await _digitalInputRepository.FindByIdWithUsers(Guid.Parse(device.Address));
                            if (digitalInput != null)
                            {
                                isDigital = true;
                                driverType = digitalInput.Driver;
                            }
                            else
                            {
                                AnalogInput analogInput = (await _analogInputRepository.Read(Guid.Parse(device.Address)));
                                if (analogInput != null)
                                {

                                    driverType = analogInput.Driver;
                                    isDigital = false;
                                }
                            }

                            if (driverType == "SIMULATION")
                            {
                                if (isDigital)
                                {
                                    switch (Global.Simulation)
                                    {
                                        case "SIN":
                                            device.Value = Sine() > 0 ? 1 : 0;
                                            break;
                                        case "COS":
                                            device.Value = Cosine() > 0 ? 1 : 0;
                                            break;
                                        default:
                                            device.Value = Ramp() > 49 ? 1 : 0;
                                            break;

                                    }
                                }
                                else
                                {
                                    switch (Global.Simulation)
                                    {
                                        case "SIN":
                                            device.Value = Sine();
                                            break;
                                        case "COS":
                                            device.Value = Cosine();
                                            break;
                                        default:
                                            device.Value = Ramp();
                                            break;

                                    }
                                }
                            }
                            else if (driverType =="RTU")
                            {
                                if (isDigital)
                                {
                                    if (r.NextDouble() > 0.5)
                                        device.Value = 1;
                                    else
                                        device.Value = 0;
                                }
                                else
                                {
                                    double newValue = device.Value * (r.NextDouble() * 0.4 + 0.8);
                                    if (Math.Abs(device.Value - newValue) < 0.01)
                                        device.Value = (Global.LowLimit + Global.HighLimit) * 0.5;
                                    else
                                    {
                                        device.Value = newValue;
                                    }

                                }
                                _deviceRepository.Update(device);
                            }

                        }
                    }
                    Thread.Sleep(Global.Frequency);
                }
            }).Start();

        }
        private double Sine()
        {
            return 100 * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI);
        }

        private double Cosine()
        {
            return 100 * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI);
        }

        private double Ramp()
        {
            return 100 * DateTime.Now.Second / 60;
        }

        public async Task<List<String>> GetAllAddresses()
        {
            return (await _deviceRepository.ReadAll()).Select(device => device.Address).ToList();
        }

        public async Task<Device> Add(Device device)
        {
            return await _deviceRepository.Create(device);
        }
    }
}
