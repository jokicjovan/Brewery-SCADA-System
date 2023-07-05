using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;
using Microsoft.AspNetCore.SignalR;

namespace Brewery_SCADA_System.Hubs
{
    public class AlarmHub : Hub<IAlarmClient>
    {
        public AlarmHub()
        {

        }

        public async Task SendDataToAllClients(AlarmAlert data)
        {
            await Clients.All.ReceiveData(data);
        }
    }
}
