using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.Repositories.Database;

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
            Name = name;
            Strike = strikes;
            Volatility = vols;
            
        }

        public DataSeries(int seriesId, int assetId, int dateId, string name)
        {
            IndividualSeriesId = seriesId;
            AssetId = assetId;
            ExpirationDateId = dateId;
            Name = name;
            var _connString = DatabaseHelper.GetConnStringAndProvider()["connString"];
            Strike = GetStrikeList(seriesId, _connString);
            Volatility = GetVolatilityList(seriesId, _connString);
            
        }

        private decimal[] GetVolatilityList(int seriesId, string connString)
        {
            var volatList = (List<decimal>) DatabaseHelper.GetDatabaseEntities(connString, Entity.Volatility, "where IndividualSeriesId = " + seriesId);
            return volatList.ToArray();
        }

        private decimal[] GetStrikeList(int seriesId, string connString)
        {
            var strikeList = (List<decimal>)DatabaseHelper.GetDatabaseEntities(connString, Entity.Strike, "where IndividualSeriesId = " + seriesId);
            return strikeList.ToArray();
        }
    }

    
}
