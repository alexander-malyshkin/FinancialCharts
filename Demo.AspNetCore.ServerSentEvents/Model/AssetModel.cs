using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB.Layer;

namespace FinancialCharts.Model
{
    public class AssetModel
    {
        public int AssetId { get; set; }
        public List<Asset> AssetList { get; set; }
    }

    
}
