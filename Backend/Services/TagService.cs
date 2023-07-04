using Brewery_SCADA_System.Exceptions;
using Brewery_SCADA_System.Models;
using Brewery_SCADA_System.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Brewery_SCADA_System.Services
{
    public class TagService : ITagService
    {
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IDigitalInputRepository _digitalInputRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUserRepository _userRepository;
        public TagService( IAnalogInputRepository analogInputRepository, IDeviceRepository deviceRepository, IUserRepository userRepository, IDigitalInputRepository digitalInputRepository)
        {
            _analogInputRepository = analogInputRepository;
            _deviceRepository = deviceRepository;
            _userRepository = userRepository;
            _digitalInputRepository = digitalInputRepository;
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
    }
}
