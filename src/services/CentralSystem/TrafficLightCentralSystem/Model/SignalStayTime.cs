using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrafficLightCentralSystem.Model
{
    public class SignalStayTimeRequest
    {
        public int Red { get; set; }
        public int Yellow { get; set; }
        public int Green { get; set; }
        public int RightTurnGreen { get; set; }
    }
}
