using Brewery_SCADA_System.Exceptions;
using Brewery_SCADA_System.Models;
using Brewery_SCADA_System.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Brewery_SCADA_System.Database;

namespace Brewery_SCADA_System.Services
{
    public class TagService : ITagService
    {
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IDigitalInputRepository _digitalInputRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIODigitalDataRepository _ioDigitalDataRepository;
        private readonly IIOAnalogDataRepository _ioAnalogDataRepository;

        // private readonly DatabaseContext _databaseContext;

        // public TagService(IConfiguration configuration)
        // {
        //     _databaseContext = new DatabaseContext(configuration);
        //     _analogInputRepository = new AnalogInputRepository(_databaseContext);
        //     _deviceRepository = new DeviceRepository(_databaseContext);
        //     _userRepository = new UserRepository(_databaseContext);
        //     _digitalInputRepository = new DigitalInputRepository(_databaseContext);
        //     _ioDigitalDataRepository = new IODigitalDataRepository(_databaseContext);
        //     _ioAnalogDataRepository = new IOAnalogDataRepository(_databaseContext);
        //
        // }

        public TagService(IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository, IDeviceRepository deviceRepository, IUserRepository userRepository, IIODigitalDataRepository ioDigitalDataRepository, IIOAnalogDataRepository ioAnalogDataRepository)
        {
            _analogInputRepository = analogInputRepository;
            _digitalInputRepository = digitalInputRepository;
            _deviceRepository = deviceRepository;
            _userRepository = userRepository;
            _ioDigitalDataRepository = ioDigitalDataRepository;
            _ioAnalogDataRepository = ioAnalogDataRepository;
        }

        public async Task<AnalogInput> addAnalogInputAsync(AnalogInput input, Guid userId)
        {
            if (await _deviceRepository.FindByAddress(input.IOAddress) == null)
                throw new InvalidInputException("Address does not exist");
            if (input.HighLimit <= input.LowLimit)
                throw new InvalidInputException("High limit cannot be lower than low limit");
            if (input.ScanTime <= 0)
                throw new InvalidInputException("Scan time must be greather than 0");
            User user = _userRepository.Read(userId);
            if (user == null)
                throw new InvalidInputException("User does not exist");

            user.AnalogInputs.Add(input);
            _userRepository.Update(user);
            return _analogInputRepository.Create(input);
        }

        public async Task<DigitalInput> addDigitalInputAsync(DigitalInput input, Guid userId)
        {
            if ( await _deviceRepository.FindByAddress(input.IOAddress) == null)
                throw new InvalidInputException("Address does not exist");
            if (input.ScanTime <= 0)
                throw new InvalidInputException("Scan time must be greather than 0");
            User user = _userRepository.Read(userId);
            if (user == null)
                throw new InvalidInputException("User does not exist");

            user.DigitalInputs.Add(input);
            _userRepository.Update(user);
            input.Id = Guid.NewGuid();
            DigitalInput saved = _digitalInputRepository.Create(input);
            return saved;
        }

        public async Task switchAnalogTag(Guid tagId, Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId);
            if (user == null)
                throw new ResourceNotFoundException("User not found!");
            AnalogInput analogInput = _analogInputRepository.Read(tagId);
            if (analogInput == null)
                throw new ResourceNotFoundException("There is no analog tag with this id!");
            if (!user.AnalogInputs.Any(tag => tag.Id == tagId))
                throw new InvalidInputException("User cannot access other users tags!");
            analogInput.ScanOn = !analogInput.ScanOn;
            _analogInputRepository.Update(analogInput);
            if(analogInput.ScanOn)
                StartAnalogTagReading(tagId);
        }

        private void StartAnalogTagReading(Guid tagId)
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    AnalogInput analogInput = _analogInputRepository.Read(tagId);
                    if (analogInput == null) 
                        break;
                    Console.WriteLine(analogInput.ScanOn);
                    if (analogInput.ScanOn)
                    {
                        Task<Device> deviceTask;
                        lock(DeviceService.dbContextLock)
                            deviceTask = _deviceRepository.FindByAddress(analogInput.IOAddress);
                        Device device = deviceTask.Result;
                        Console.WriteLine(device.Value);
                        IOAnalogData ioAnalogData = new IOAnalogData
                        {
                            Id = new Guid(),
                            Address = device.Address,
                            Value = device.Value,
                            Timestamp = DateTime.Now,
                            TagId = analogInput.Id
                        };
                        _ioAnalogDataRepository.Create(ioAnalogData);

                        // TODO: obavestiti alarme

                        // TODO: poslati socketom na front

                    }
                    Thread.Sleep(analogInput.ScanTime * 1000);

                }
            }).Start();
        }

        public async Task switchDigitalTag(Guid tagId, Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId);
            if (user == null)
                throw new ResourceNotFoundException("User not found!");
            DigitalInput digitalInput = _digitalInputRepository.Read(tagId);
            if (digitalInput == null)
                throw new ResourceNotFoundException("There is no digital tag with this id!");
            if (!user.DigitalInputs.Any(tag => tag.Id == tagId))
                throw new InvalidInputException("User cannot access other users tags!");
            digitalInput.ScanOn = !digitalInput.ScanOn;
            _digitalInputRepository.Update(digitalInput);
            if(digitalInput.ScanOn)
                StartDigitalTagReading(tagId);
        }

        private void StartDigitalTagReading(Guid tagId)
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    DigitalInput digitalInput = _digitalInputRepository.Read(tagId);
                    if (digitalInput == null)
                        break;

                    if (digitalInput.ScanOn)
                    {

                        Task<Device> deviceTask;
                        lock (DeviceService.dbContextLock)
                            deviceTask = _deviceRepository.FindByAddress(digitalInput.IOAddress);
                        Device device = deviceTask.Result; IODigitalData ioDigitalData = new IODigitalData
                        {
                            Id = new Guid(),
                            Address = device.Address,
                            Value = device.Value,
                            Timestamp = DateTime.Now,
                            TagId = digitalInput.Id
                        };
                        _digitalInputRepository.Create(digitalInput);

                        // TODO: obavestiti alarme

                        // TODO: poslati socketom na front

                    }
                    Thread.Sleep(digitalInput.ScanTime * 1000);

                }
            }).Start();
        }
    }
}
