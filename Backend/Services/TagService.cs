using Brewery_SCADA_System.Exceptions;
using Brewery_SCADA_System.Models;
using Brewery_SCADA_System.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.DTO;
using Microsoft.AspNetCore.SignalR;
using Brewery_SCADA_System.Hubs;

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
        private readonly IAlarmAlertRepository _alarmAlertRepository;
        private readonly IAlarmRepository _alarmRepository;
        private readonly IHubContext<TagHub, ITagClient> _tagHub;
        private readonly IHubContext<AlarmHub, IAlarmClient> _alarmHub;

        public TagService(IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository,
            IDeviceRepository deviceRepository, IUserRepository userRepository, IIODigitalDataRepository ioDigitalDataRepository,
            IIOAnalogDataRepository ioAnalogDataRepository, IAlarmAlertRepository alarmAlertRepository,
            IHubContext<TagHub, ITagClient> tagHub, IHubContext<AlarmHub, IAlarmClient> alarmHub, IAlarmRepository alarmRepository)
        {
            _analogInputRepository = analogInputRepository;
            _digitalInputRepository = digitalInputRepository;
            _deviceRepository = deviceRepository;
            _userRepository = userRepository;
            _ioDigitalDataRepository = ioDigitalDataRepository;
            _ioAnalogDataRepository = ioAnalogDataRepository;
            _alarmAlertRepository = alarmAlertRepository;
            _tagHub = tagHub;
            _alarmHub = alarmHub;
            _alarmRepository = alarmRepository;

            
        }

        public async Task startupCheck()
        {
            foreach(AnalogInput input in await _analogInputRepository.ReadAll())
            {
                if (input.ScanOn)
                {
                    StartAnalogTagReading(input.Id);
                }
            }
            foreach (DigitalInput input in await _digitalInputRepository.ReadAll())
            {
                if (input.ScanOn)
                {
                    StartDigitalTagReading(input.Id);
                }
            }
        }

        public async Task<AnalogInput> addAnalogInputAsync(AnalogInput input, Guid userId)
        {
            if (input.HighLimit <= input.LowLimit)
                throw new InvalidInputException("High limit cannot be lower than low limit");
            if (input.ScanTime <= 0)
                throw new InvalidInputException("Scan time must be greater than 0");
            User user = await _userRepository.Read(userId);
            if (user == null)
                throw new InvalidInputException("User does not exist");

            Guid guid = Guid.NewGuid();
            input.Id = guid;
            input.IOAddress = guid.ToString();
            Device device = new Device(guid.ToString(), 50);
            _deviceRepository.Create(device);
            _analogInputRepository.Create(input);
            input.Users.Add(user);
            user.AnalogInputs.Add(input);

            User saved = await _userRepository.Update(user);
            UpdatePermissionsAnalogInputs(saved.AnalogInputs.Last(), userId);
            if (input.ScanOn)
                StartAnalogTagReading(saved.AnalogInputs.Last().Id);
            return input;
        }

        public async Task<DigitalInput> addDigitalInputAsync(DigitalInput input, Guid userId)
        {
            if (input.ScanTime <= 0)
                throw new InvalidInputException("Scan time must be greater than 0");
            User user = await _userRepository.Read(userId);
            if (user == null)
                throw new InvalidInputException("User does not exist");

            Guid guid = Guid.NewGuid();
            input.Id = guid;
            input.IOAddress = guid.ToString();
            Device device = new Device(guid.ToString(), 0);

            _deviceRepository.Create(device);
            _digitalInputRepository.Create(input);
            user.DigitalInputs.Add(input);
            User saved = await _userRepository.Update(user);
            UpdatePermissionsDigitalInputs(saved.DigitalInputs.Last(), userId);
            if (input.ScanOn)
                StartDigitalTagReading(saved.DigitalInputs.Last().Id);
            return input;
        }

        private async Task UpdatePermissionsAnalogInputs(AnalogInput input,Guid userId)
        {
            List<User> users=await _userRepository.GetAllByCreatedBy(userId);
            foreach (User user in users)
            {
                user.AnalogInputs.Add(input);
                _userRepository.Update(user);
            }
        }

        private async Task UpdatePermissionsDigitalInputs(DigitalInput input, Guid userId)
        {
            List<User> users = await _userRepository.GetAllByCreatedBy(userId);
            foreach (User user in users)
            {
                user.DigitalInputs.Add(input);
                _userRepository.Update(user);
            }
        }

        public async Task<AnalogInput> getAnalogInput(Guid tagId, Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId);
            if (user == null)
                throw new InvalidInputException("User does not exist");

            AnalogInput tag = await _analogInputRepository.Read(tagId);
            if (tag == null)
                throw new InvalidInputException("Tag does not exist");

            if (!user.AnalogInputs.Any(input => input.Id == tagId))
                throw new ResourceNotFoundException("Tag does net exist");

            return tag;

        }

        public async Task<DigitalInput> getDigitalInput(Guid tagId, Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId);
            if (user == null)
                throw new InvalidInputException("User does not exist");

            DigitalInput tag = await _digitalInputRepository.Read(tagId);
            if (tag == null)
                throw new InvalidInputException("Tag does not exist");

            if (!user.AnalogInputs.Any(input => input.Id == tagId))
                throw new ResourceNotFoundException("Tag does net exist");

            return tag;

        }



        public async Task deleteDigitalInputAsync(Guid tagId, Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId);
            if (user == null)
                throw new InvalidInputException("User does not exist");

            DigitalInput tag = await _digitalInputRepository.Read(tagId);
            if (tag == null)
                throw new InvalidInputException("Tag does not exist");

            if (!user.DigitalInputs.Any(input => input.Id == tagId))
                throw new ResourceNotFoundException("Tag does net exist");

            user.DigitalInputs.Remove(tag);
            List<User> users = await _userRepository.GetAllByCreatedBy(userId);
            foreach (User u in users)
            {
                u.DigitalInputs.Remove(tag);
                _userRepository.Update(user);
            }
            _userRepository.Update(user);
            _digitalInputRepository.Delete(tag.Id);

        }

        public async Task<TagReportsDto> getAllTagValuesByTime(Guid userId, DateTime timeFrom, DateTime timeTo)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            TagReportsDto reports = new TagReportsDto();
            foreach (AnalogInput analogInput in user.AnalogInputs)
            {
                List<IOAnalogData> analogData = await _ioAnalogDataRepository.FindByIdByTime(analogInput.Id, timeFrom, timeTo);
                reports.IoAnalogDataList.AddRange(analogData);

                List<IODigitalData> digitalData = await _ioDigitalDataRepository.FindByIdByTime(analogInput.Id, timeFrom, timeTo);
                reports.IoDigitalDataList.AddRange(digitalData);
            }

            return reports;
        }

        public async Task<List<IOAnalogData>> getLatestAnalogTagsValues(Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            List<IOAnalogData> analogDatas = new List<IOAnalogData>();
            foreach (AnalogInput analogInput in user.AnalogInputs)
            {
                IOAnalogData analogData = await _ioAnalogDataRepository.FindLatestById(analogInput.Id);
                analogDatas.Add(analogData);
            }

            return analogDatas;
        }
        public async Task<IOAnalogData> getLatestAnalogTagValue(Guid tagId,Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            IOAnalogData analogData = await _ioAnalogDataRepository.FindLatestById(tagId);
            return analogData;
        }

        public async Task<IODigitalData> getLatestDigitalTagValue(Guid tagId, Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            IODigitalData digitalData = await _ioDigitalDataRepository.FindLatestById(tagId);
            return digitalData;
        }

        public async Task updateAnalog(Guid id, double value, Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            AnalogInput tag = await _analogInputRepository.Read(id);
            IOAnalogData ioAnalogData = new IOAnalogData
            {
                Id = new Guid(),
                IOAddress = tag.IOAddress,
                Value = value,
                Timestamp = DateTime.Now,
                TagId = tag.Id
            };
            _ioAnalogDataRepository.Create(ioAnalogData);
        }
        public async Task updateDigital(Guid id, double value, Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            DigitalInput tag = await _digitalInputRepository.Read(id);
            IODigitalData ioDigitalData = new IODigitalData
            {
                Id = new Guid(),
                IOAddress = tag.IOAddress,
                Value = value,
                Timestamp = DateTime.Now,
                TagId = tag.Id
            };
            _ioDigitalDataRepository.Create(ioDigitalData);
        }

        public async Task<List<IODigitalData>> getLatestDigitalTagsValues(Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            List<IODigitalData> digitalDatas = new List<IODigitalData>();
            foreach (DigitalInput digitalInput in user.DigitalInputs)
            {
                IODigitalData digitalData = await _ioDigitalDataRepository.FindLatestById(digitalInput.Id);
                digitalDatas.Add(digitalData);
            }

            return digitalDatas;
        }

        public async Task<List<IOAnalogData>> getAllAnalogTagValues(Guid userId, Guid tagId)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            AnalogInput analogInput = await _analogInputRepository.Read(tagId) ?? throw new ResourceNotFoundException("There is no analog tag with this id!");
            if (user.AnalogInputs.All(tag => tag.Id != tagId))
                throw new InvalidInputException("User cannot access other users tags!");

            return await _ioAnalogDataRepository.FindByTagId(tagId);
        }

        public async Task<List<IODigitalData>> getAllDigitalTagValues(Guid userId, Guid tagId)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            DigitalInput digitalInput = await _digitalInputRepository.Read(tagId) ?? throw new ResourceNotFoundException("There is no digital tag with this id!");
            if (user.DigitalInputs.All(tag => tag.Id != tagId))
                throw new InvalidInputException("User cannot access other users tags!");

            return await _ioDigitalDataRepository.FindByTagId(tagId);
        }


        public async Task deleteAnalogInputAsync(Guid tagId, Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId);
            if (user == null)
                throw new InvalidInputException("User does not exist");

            AnalogInput tag = await _analogInputRepository.FindByIdWithAlarmsAndUsers(tagId);
            if (tag == null)
                throw new InvalidInputException("Tag does not exist");

            if (!user.AnalogInputs.Any(input => input.Id == tagId))
                throw new ResourceNotFoundException("Tag does net exist");

            user.AnalogInputs.Remove(tag);
            await _ioAnalogDataRepository.DeleteByTagId(tagId);
            _userRepository.Update(user);
            List<User> users = await _userRepository.GetAllByCreatedBy(userId);
            foreach (User u in users)
            {
                u.AnalogInputs.Remove(tag);
                _userRepository.Update(user);
            }
            _analogInputRepository.Delete(tagId);
            foreach (var alarm in tag.Alarms)
            {
                await _alarmAlertRepository.DeleteByAlarmId(alarm.Id);
                _alarmRepository.Delete(alarm.Id);
            }
        }

        public async Task switchAnalogTag(Guid tagId, Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            AnalogInput analogInput = await _analogInputRepository.Read(tagId) ?? throw new ResourceNotFoundException("There is no analog tag with this id!");
            if (user.AnalogInputs.All(tag => tag.Id != tagId))
                throw new InvalidInputException("User cannot access other users tags!");
            analogInput.ScanOn = !analogInput.ScanOn;
            _analogInputRepository.Update(analogInput);
            if (analogInput.ScanOn)
                StartAnalogTagReading(tagId);
        }

        private async Task StartAnalogTagReading(Guid tagId)
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    AnalogInput analogInput = await _analogInputRepository.FindByIdWithAlarmsAndUsers(tagId);
                    if (analogInput == null)
                        break;
                    Console.WriteLine(analogInput.ScanOn);
                    if (analogInput.ScanOn)
                    {
                        Task<Device> deviceTask;
                        deviceTask = _deviceRepository.FindByAddress(analogInput.IOAddress);
                        Device device = deviceTask.Result;
                        Console.WriteLine(device.Value);
                        IOAnalogData ioAnalogData = new IOAnalogData
                        {
                            Id = new Guid(),
                            IOAddress = device.Address,
                            Value = device.Value,
                            Timestamp = DateTime.Now,
                            TagId = analogInput.Id
                        };
                        _ioAnalogDataRepository.Create(ioAnalogData);

                        foreach (Alarm analogInputAlarm in analogInput.Alarms)
                        {
                            if ((analogInputAlarm.Type == AlarmType.HIGH &&
                                 analogInputAlarm.EdgeValue < device.Value) ||
                                (analogInputAlarm.Type == AlarmType.LOW && analogInputAlarm.EdgeValue > device.Value))
                            {
                                AlarmAlert alarmAlert = new AlarmAlert
                                {
                                    Id = new Guid(),
                                    AlarmId = analogInputAlarm.Id,
                                    Timestamp = DateTime.Now,
                                    Value = device.Value
                                };
                                _alarmAlertRepository.Create(alarmAlert);
                                await Global._semaphoreWriter.WaitAsync();

                                try
                                {
                                    using (StreamWriter outputFile = new StreamWriter("alarmLog.txt", true))
                                    {
                                        await outputFile.WriteAsync("Alarm (id: " + alarmAlert.AlarmId + ") triggered for tag (id: " + ioAnalogData.TagId + ") at " + alarmAlert.Timestamp + "\n");
                                    }
                                }
                                finally
                                {
                                    Global._semaphoreWriter.Release();
                                }

                                AlarmReportsDTO report = new AlarmReportsDTO(analogInputAlarm, alarmAlert.Timestamp, device.Value);
                                await _alarmHub.Clients.Users(analogInput.Users.Select(u => u.Id.ToString()).ToList()).ReceiveAlarmData(report);
                            }
                        }

                        await _tagHub.Clients.Users(analogInput.Users.Select(u => u.Id.ToString()).ToList()).ReceiveAnalogData(ioAnalogData);
                    }
                    else
                    {
                        break;
                    }
                    Thread.Sleep(analogInput.ScanTime * 1000);

                }
            }).Start();
        }

        public async Task switchDigitalTag(Guid tagId, Guid userId)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            DigitalInput digitalInput = await _digitalInputRepository.Read(tagId) ?? throw new ResourceNotFoundException("There is no digital tag with this id!");
            if (user.DigitalInputs.All(tag => tag.Id != tagId))
                throw new InvalidInputException("User cannot access other users tags!");
            digitalInput.ScanOn = !digitalInput.ScanOn;
            _digitalInputRepository.Update(digitalInput);
            if (digitalInput.ScanOn)
                StartDigitalTagReading(tagId);
        }

        private async Task StartDigitalTagReading(Guid tagId)
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    DigitalInput digitalInput = await _digitalInputRepository.FindByIdWithUsers(tagId);
                    if (digitalInput == null)
                        break;

                    if (digitalInput.ScanOn)
                    {

                        Task<Device> deviceTask;
                        deviceTask = _deviceRepository.FindByAddress(digitalInput.IOAddress);
                        Device device = deviceTask.Result;
                        IODigitalData ioDigitalData = new IODigitalData
                        {
                            Id = new Guid(),
                            IOAddress = device.Address,
                            Value = device.Value,
                            Timestamp = DateTime.Now,
                            TagId = digitalInput.Id
                        };
                        _ioDigitalDataRepository.Create(ioDigitalData);

                        await _tagHub.Clients.Users(digitalInput.Users.Select(u => u.Id.ToString()).ToList()).ReceiveDigitalData(ioDigitalData);
                    }
                    else
                    {
                        break;
                    }
                    Thread.Sleep(digitalInput.ScanTime * 1000);

                }
            }).Start();
        }
    }
}
