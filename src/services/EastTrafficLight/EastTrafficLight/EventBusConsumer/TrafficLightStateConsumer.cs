﻿using EventBus.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

namespace EastTrafficLight.EventBusConsumer
{
    public class TrafficLightStateConsumer : IConsumer<SignalStateEvent>
    {

        private readonly IMapper _mapper;

        public TrafficLightStateConsumer(IMapper mapper)
        {           
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<SignalStateEvent> context)
        {
            //EastTrafficLight
            Console.WriteLine("EastTrafficLight");
        }
    }
}
