using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCharts.Model
{
    public class DataSeries
    {
        public string name { get; set; }
        public float[] data { get; set; }

    }

    public static class DataSeriesHelper
    {
        public static List<DataSeries> GenerateDummySeries(int seriesAmount, int seriesLength)
        {
            List<DataSeries> res = new List<DataSeries>();
            for (int i = 0; i < seriesAmount; i++)
            {
                string name = "Series " + i;
                float[] data = new float[seriesLength];
                for (int j = 0; j < seriesLength; j++)
                {
                    data[j] = (float)(new Random()).NextDouble();
                }

                DataSeries dt = new DataSeries(){name=name,data=data};
                res.Add(dt);
            }
            return res;
        }
    }
}
