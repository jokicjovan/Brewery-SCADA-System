using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Brewery_SCADA_System.Hubs
{
    [Authorize]
    public class TagHub : Hub<ITagClient>
    {
        public TagHub()
        {

        }

        public async Task SendAnalogDataToAllClients(IOAnalogData data)
        {
            await Clients.All.ReceiveAnalogData(data);
        }

        public async Task SendDigitalDataToAllClients(IODigitalData data)
        {
            await Clients.All.ReceiveDigitalData(data);
        }
    }
}
