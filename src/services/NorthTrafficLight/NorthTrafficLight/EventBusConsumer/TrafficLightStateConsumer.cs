﻿using EventBus.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
 
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using MessageSenderHub;

namespace NorthTrafficLight.EventBusConsumer
{
    public class TrafficLightStateConsumer : IConsumer<SignalStateEvent>
    {

        private readonly IMapper _mapper;
        private readonly   IHubContext<CentralHub> _hub;      

        public TrafficLightStateConsumer(IMapper mapper, IHubContext<CentralHub> hub)
        {           
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _hub = hub ?? throw new ArgumentNullException(nameof(hub)); 
        }

        public async Task Consume(ConsumeContext<SignalStateEvent> context)
        {             
            Console.WriteLine("NorthTrafficLight");
            await _hub.Clients.All.SendAsync("ReceiveMessage", "NorthTrafficLight", "1234");
        }
    }
}
