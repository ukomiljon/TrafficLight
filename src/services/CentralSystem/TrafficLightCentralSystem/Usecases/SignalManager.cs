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

        public void Error()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public async void Run(TrafficLightIntersection trafficLightIntersection, CommandRequest request)
        {
            Clean();

            _stopProcess = false;

            var map = CreateMap(trafficLightIntersection);
            var queueLength = map.ElementAt(0).Value.Count;

            _task = Task.Run(() =>
           {
               var queueIndex = 0;
               while (!_stopProcess)
               {
                   var currentQueue = queueIndex % queueLength;

                   SendMessage(map.CreateMessage(currentQueue));
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
        private SignalStateEvent CreateMessage(int currentQueue)
        {
            throw new NotImplementedException();
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

        private SignalMap CreateMap(TrafficLightIntersection trafficLightIntersection)
        {
            var map = new SignalMap();

            var south = trafficLightIntersection.TrafficBounds.Find(_ => _.TrafficLightBound == TrafficLightBound.South);
            var north = trafficLightIntersection.TrafficBounds.Find(_ => _.TrafficLightBound == TrafficLightBound.North);
            var west = trafficLightIntersection.TrafficBounds.Find(_ => _.TrafficLightBound == TrafficLightBound.West);
            var east = trafficLightIntersection.TrafficBounds.Find(_ => _.TrafficLightBound == TrafficLightBound.East);

            var southQueue = new List<Signal>();
            var nourthQueue = new List<Signal>();
            var westQueue = new List<Signal>();
            var eastQueue = new List<Signal>();

            nourthQueue.Add(Signal.Red);
            southQueue.Add(Signal.Red);
            westQueue.Add(Signal.Red);
            eastQueue.Add(Signal.Red);
            map.MaxStayTime.Add(new int[] { south.NormalHour.Red, north.NormalHour.Red, west.NormalHour.Red, east.NormalHour.Red }.Max());
            map.MaxStayPickTime.Add(new int[] { south.PickHours[0].Red, north.PickHours[0].Red, west.PickHours[0].Red, east.PickHours[0].Red }.Max());

            var noralMaxStay = 0;
            var pickMaxStay = 0;
            if (north.NormalHour.RightTurnGreen > 0)
            {
                nourthQueue.Add(Signal.GreenAndRightArrowGreen);
                southQueue.Add(Signal.Red);
                westQueue.Add(Signal.Red);
                eastQueue.Add(Signal.Red);

                noralMaxStay = new int[] { south.NormalHour.Red, north.NormalHour.RightTurnGreen, west.NormalHour.Red, east.NormalHour.Red }.Max();
                pickMaxStay = new int[] { south.PickHours[0].Red, north.PickHours[0].RightTurnGreen, west.PickHours[0].Red, east.PickHours[0].Red }.Max();

                map.MaxStayTime.Add(noralMaxStay);
                map.MaxStayPickTime.Add(pickMaxStay);
            }

            if (south.NormalHour.RightTurnGreen > 0)
            {
                nourthQueue.Add(Signal.Red);
                southQueue.Add(Signal.GreenAndRightArrowGreen);
                westQueue.Add(Signal.Red);
                eastQueue.Add(Signal.Red);

                noralMaxStay = new int[] { south.NormalHour.RightTurnGreen, north.NormalHour.RightTurnGreen, west.NormalHour.Red, east.NormalHour.Red }.Max();
                pickMaxStay = new int[] { south.PickHours[0].RightTurnGreen, north.PickHours[0].Red, west.PickHours[0].Red, east.PickHours[0].Red }.Max();

                map.MaxStayTime.Add(noralMaxStay);
                map.MaxStayPickTime.Add(pickMaxStay);
            }

            nourthQueue.Add(Signal.Green);
            southQueue.Add(Signal.Green);
            westQueue.Add(Signal.Red);
            eastQueue.Add(Signal.Red);
            noralMaxStay = new int[] { south.NormalHour.Green, north.NormalHour.Green, west.NormalHour.Red, east.NormalHour.Red }.Max() - noralMaxStay;
            pickMaxStay = new int[] { south.PickHours[0].Green, north.PickHours[0].Green, west.PickHours[0].Red, east.PickHours[0].Red }.Max() - pickMaxStay;
            map.MaxStayTime.Add(noralMaxStay);
            map.MaxStayPickTime.Add(pickMaxStay);

            nourthQueue.Add(Signal.Yellow);
            southQueue.Add(Signal.Yellow);
            westQueue.Add(Signal.Red);
            eastQueue.Add(Signal.Red);
            map.MaxStayTime.Add(new int[] { south.NormalHour.Yellow, north.NormalHour.Yellow, west.NormalHour.Red, east.NormalHour.Red }.Max());
            map.MaxStayPickTime.Add(new int[] { south.PickHours[0].Yellow, north.PickHours[0].Yellow, west.PickHours[0].Red, east.PickHours[0].Red }.Max());

            nourthQueue.Add(Signal.Red);
            southQueue.Add(Signal.Red);
            westQueue.Add(Signal.Red);
            eastQueue.Add(Signal.Red);
            map.MaxStayTime.Add(new int[] { south.NormalHour.Red, north.NormalHour.Red, west.NormalHour.Red, east.NormalHour.Red }.Max());
            map.MaxStayPickTime.Add(new int[] { south.PickHours[0].Red, north.PickHours[0].Red, west.PickHours[0].Red, east.PickHours[0].Red }.Max());

            noralMaxStay = 0;
            pickMaxStay = 0;
            if (west.NormalHour.RightTurnGreen > 0)
            {
                nourthQueue.Add(Signal.Red);
                southQueue.Add(Signal.Red);
                westQueue.Add(Signal.GreenAndRightArrowGreen);
                eastQueue.Add(Signal.Red);

                noralMaxStay = new int[] { south.NormalHour.Red, north.NormalHour.Red, west.NormalHour.RightTurnGreen, east.NormalHour.Red }.Max();
                pickMaxStay = new int[] { south.PickHours[0].Red, north.PickHours[0].Red, west.PickHours[0].RightTurnGreen, east.PickHours[0].Red }.Max();

                map.MaxStayTime.Add(noralMaxStay);
                map.MaxStayPickTime.Add(pickMaxStay);
            }

            if (east.NormalHour.RightTurnGreen > 0)
            {
                nourthQueue.Add(Signal.Red);
                southQueue.Add(Signal.Red);
                westQueue.Add(Signal.GreenAndRightArrowGreen);
                eastQueue.Add(Signal.Red);

                noralMaxStay = new int[] { south.NormalHour.Red, north.NormalHour.Red, west.NormalHour.Red, east.NormalHour.RightTurnGreen }.Max();
                pickMaxStay = new int[] { south.PickHours[0].Red, north.PickHours[0].Red, west.PickHours[0].Red, east.PickHours[0].RightTurnGreen }.Max();

                map.MaxStayTime.Add(noralMaxStay);
                map.MaxStayPickTime.Add(pickMaxStay);
            }

            nourthQueue.Add(Signal.Red);
            southQueue.Add(Signal.Red);
            westQueue.Add(Signal.Green);
            eastQueue.Add(Signal.Green);
            noralMaxStay = new int[] { south.NormalHour.Red, north.NormalHour.Red, west.NormalHour.Green, east.NormalHour.Green }.Max() - noralMaxStay;
            pickMaxStay = new int[] { south.PickHours[0].Red, north.PickHours[0].Red, west.PickHours[0].Green, east.PickHours[0].Green }.Max() - pickMaxStay;
            map.MaxStayTime.Add(noralMaxStay);
            map.MaxStayPickTime.Add(pickMaxStay);

            nourthQueue.Add(Signal.Red);
            southQueue.Add(Signal.Red);
            westQueue.Add(Signal.Yellow);
            eastQueue.Add(Signal.Yellow);
            map.MaxStayTime.Add(new int[] { south.NormalHour.Red, north.NormalHour.Red, west.NormalHour.Yellow, east.NormalHour.Yellow }.Max());
            map.MaxStayPickTime.Add(new int[] { south.PickHours[0].Red, north.PickHours[0].Red, west.PickHours[0].Yellow, east.PickHours[0].Yellow }.Max());

            return map;
        }
    }

}
