

namespace EventBus.Messages
{
    public enum Signal { Red, Yellow, Green, GreenAndRightArrowGreen, OnlyRightArrowGreen }
    public enum ProccessCommand { None, Run, Stop }
    public class SignalStateEvent
    {
        public SignalStateEvent()
        {
            ProccessCommand = ProccessCommand.None;
        }
        public Signal North { get; set; }
        public Signal South { get; set; }
        public Signal West { get; set; }
        public Signal East { get; set; }

        public ProccessCommand ProccessCommand { get; set; }
    }



}
