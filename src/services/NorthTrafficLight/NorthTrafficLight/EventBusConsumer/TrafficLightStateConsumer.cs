using AutoMapper;
using EventBus.Messages;
using MassTransit;
using MessageSenderHub;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace NorthTrafficLight.EventBusConsumer
{
    public class TrafficLightStateConsumer : IConsumer<SignalStateEvent>
    {

        private readonly IHubContext<CentralHub> _hub;

        public TrafficLightStateConsumer(IHubContext<CentralHub> hub)
        {
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
        }

        public async Task Consume(ConsumeContext<SignalStateEvent> context)
        {
            Console.WriteLine(context.Message.North.ToString());
            if (context.Message.ProccessCommand == ProccessCommand.None)
                await _hub.Clients.All.SendAsync("ReceiveMessage", "NorthTrafficLight", context.Message.North.ToString());
        }
    }
}
