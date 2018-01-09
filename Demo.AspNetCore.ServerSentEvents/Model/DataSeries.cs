using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCharts.Model
{
    public class DataSeries
    {
        public int Id { get; }
        public int AssetId { get; }
        public int ExpirationDateId { get; }
        public int IndividualSeriesId { get; }
        public decimal[] Strike { get; set; }
        public decimal[] Volatility { get; set; }
        public string Name { get; set; }
    }

    public static class DataSeriesHelper
    {
        public static List<DataSeries> GenerateDummySeries(int seriesAmount, int seriesLength)
        {
            List<DataSeries> res = new List<DataSeries>();
            for (int i = 0; i < seriesAmount; i++)
            {
                string name = "Series " + i;
                decimal[] volatility = new decimal[seriesLength];
                for (int j = 0; j < seriesLength; j++)
                {
                    volatility[j] = (decimal)(new Random()).NextDouble();
                }

                DataSeries dt = new DataSeries(){Name=name,Volatility=volatility};
                res.Add(dt);
            }
            return res;
        }
    }
}
