using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.EventSending;

namespace FinancialCharts.Middleware
{
    public class ServerSentEventsService
    {
        private readonly ConcurrentDictionary<Guid, ServerSentEventsClient> _clients =
            new ConcurrentDictionary<Guid, ServerSentEventsClient>();

        internal Guid AddClient(ServerSentEventsClient client)
        {
            var guid = Guid.NewGuid();
            _clients.TryAdd(guid, client);
            return guid;
        }

        internal void RemoveClient(Guid clientId)
        {
            ServerSentEventsClient client;

            _clients.TryRemove(clientId, out client);
        }

        public Task SendEventAsync(ServerSentEvent ev)
        {
            List<Task> clientsTasks = new List<Task>();
            foreach (var client in _clients.Values)
            {
                clientsTasks.Add(client.SendEventAsync(ev));
            }
            return Task.WhenAll(clientsTasks);
        }
    }
}
