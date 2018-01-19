using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.Model;
using FinancialCharts.Repositories.Database;

namespace FinancialCharts.Repositories.Random
{
    public class RandomSeriesGenerator : IReadonlySeriesRepository
    {
        private static int _seriesAmount = 5;
        private static int _seriesLength = 10;
        private string _connString;
        private string _provider;
        private List<DataSeries> seriesList;

        public RandomSeriesGenerator()
        {
            _connString = DatabaseHelper.GetConnStringAndProvider()["connString"];
            _provider = DatabaseHelper.GetConnStringAndProvider()["provider"];

            seriesList = GenerateDummySeries();
            seriesList.Sort();
        }
        private List<DataSeries> GenerateDummySeries()
        {
            List<DataSeries> res = new List<DataSeries>();

            var assetRepo = new DatabaseReadonlyAssetRepository();
            var datesRepo = new DatabaseReadonlyExpDatesRepository();
            var assets = assetRepo.GetAssets();
            var dates = datesRepo.GetDates();

            int seriesId = 0;
            foreach (var a in assets)
            {
                foreach (var d in dates)
                {
                    for (int i = 0; i < _seriesAmount; i++)
                    {
                        seriesId++;
                        string seriesName = "Series for asset " + a.Name + " and date " + d.DateString;
                        decimal[] strike = new decimal[_seriesLength];
                        decimal[] volatility = new decimal[_seriesLength];
                        for (int j = 0; j < _seriesLength; j++)
                        {
                            strike[j] = j;
                            volatility[j] = (decimal)(new System.Random()).NextDouble();
                        }

                        DataSeries dt = new DataSeries(
                            seriesId,
                            a.Id,
                            d.Id,
                            strike,
                            volatility,
                            seriesName
                            );
                        res.Add(dt);
                    }
                }
            }
            
            
            return res;
        }

        public IEnumerable<DataSeries> GetSeriesList() => seriesList;

        public DataSeries GetSeriesById(int id) => seriesList.FirstOrDefault(s => s.IndividualSeriesId == id);
    }
}
