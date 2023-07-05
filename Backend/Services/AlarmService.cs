using System.Linq;
using System.Net;
using System.Security.Cryptography;
using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Exceptions;
using Brewery_SCADA_System.Models;
using Brewery_SCADA_System.Repository;

namespace Brewery_SCADA_System.Services
{ 
    public class AlarmService : IAlarmService
    {
        private readonly IAlarmRepository _alarmRepository;
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAlarmAlertRepository _alarmAlertRepository;
        
        public AlarmService(IAlarmRepository alarmRepository, IAnalogInputRepository analogInputRepository, IUserRepository userRepository, IAlarmAlertRepository alarmAlertRepository)
        {
            _alarmRepository = alarmRepository;
            _analogInputRepository = analogInputRepository;
            _userRepository = userRepository;
            _alarmAlertRepository = alarmAlertRepository;
        }


        public async Task makeAlarm(Guid userId, AlarmDTO alarmDto)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            AnalogInput analogInput = await _analogInputRepository.FindByIdWithAlarmsAndUsers(alarmDto.TagId) ?? throw new ResourceNotFoundException("There is no analog tag with this id!");
            if (user.AnalogInputs.All(tag => tag.Id != alarmDto.TagId))
                throw new InvalidInputException("User cannot access other users tags!");

            Alarm alarm = new Alarm
            {
                Id = new Guid(),
                EdgeValue = alarmDto.EdgeValue,
                Type = alarmDto.Type,
                Unit = alarmDto.Unit,
                Priority = alarmDto.Priority,
            };

            analogInput.Alarms.Add(alarm);
            _analogInputRepository.Update(analogInput);

        }

        public async Task deleteAlarm(Guid userId, Guid alarmId, Guid tagId)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            AnalogInput analogInput = await _analogInputRepository.FindByIdWithAlarmsAndUsers(tagId) ?? throw new ResourceNotFoundException("There is no analog tag with this id!");
            if (user.AnalogInputs.All(tag => tag.Id != tagId))
                throw new InvalidInputException("User cannot access other users tags!");

            await _alarmAlertRepository.DeleteByAlarmId(alarmId);
            analogInput.Alarms = analogInput.Alarms.Where(alarm => alarm.Id != alarmId).ToList();
            _analogInputRepository.Update(analogInput);
            _alarmRepository.Delete(alarmId);
        }

        public async Task<List<AlarmReportsDTO>> getAllAlarmsByTime(Guid userId, DateTime timeFrom, DateTime timeTo)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            List<AlarmReportsDTO> alarmReports = new List<AlarmReportsDTO>();
            foreach (AnalogInput analogInput in user.AnalogInputs)
            {
                foreach (Alarm analogInputAlarm in analogInput.Alarms)
                {
                    List<AlarmAlert> alarmAlerts = (List<AlarmAlert>)await _alarmAlertRepository.FindByIdByTime(analogInputAlarm.Id, timeFrom, timeTo);
                    List<AlarmReportsDTO> alarmReportsDtos = alarmAlerts.Select(item => new AlarmReportsDTO
                    {
                        AlarmId = analogInputAlarm.Id,
                        Type = analogInputAlarm.Type,
                        Priority = analogInputAlarm.Priority,
                        EdgeValue = analogInputAlarm.EdgeValue,
                        Unit = analogInputAlarm.Unit,
                        Timestamp = item.Timestamp
                    }).ToList();
                    alarmReports.AddRange(alarmReportsDtos);
                }
            }

            return alarmReports.OrderBy(item => item.Timestamp).ToList();
        }


        public async Task<List<AlarmReportsDTO>> getAllAlarmsByPriority(Guid userId, AlarmPriority priority)
        {
            User user = await _userRepository.FindByIdWithTags(userId) ?? throw new ResourceNotFoundException("User not found!");
            List<AlarmReportsDTO> alarmReports = new List<AlarmReportsDTO>();
            foreach (AnalogInput analogInput in user.AnalogInputs)
            {
                foreach (Alarm analogInputAlarm in analogInput.Alarms)
                {
                    if(analogInputAlarm.Priority != priority) continue;
                    List<AlarmAlert> alarmAlerts = (List<AlarmAlert>)await _alarmAlertRepository.FindByAlarmId(analogInputAlarm.Id);
                    List<AlarmReportsDTO> alarmReportsDtos = alarmAlerts.Select(item => new AlarmReportsDTO
                    {
                        AlarmId = analogInputAlarm.Id,
                        Type = analogInputAlarm.Type,
                        Priority = analogInputAlarm.Priority,
                        EdgeValue = analogInputAlarm.EdgeValue,
                        Unit = analogInputAlarm.Unit,
                        Timestamp = item.Timestamp
                    }).ToList();
                    alarmReports.AddRange(alarmReportsDtos);
                }
            }

            return alarmReports.OrderBy(item => item.Timestamp).ToList();
        }
    }
}
