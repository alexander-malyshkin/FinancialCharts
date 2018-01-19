using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.Model;

namespace FinancialCharts.Repositories.Database
{
    public class DatabaseReadonlySeriesRepository : IReadonlySeriesRepository
    {
        private string _connString;
        private string _provider;
        private List<DataSeries> seriesList;

        public DatabaseReadonlySeriesRepository()
        {
            _connString = DatabaseHelper.GetConnStringAndProvider()["connString"];
            _provider = DatabaseHelper.GetConnStringAndProvider()["provider"];

            seriesList = (List<DataSeries>)DatabaseHelper.GetDatabaseEntities(_connString, Entity.DataSeries);
        }

        public IEnumerable<DataSeries> GetSeriesList() => seriesList;

        public DataSeries GetSeriesById(int id) => seriesList.FirstOrDefault(s => s.IndividualSeriesId == id);
        
    }
}
