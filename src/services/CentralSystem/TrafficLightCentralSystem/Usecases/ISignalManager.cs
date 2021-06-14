using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficLightCentralSystem.Model.DTO;

namespace TrafficLightCentralSystem.Usecases
{
    public interface ISignalManager
    {
        public void Run(TrafficLightIntersection trafficLightIntersection, CommandRequest request);
        public void Stop(); 
    }
}
