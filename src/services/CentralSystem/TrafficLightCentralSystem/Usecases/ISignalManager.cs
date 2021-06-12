using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrafficLightCentralSystem.Usecases
{
    public interface ISignalManager
    {
        public void Run();
        public void Stop();
        public void Reset();

        public void Error();
    }
}
