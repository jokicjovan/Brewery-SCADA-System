using Brewery_SCADA_System.DTO;
using Microsoft.AspNetCore.Identity;

namespace Brewery_SCADA_System.Services
{
    public interface IAlarmService
    {
        Task makeAlarm(Guid userId, AlarmDTO alarmDto);
        Task deleteAlarm(Guid userId, Guid alarmId, Guid tagId);
    }
}
