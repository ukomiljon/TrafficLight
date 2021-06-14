using EventBus.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using MessageSenderHub;

namespace WestTrafficLight.EventBusConsumer
{
    public class TrafficLightStateConsumer : IConsumer<SignalStateEvent>
    {
        private readonly IMapper _mapper;
        private readonly IHubContext<CentralHub> _hub;
        
        public TrafficLightStateConsumer(IMapper mapper, IHubContext<CentralHub> hub)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
        }

        public async Task Consume(ConsumeContext<SignalStateEvent> context)
        {
            Console.WriteLine(context.Message.West.ToString());
            if (context.Message.ProccessCommand == ProccessCommand.None)
                await _hub.Clients.All.SendAsync("ReceiveMessage", "WestTrafficLight", context.Message.West.ToString());
        }
    }
}
