using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCharts.Model
{
    public class CompositeModel
    {
        public AssetModel assetModel { get; set; }
        public OptionModel optionsModel { get; set; }
        public UserPreferenceModel userPreferenceModel { get; set; }

        public CompositeModel()
        {
            assetModel = new AssetModel();
            optionsModel = new OptionModel();
            userPreferenceModel = new UserPreferenceModel();
        }
    }
}
