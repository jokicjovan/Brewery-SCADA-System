﻿using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Brewery_SCADA_System.Hubs
{
    [Authorize]
    public class AlarmHub : Hub<IAlarmClient>
    {
        public AlarmHub()
        {

        }

        public async Task SendAlarmDataToAllClients(AlarmReportsDTO data)
        {
            await Clients.All.ReceiveAlarmData(data);
        }
    }
}
