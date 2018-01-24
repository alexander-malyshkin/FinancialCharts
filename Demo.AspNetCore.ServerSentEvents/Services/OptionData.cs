using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DB.Layer;

namespace FinancialCharts.Services
{
    internal class OptionData
    {
        private Option _option;
        private decimal _volatility;

        public Option Option => _option;
        public decimal Volatility => _volatility;

        public OptionData(Option opt)
        {
            _option = opt;
            var optionId = opt.Id;
            _volatility = VolatilityHelper.GetVolatility(optionId);
        }
    }
}
