using Brewery_SCADA_System.DTO;
using Microsoft.AspNetCore.SignalR;

namespace Brewery_SCADA_System.Hubs
{
    public class AlarmHub : Hub<IAlarmClient>
    {
        public AlarmHub()
        {

        }
    }
}
