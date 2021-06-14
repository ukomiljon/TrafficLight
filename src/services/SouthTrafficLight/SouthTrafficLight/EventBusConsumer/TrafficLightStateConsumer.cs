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

namespace SouthTrafficLight.EventBusConsumer
{
    public class TrafficLightStateConsumer : IConsumer<SignalStateEvent>
    {
        private readonly IMapper _mapper;
        private readonly IHubContext<CentralHub> _hub;
        private readonly ConnectionFactory _connectionFactory;

        public TrafficLightStateConsumer(IMapper mapper, IHubContext<CentralHub> hub)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));             
        }

        public async Task Consume(ConsumeContext<SignalStateEvent> context)
        {
            //SouthTrafficLight
            Console.WriteLine("SouthTrafficLight");
            await _hub.Clients.All.SendAsync("ReceiveMessage", "SouthTrafficLight", "1234");

        }
    }
}
