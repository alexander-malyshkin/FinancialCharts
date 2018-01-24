using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DB.Layer;
using FinancialCharts.Services;
using Lib.AspNetCore.ServerSentEvents;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Demo.AspNetCore.ServerSentEvents.Services
{
    internal class FinancialDataService : IHostedService
    {
        #region fields
        private const int  _interval = 7;
        private const int _seriesAmount = 4;
        private const int _seriesLength = 10;

        private FinancialData _finOptionsData;
        private readonly IServerSentEventsService _serverSentEventsService;
        private Task _finDataTask;
        private CancellationTokenSource _cancellationTokenSource;
        #endregion


        #region constructor

        public FinancialDataService(IServerSentEventsService service)
        {
            _serverSentEventsService = service;
            //_finOptionsData = DataSeriesHelper.GenerateDummySeries(_seriesAmount, _seriesLength);
        }

        public FinancialDataService(IServerSentEventsService service, FinancialData optionsData)
        {
            _serverSentEventsService = service;
            _finOptionsData = optionsData;
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
                _finOptionsData = new FinancialData();


                string jsonDataString = JsonConvert.SerializeObject(_finOptionsData);
                await _serverSentEventsService.SendEventAsync(jsonDataString);

                await Task.Delay(TimeSpan.FromSeconds(_interval), cancellationToken);
            }
        }

        #endregion
    }
}
