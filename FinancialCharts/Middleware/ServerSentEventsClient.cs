using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.EventSending;
using Microsoft.AspNetCore.Http;

namespace FinancialCharts.Middleware
{
    public class ServerSentEventsClient
    {
        private readonly HttpResponse _response;

        internal ServerSentEventsClient(HttpResponse response)
        {
            _response = response;
        }

        public Task SendEventAsync(ServerSentEvent ev)
        {
            return _response.WriteSseEventAsync(ev);
        }
    }
}
