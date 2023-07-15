using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Brewery_SCADA_System.Services
{
    public interface IAlarmService
    {
        Task makeAlarm(Guid userId, AlarmDTO alarmDto);
        Task deleteAlarm(Guid userId, Guid alarmId, Guid tagId);
        Task<List<AlarmReportsDTO>> getAllAlarmsByTime(Guid userId, DateTime timeFrom, DateTime timeTo);
        Task<List<AlarmReportsDTO>> getAllAlarmsByPriority(Guid userId, AlarmPriority priority);
    }
}
