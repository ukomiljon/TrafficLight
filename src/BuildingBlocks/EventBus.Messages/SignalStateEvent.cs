using System;

namespace EventBus.Messages
{     
    public class SignalStateEvent  
    {
        public bool Red { get; set; }
        public bool Yellow { get; set; }
        public bool Green { get; set; }
        public bool RightTurnGreen { get; set; }
    }
}
