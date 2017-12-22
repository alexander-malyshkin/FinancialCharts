using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.Model;
using System.Data.SqlClient;
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
            _connString = (string)ConfigHelper.GetConfigValue("DatabaseConnectionString");
            _provider = (string)ConfigHelper.GetConfigValue("DatabaseProviderName");

            using (var dbConnection = new SqlConnection(_connString))
            {
                try
                {
                    dbConnection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public DatabaseReadonlyAssetRepository(string connString, string provider)
        {
            _connString = connString;
            _provider = provider;
        }


        public IEnumerable<Asset> GetAssets()
        {
            return Assets;
        }

        public Asset GetAssetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Asset> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get; }

        public Asset this[int index]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
