using System;

namespace TrafficLightCentralSystem.Usecases
{
    public class PeakHour
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public int  StayTime { get; set; }
    }
}
