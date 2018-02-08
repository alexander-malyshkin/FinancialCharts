using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB.Layer;

namespace FinancialCharts.Model
{
    public class AssetModel
    {
        public List<Asset> AssetList { get; set; }

        public AssetModel()
        {
            //AssetList = new List<Asset>();
            try
            {
                using (var context = new FinancialChartsContext())
                {
                    AssetList = context.Asset.ToList();
                }
            }
            catch
            {
            }
        }
    }

    
}
