using EventBus.Messages;
using System;
using System.Collections.Generic;
using System.Threading;
using TrafficLightCentralSystem.Model.DTO;

namespace TrafficLightCentralSystem.Usecases
{
    public class SignalMap : Dictionary<TrafficLightBound, List<Signal>>
    {
        public List<int> MaxStayTime { get; set; }
        public List<int> MaxStayPickTime { get; set; }
        public List<PeakHour> PickHours { get; set; }

        public SignalMap()
        {
            PickHours = new List<PeakHour>();
            MaxStayTime = new List<int>();
            MaxStayPickTime = new List<int>();
        }
       

        public void StayTime(int queueIndex)
        {
            if (IsPickHours())
            {
                Thread.Sleep(MaxStayPickTime[queueIndex] * 1000);
                return;
            }

            Thread.Sleep(MaxStayTime[queueIndex]*1000);
        }


        private bool IsPickHours()
        {
            foreach (var pickHour in PickHours)
            {
                if (pickHour.Start <= DateTime.Now.TimeOfDay
                   && pickHour.End > DateTime.Now.TimeOfDay)
                    return true;
            }

            return false;
        }

        public SignalStateEvent CreateMessage(int queueIndex)
        {
            return new SignalStateEvent()
            {
                West = this[TrafficLightBound.West][queueIndex],
                East = this[TrafficLightBound.East][queueIndex],
                North = this[TrafficLightBound.North][queueIndex],
                South = this[TrafficLightBound.South][queueIndex],
            };
        }
    }
}
