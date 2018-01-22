using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.Model;

namespace FinancialCharts.Repositories
{
    public interface IReadonlyAssetRepository
    {

        IEnumerable<Asset> GetAssets();
        Asset GetAssetById(int id);
    }
}
