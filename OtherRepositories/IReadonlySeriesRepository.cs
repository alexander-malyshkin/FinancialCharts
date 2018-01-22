using System.Collections.Generic;
using FinancialCharts.Model;

namespace FinancialCharts.Repositories
{
    public interface IReadonlySeriesRepository
    {
        IEnumerable<DataSeries> GetSeriesList();
        DataSeries GetSeriesById(int id);
    }
}