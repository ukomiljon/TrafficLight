using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        private static ServerConnector _eastTrafficLight;
        private static ServerConnector _westTrafficLight;
        private static ServerConnector _northTrafficLight;
        private static ServerConnector _southTrafficLight;

        static void Main(string[] args)
        {
            _eastTrafficLight = new ServerConnector("http://localhost:5001/east"); 
            _westTrafficLight = new ServerConnector("http://localhost:5002/west");
            _northTrafficLight = new ServerConnector("http://localhost:5009/north");
            _southTrafficLight = new ServerConnector("http://localhost:56625/south");

            var eastConnection = Task.Run(() => { _eastTrafficLight.Connect(); });
            var northConnection = Task.Run(() => { _northTrafficLight.Connect(); });
            var souththConnection = Task.Run(() => { _southTrafficLight.Connect(); });
            var westConnection = Task.Run(() => { _westTrafficLight.Connect(); });

            Task.WaitAll(new[] { eastConnection, souththConnection, northConnection, westConnection });
        }
    }
}
