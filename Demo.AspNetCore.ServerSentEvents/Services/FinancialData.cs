using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DB.Layer;

namespace FinancialCharts.Services
{
    internal class FinancialData
    {
        private Dictionary<int, decimal> _data;

        public Dictionary<int, decimal> Data => _data;

        public FinancialData()
        {
            _data = new Dictionary<int, decimal>();
            List<Option> options;
            using (var dbContext = new FinancialChartsContext())
            {
                options = dbContext.Option.ToList();
            }

            foreach (var o in options)
            {
                var optionId = o.Id;
                var volat = VolatilityHelper.GetVolatility(optionId);
                _data.Add(optionId, volat);
            }

        }
    }
}
