using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Hubs
{
    public interface IAlarmClient
    {
        Task ReceiveAlarmData(AlarmReportsDTO data);
    }
}
