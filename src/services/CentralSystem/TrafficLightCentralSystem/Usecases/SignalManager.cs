using AutoMapper;
using EventBus.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficLightCentralSystem.Model.DTO;
using TrafficLightCentralSystem.Usecases.Rules;

namespace TrafficLightCentralSystem.Usecases
{

    public class SignalManager : ISignalManager
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private static bool _stopProcess;
        private object obj = new object();
        private Task _task;

        public SignalManager(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _stopProcess = true;
        }

        public async void Run(TrafficLightIntersection trafficLightIntersection, CommandRequest request)
        {
            Clean();

            _stopProcess = false;

            var map = new QueueBuilder(trafficLightIntersection).Build();
            var queueLength = map.ElementAt(0).Value.Count;

            _task = Task.Run(() =>
           {
               var queueIndex = 0;
               while (!_stopProcess)
               {
                   var currentQueue = queueIndex % queueLength;
                   var currentState = map.CreateMessage(currentQueue);

                   Console.WriteLine($"North: {currentState.North}, South: {currentState.South}, West:{currentState.West}, East:{currentState.East}");

                   SendMessage(currentState);
                   map.StayTime(currentQueue);

                   queueIndex++;
               }
           });

        }


        private void Clean()
        {
            _stopProcess = true;//stop  
            if (_task != null) _task.Dispose();
        }

        public async void Stop()
        {
            lock (obj)
            {
                _stopProcess = true;
            }
        }

        private async void SendMessage(SignalStateEvent signalStateEvent)
        {
            await _publishEndpoint.Publish<SignalStateEvent>(signalStateEvent);
        }
    }
}
