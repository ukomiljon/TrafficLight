using System;

namespace EventBus.Messages
{     
    public class SignalStayTimeEvent  
    {
        public int Red { get; set; }
        public int Yellow { get; set; }
        public int Green { get; set; }
        public int RightTurnGreen { get; set; }
    }
}
