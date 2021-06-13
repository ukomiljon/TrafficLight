using EventBus.Messages;
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
        //private readonly IMediator _mediator;
        //private readonly IMapper _mapper;

        //public TrafficLightStateConsumer(IMediator mediator, IMapper mapper)
        //{
        //    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        //    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        //}

        public async Task Consume(ConsumeContext<SignalStateEvent> context)
        {

                         
        }
    }
}
