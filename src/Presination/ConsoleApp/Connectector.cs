using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace ConsoleApp
{
    public class ServerConnector
    {
        private string _connectionUrl;

        public ServerConnector(string connectionUrl)
        {
            _connectionUrl = connectionUrl;
        }
        public void Connect()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl(_connectionUrl)
                .Build();

            connection.StartAsync().Wait();
            connection.InvokeCoreAsync("SendMessage", args: new[] { "Client", $"I am listening {_connectionUrl}" });
            connection.On("ReceiveMessage", (string trafficLight, string state) =>
            {
                Console.WriteLine(trafficLight + ":" + state);
                timer.Restart();
            });

            Console.ReadKey();
        }

        private static Timer timer = new Timer();
    }
}
