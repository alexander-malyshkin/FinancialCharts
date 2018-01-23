using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB.Layer;
using Newtonsoft.Json;

namespace FinancialCharts.Model
{
    public class OptionModel
    {
        public List<Option> OptionList { get; set; }

        public OptionModel()
        {
            using (var ctx = new FinancialChartsContext())
            {
                OptionList = ctx.Option.ToList();
            }
        }

        public string OptionsJsonString => JsonConvert.SerializeObject(OptionList);
    }
}
