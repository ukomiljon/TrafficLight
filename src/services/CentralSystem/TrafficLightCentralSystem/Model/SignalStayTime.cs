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
