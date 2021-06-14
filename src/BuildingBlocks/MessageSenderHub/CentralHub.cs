using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace MessageSenderHub
{
    public class CentralHub:Hub
    {
        public async Task SendMessage(string trafficLight, string state)
        {
            await Clients.All.SendAsync("ReceiveMessage", trafficLight, state); 
        }

        public async void Notify(string trafficLight, string status)
        {
            await Clients.All.SendAsync("ReceiveMessage", trafficLight, status);
        }
    }
}
