using System;

namespace Common
{

    public interface ITrafficLightSettings
    {
        int StayRed { get; set; }
        int StayYellow { get; set; }
        int StayGreen { get; set; }
        int StayRightTurnGreen { get; set; }
    }

    public class TrafficLightSettings : ITrafficLightSettings
    {
        public int StayRed { get; set; }
        public int StayYellow { get; set; }
        public int StayGreen { get; set; }
        public int StayRightTurnGreen { get; set; }
    }
}
