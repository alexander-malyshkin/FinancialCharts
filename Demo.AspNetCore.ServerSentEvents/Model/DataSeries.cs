using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCharts.Model
{
    public class DataSeries
    {
        public static readonly string ListSeparator = ";";
        public int IndividualSeriesId { get; }
        public int AssetId { get; }
        public int ExpirationDateId { get; }
        public decimal[] Strike { get; set; }
        public decimal[] Volatility { get; set; }
        public string Name { get; set; }

        public DataSeries(int seriesId, int assetId, int dateId,
            decimal[] strikes, decimal[] vols, string name)
        {
            IndividualSeriesId = seriesId;
            AssetId = assetId;
            ExpirationDateId = dateId;
            Strike = strikes;
            Volatility = vols;
            Name = name;
        }

        public DataSeries(int seriesId, int assetId, int dateId, 
            string strikeList, string volatList, string name)
        {
            IndividualSeriesId = seriesId;
            AssetId = assetId;
            ExpirationDateId = dateId;
            Strike = ParseDecimalsList(strikeList);
            Volatility = ParseDecimalsList(volatList);
            Name = name;
        }

        private decimal[] ParseDecimalsList(string decimalsList)
        {
            throw new NotImplementedException();
        }
    }

    
}
