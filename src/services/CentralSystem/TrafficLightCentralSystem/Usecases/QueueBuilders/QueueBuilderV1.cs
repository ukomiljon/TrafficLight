using EventBus.Messages;
using System.Collections.Generic;
using System.Linq;
using TrafficLightCentralSystem.Model.DTO;

namespace TrafficLightCentralSystem.Usecases.Rules
{
    public class QueueBuilderV1
    {
        private TrafficLightIntersection _trafficLightIntersection;
        private TrafficLighBoundRequest _south;
        private TrafficLighBoundRequest _north;
        private TrafficLighBoundRequest _west;
        private TrafficLighBoundRequest _east;
        private SignalMap _map;
        public QueueBuilderV1(TrafficLightIntersection trafficLightIntersection)
        {
            _trafficLightIntersection = trafficLightIntersection;
            _south = trafficLightIntersection.TrafficBounds.Find(_ => _.TrafficLightBound == TrafficLightBound.South);
            _north = trafficLightIntersection.TrafficBounds.Find(_ => _.TrafficLightBound == TrafficLightBound.North);
            _west = trafficLightIntersection.TrafficBounds.Find(_ => _.TrafficLightBound == TrafficLightBound.West);
            _east = trafficLightIntersection.TrafficBounds.Find(_ => _.TrafficLightBound == TrafficLightBound.East);
            _map = new SignalMap();

            _map[TrafficLightBound.South] = new List<Signal>();
            _map[TrafficLightBound.North] = new List<Signal>();
            _map[TrafficLightBound.West] = new List<Signal>();
            _map[TrafficLightBound.East] = new List<Signal>();
        }

        public SignalMap Build()
        {                 //   S  N  W  E
            BuildQueue1();//1. R, R, R, R
            BuildQueue2();//2,3. R, GRightArrow, R, R | G, G, R, R
            BuildQueue3();//4. Y, Y, R, R
            BuildQueue1();//5. R, R, R, R
            BuildQueue4();//6. R, R, G, G
            BuildQueue5();//7. R, R, Y, Y

            return _map;
        }

        private void BuildQueue1()
        {
            _map[TrafficLightBound.North].Add(Signal.Red);
            _map[TrafficLightBound.South].Add(Signal.Red);
            _map[TrafficLightBound.West].Add(Signal.Red);
            _map[TrafficLightBound.East].Add(Signal.Red);
            _map.MaxStayTime.Add(new int[] { _south.NormalHour.Red, _north.NormalHour.Red, _west.NormalHour.Red, _east.NormalHour.Red }.Max());
            _map.MaxStayPickTime.Add(new int[] { _south.PickHours[0].Red, _north.PickHours[0].Red, _west.PickHours[0].Red, _east.PickHours[0].Red }.Max());
        }
        private void BuildQueue2()
        {
            var noralMaxStay = 0;
            var pickMaxStay = 0;
            if (_north.NormalHour.RightTurnGreen > 0)
            {
                _map[TrafficLightBound.North].Add(Signal.GreenAndRightArrowGreen);
                _map[TrafficLightBound.South].Add(Signal.Red);
                _map[TrafficLightBound.West].Add(Signal.Red);
                _map[TrafficLightBound.East].Add(Signal.Red);

                noralMaxStay = new int[] { _south.NormalHour.Red, _north.NormalHour.RightTurnGreen, _west.NormalHour.Red, _east.NormalHour.Red }.Max();
                pickMaxStay = new int[] { _south.PickHours[0].Red, _north.PickHours[0].RightTurnGreen, _west.PickHours[0].Red, _east.PickHours[0].Red }.Max();

                _map.MaxStayTime.Add(noralMaxStay);
                _map.MaxStayPickTime.Add(pickMaxStay);
            }

            if (_south.NormalHour.RightTurnGreen > 0)
            {
                _map[TrafficLightBound.North].Add(Signal.Red);
                _map[TrafficLightBound.South].Add(Signal.GreenAndRightArrowGreen);
                _map[TrafficLightBound.West].Add(Signal.Red);
                _map[TrafficLightBound.East].Add(Signal.Red);

                noralMaxStay = new int[] { _south.NormalHour.RightTurnGreen, _north.NormalHour.RightTurnGreen, _west.NormalHour.Red, _east.NormalHour.Red }.Max();
                pickMaxStay = new int[] { _south.PickHours[0].RightTurnGreen, _north.PickHours[0].Red, _west.PickHours[0].Red, _east.PickHours[0].Red }.Max();

                _map.MaxStayTime.Add(noralMaxStay);
                _map.MaxStayPickTime.Add(pickMaxStay);
            }

            _map[TrafficLightBound.North].Add(Signal.Green);
            _map[TrafficLightBound.South].Add(Signal.Green);
            _map[TrafficLightBound.West].Add(Signal.Red);
            _map[TrafficLightBound.East].Add(Signal.Red);
            noralMaxStay = new int[] { _south.NormalHour.Green, _north.NormalHour.Green, _west.NormalHour.Red, _east.NormalHour.Red }.Max() - noralMaxStay;
            pickMaxStay = new int[] { _south.PickHours[0].Green, _north.PickHours[0].Green, _west.PickHours[0].Red, _east.PickHours[0].Red }.Max() - pickMaxStay;
            _map.MaxStayTime.Add(noralMaxStay);
            _map.MaxStayPickTime.Add(pickMaxStay);
        }
        private void BuildQueue3()
        {
            _map[TrafficLightBound.North].Add(Signal.Yellow);
            _map[TrafficLightBound.South].Add(Signal.Yellow);
            _map[TrafficLightBound.West].Add(Signal.Red);
            _map[TrafficLightBound.East].Add(Signal.Red);
            _map.MaxStayTime.Add(new int[] { _south.NormalHour.Yellow, _north.NormalHour.Yellow, _west.NormalHour.Red, _east.NormalHour.Red }.Max());
            _map.MaxStayPickTime.Add(new int[] { _south.PickHours[0].Yellow, _north.PickHours[0].Yellow, _west.PickHours[0].Red, _east.PickHours[0].Red }.Max());

        }
        private void BuildQueue4()
        {

            var noralMaxStay = 0;
            var pickMaxStay = 0;
            if (_west.NormalHour.RightTurnGreen > 0)
            {
                _map[TrafficLightBound.North].Add(Signal.Red);
                _map[TrafficLightBound.South].Add(Signal.Red);
                _map[TrafficLightBound.West].Add(Signal.GreenAndRightArrowGreen);
                _map[TrafficLightBound.East].Add(Signal.Red);

                noralMaxStay = new int[] { _south.NormalHour.Red, _north.NormalHour.Red, _west.NormalHour.RightTurnGreen, _east.NormalHour.Red }.Max();
                pickMaxStay = new int[] { _south.PickHours[0].Red, _north.PickHours[0].Red, _west.PickHours[0].RightTurnGreen, _east.PickHours[0].Red }.Max();

                _map.MaxStayTime.Add(noralMaxStay);
                _map.MaxStayPickTime.Add(pickMaxStay);
            }

            if (_east.NormalHour.RightTurnGreen > 0)
            {
                _map[TrafficLightBound.North].Add(Signal.Red);
                _map[TrafficLightBound.South].Add(Signal.Red);
                _map[TrafficLightBound.West].Add(Signal.GreenAndRightArrowGreen);
                _map[TrafficLightBound.East].Add(Signal.Red);

                noralMaxStay = new int[] { _south.NormalHour.Red, _north.NormalHour.Red, _west.NormalHour.Red, _east.NormalHour.RightTurnGreen }.Max();
                pickMaxStay = new int[] { _south.PickHours[0].Red, _north.PickHours[0].Red, _west.PickHours[0].Red, _east.PickHours[0].RightTurnGreen }.Max();

                _map.MaxStayTime.Add(noralMaxStay);
                _map.MaxStayPickTime.Add(pickMaxStay);
            }

            _map[TrafficLightBound.North].Add(Signal.Red);
            _map[TrafficLightBound.South].Add(Signal.Red);
            _map[TrafficLightBound.West].Add(Signal.Green);
            _map[TrafficLightBound.East].Add(Signal.Green);
            noralMaxStay = new int[] { _south.NormalHour.Red, _north.NormalHour.Red, _west.NormalHour.Green, _east.NormalHour.Green }.Max() - noralMaxStay;
            pickMaxStay = new int[] { _south.PickHours[0].Red, _north.PickHours[0].Red, _west.PickHours[0].Green, _east.PickHours[0].Green }.Max() - pickMaxStay;
            _map.MaxStayTime.Add(noralMaxStay);
            _map.MaxStayPickTime.Add(pickMaxStay);
        }
        private void BuildQueue5()
        {
            _map[TrafficLightBound.North].Add(Signal.Red);
            _map[TrafficLightBound.South].Add(Signal.Red);
            _map[TrafficLightBound.West].Add(Signal.Yellow);
            _map[TrafficLightBound.East].Add(Signal.Yellow);
            _map.MaxStayTime.Add(new int[] { _south.NormalHour.Red, _north.NormalHour.Red, _west.NormalHour.Yellow, _east.NormalHour.Yellow }.Max());
            _map.MaxStayPickTime.Add(new int[] { _south.PickHours[0].Red, _north.PickHours[0].Red, _west.PickHours[0].Yellow, _east.PickHours[0].Yellow }.Max());
        }
    }
}
