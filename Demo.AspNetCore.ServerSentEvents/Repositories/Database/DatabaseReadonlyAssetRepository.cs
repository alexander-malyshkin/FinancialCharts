using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.Model;
using System.Data.SqlClient;
using FinancialCharts.Repositories.Database;
using Microsoft.Extensions.Configuration;

namespace FinancialCharts.Repositories
{
    public class DatabaseReadonlyAssetRepository : IReadonlyAssetRepository, IReadOnlyList<Asset>
    {
        private string _connString;
        private string _provider;
        
        public List<Asset> Assets { get; }

        

        public DatabaseReadonlyAssetRepository()
        {
            _connString = DatabaseHelper.GetConnStringAndProvider()["connString"];
            _provider = DatabaseHelper.GetConnStringAndProvider()["provider"];

            Assets = (List<Asset>)DatabaseHelper.GetDatabaseEntities(_connString, Entity.Asset);
        }

        

        public IEnumerable<Asset> GetAssets()
        {
            return Assets;
        }

        public Asset GetAssetById(int id)
        {
            return Assets.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerator<Asset> GetEnumerator()
        {
            return Assets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => Assets.Count;

        public Asset this[int index] => Assets[index];
    }
}
