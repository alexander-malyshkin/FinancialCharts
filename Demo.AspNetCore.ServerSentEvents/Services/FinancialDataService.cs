using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinancialCharts.Model;
using Lib.AspNetCore.ServerSentEvents;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Demo.AspNetCore.ServerSentEvents.Services
{
    internal class FinancialDataService : IHostedService
    {
        #region fields
        private const int  _interval = 50;
        private const int _seriesAmount = 4;
        private const int _seriesLength = 10;

        private List<DataSeries> _finDataList;
        private readonly IServerSentEventsService _serverSentEventsService;
        private Task _finDataTask;
        private CancellationTokenSource _cancellationTokenSource;
        #endregion


        #region constructor

        public FinancialDataService(IServerSentEventsService service)
        {
            _serverSentEventsService = service;
            //_finDataList = DataSeriesHelper.GenerateDummySeries(_seriesAmount, _seriesLength);
        }

        public FinancialDataService(IServerSentEventsService service, List<DataSeries> dataSeries)
        {
            _serverSentEventsService = service;
            _finDataList = dataSeries;
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
                _finDataList = DataSeriesHelper.GenerateDummySeries(_seriesAmount, _seriesLength);
                string jsonDataString = JsonConvert.SerializeObject(_finDataList);
                await _serverSentEventsService.SendEventAsync(jsonDataString);

                await Task.Delay(TimeSpan.FromSeconds(_interval), cancellationToken);
            }
        }

        #endregion
    }
}
