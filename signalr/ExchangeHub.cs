using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MachSecure.SVM.OverviewService.SignalR
{
    public class ExchangeHub : Hub
    {
        /// <summary>
        /// Instatiations
        /// </summary>
        private readonly Broadcaster broadcaster;
        private static readonly ConnectionMapping _connections = new ConnectionMapping();
        public static List<DeviceList> userList = new List<DeviceList>();

        public ExchangeHub()
            :this(Broadcaster.Instance)
        {
            
        }

        private ExchangeHub(Broadcaster broadcaster)
        {
            this.broadcaster = broadcaster;
        }

        /// <summary>
        /// HubMethods 
        /// </summary>

        [HubMethodName("test")]
        public string Test()
        {
            const string message = "hello";  
            Console.WriteLine("sending on the test thread");          
            return message;
        }

        public Task Subscribe(string group)
        {
            Console.WriteLine("[{0}] Client '{1}' has been added to group {2}.", DateTime.Now.ToString("dd-mm-yyyy hh:MM:ss"), Context.ConnectionId, group);
            var adv = new DeviceList()
            {
                Name = @group,
                User = Context.ConnectionId
            };
            if (userList.Contains(adv))
                Groups.Remove(Context.ConnectionId, group);

            userList.Add(adv);
            Console.WriteLine("user list contains this many people on subscription" + userList.Count());
            return Groups.Add(Context.ConnectionId, group);
        }

        public Task Unsubscribe(string group)
        {
            if (userList.Count > 0)
            {
                Console.WriteLine("[{0}] Client '{1}' has been unsubscribed from group {2}.", DateTime.Now.ToString("dd-mm-yyyy hh:MM:ss"), Context.ConnectionId, group);
                var itemToRemove = userList.Single(x => x.User == Context.ConnectionId);
                userList.Remove(itemToRemove);
                Console.WriteLine("user list contains this many people on unsubscription" + userList.Count());
            }

            return Groups.Remove(Context.ConnectionId, group);
        }

        public override Task OnConnected()
        {
            ConnectionMapping.Add(Context.ConnectionId);
            Console.WriteLine("[{0}] Client '{1}' connected.", DateTime.Now.ToString("dd-mm-yyyy hh:MM:ss"), Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (userList.Count > 0)
            {


                var itemToRemove = userList.Single(x => x.User == Context.ConnectionId);
                userList.Remove(itemToRemove);
                Console.WriteLine("user list contains this many people on unsubscription" + userList.Count());
                Console.WriteLine("[{0}] Client '{1}' disconnected.", DateTime.Now.ToString("dd-mm-yyyy hh:MM:ss"), Context.ConnectionId);

            }
            ConnectionMapping.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            if (!ConnectionMapping.GetConnections().Contains(Context.ConnectionId))
            {
                ConnectionMapping.Add(Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}
