﻿using AutoMapper;
using EventBus.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficLightCentralSystem.Model.DTO;

namespace TrafficLightCentralSystem.Usecases
{
   
    public class SignalManager : ISignalManager
    {
        private readonly ILogger<SignalManager> _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly CommandRequest _request;

        public SignalManager(CommandRequest request,  IPublishEndpoint publishEndpoint)
        {
            _request = request;
            _publishEndpoint = publishEndpoint;
        }

        public void Error()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public async void Run()
        {
            var signal = new SignalStateEvent() { Green = true };
            await _publishEndpoint.Publish<SignalStateEvent>(signal); 
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
