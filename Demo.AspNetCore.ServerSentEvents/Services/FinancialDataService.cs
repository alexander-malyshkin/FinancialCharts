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
        private const int  _interval = 1;
        private const int _seriesAmount = 4;
        private const int _seriesLength = 10;

        private List<OptionData> _finOptionsData;
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

        public FinancialDataService(IServerSentEventsService service, List<OptionData> optionsData)
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
                _finOptionsData = new List<OptionData>();
                List<Option> options = null;

                // retrieve option list from database
                using (var ctx = new FinancialChartsContext())
                {
                    options = ctx.Option.ToList();
                }
                // sort the options by Strike values
                var strikeComparer = new OptionComparer();
                options.Sort(strikeComparer);

                foreach (var opt in options)
                {
                    var optionData = new OptionData(opt);
                    _finOptionsData.Add(optionData);
                }

                string jsonDataString = JsonConvert.SerializeObject(_finOptionsData);
                await _serverSentEventsService.SendEventAsync(jsonDataString);

                await Task.Delay(TimeSpan.FromSeconds(_interval), cancellationToken);
            }
        }

        #endregion
    }
}
