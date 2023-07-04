using Brewery_SCADA_System.DTO;

namespace Brewery_SCADA_System.Hubs
{
    public interface IAlarmClient
    {
        Task ReceiveMessage(AlarmMessageDTO message);
    }
}
