using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MachSecure.SVM.OverviewService.SignalR
{
    public class Broadcaster
    {
        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(5);
        public Timer _timer;
        private static readonly Lazy<Broadcaster> instance = new Lazy<Broadcaster>(() =>
            new Broadcaster(GlobalHost.ConnectionManager.GetHubContext<ExchangeHub>().Clients)
            );

        private Broadcaster(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            //timer goes here
            _timer = new Timer(TestBroadcast, null, _updateInterval, _updateInterval);
        }

        public static Broadcaster Instance => instance.Value;

        private IHubConnectionContext<dynamic> Clients
        {
            get;
        }

        private void TestBroadcast(object state)
        {
            if (ConnectionMapping.Count == 0)
                return;
            Clients.All.testBroadcast("hello");

        }
    }
}
