using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lib.AspNetCore.ServerSentEvents;
using Microsoft.Extensions.Hosting;

namespace Demo.AspNetCore.ServerSentEvents.Services
{
    internal class FinancialDataService : IHostedService
    {
        #region fields
        private const int  _interval = 7;
        private List<double> _finDataList;
        private readonly IServerSentEventsService _serverSentEventsService;
        private Task _finDataTask;
        private CancellationTokenSource _cancellationTokenSource;
        #endregion


        #region constructor

        public FinancialDataService(IServerSentEventsService service)
        {
            _serverSentEventsService = service;
            _finDataList = new List<double>(){1.1, 2.4, 3.5, 6.7, 2.2, 1.7};
        }

        public FinancialDataService(IServerSentEventsService service, IList<double> data)
        {
            _serverSentEventsService = service;
            _finDataList = data.ToList();
        }
        #endregion

        #region methods
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            _finDataTask = FinDataAsync(_cancellationTokenSource.Token);

            return _finDataTask.IsCompleted ? _finDataTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_finDataTask != null)
            {
                _cancellationTokenSource.Cancel();

                await Task.WhenAny(_finDataTask, Task.Delay(-1, cancellationToken));

                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        private async Task FinDataAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                RandomizeDataSeries();
                
                //await _serverSentEventsService.SendEventAsync(finDataEvent);
                string jsonDataString =
                    "[{"
                    + "    \"name\": \"Brands\","
                    + "    \"colorByPoint\": true,"
                    + "    \"data\":"
                    + "        ["
                    + "          {"
                    + "              \"name\": \"tool 1\","
                    + "              \"y\": " + _finDataList[0]
                    + "          },"
                    + "          {"
                    + "              \"name\": \"tool 2\","
                    + "              \"y\":" + _finDataList[1] + ","
                    + "              \"sliced\": true,"
                    + "              \"selected\": true"
                    + "          },"
                    + "          {"
                    + "              \"name\": \"tool 3\","
                    + "              \"y\": " + _finDataList[2]
                    + "          },"
                    + "          {"
                    + "              \"name\": \"tool 4\","
                    + "              \"y\": " + _finDataList[3]
                    + "          },"
                    + "          {"
                    + "              \"name\": \"tool 5\","
                    + "              \"y\": " + _finDataList[4]
                    + "          },"
                    + "          {"
                    + "              \"name\": \"tool 6\","
                    + "              \"y\": " + _finDataList[5]
                    + "          }"
                    + "     ]"
                    + "}]";
                //string jsonDataString2 = 
                await _serverSentEventsService.SendEventAsync(jsonDataString);

                await Task.Delay(TimeSpan.FromSeconds(_interval), cancellationToken);
            }
        }

        private void RandomizeDataSeries()
        {
            for (int i = 0; i < _finDataList.Count; i++)
            {
                var r = new Random();
                _finDataList[i] = r.NextDouble();
            }
        }
        #endregion
    }
}
