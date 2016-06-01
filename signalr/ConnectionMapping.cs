using System.Collections.Generic;
using System.Linq;

namespace MachSecure.SVM.OverviewService.SignalR
{
    public class ConnectionMapping
    {
        private static readonly List<string> Connections =
            new List<string>();

        public static int Count
        {
            get
            {
                return Connections.Count;
            }
        }

        public static void Add(string connectionId)
        {
            lock (Connections)
            {
                if (Connections.All(x => x != connectionId))
                {
                    Connections.Add(connectionId);
                }
            }
        }

        public static IEnumerable<string> GetConnections()
        {
            return Connections;
        }

        public static void Remove(string connectionId)
        {
            lock (Connections)
            {
                if (Connections.Any(x => x == connectionId))
                {
                    Connections.Remove(connectionId);
                }
            }
        }
    }
}
