using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FinancialCharts.Middleware
{
    public class ServerSentEventsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ServerSentEventsService _serverSentEventsService;

        public ServerSentEventsMiddleware(RequestDelegate next, ServerSentEventsService service)
        {
            _next = next;
            _serverSentEventsService = service;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Headers["Accept"] == "text/event-stream")
            {
                context.Response.ContentType = "text/event-stream";
                context.Response.Body.Flush();

                ServerSentEventsClient client = new ServerSentEventsClient(context.Response);
                Guid clientId = _serverSentEventsService.AddClient(client);

                context.RequestAborted.WaitHandle.WaitOne();

                _serverSentEventsService.RemoveClient(clientId);

                return Task.FromResult(true);
            }
            else
            {
                return _next(context);
            }

        }
    }
}
