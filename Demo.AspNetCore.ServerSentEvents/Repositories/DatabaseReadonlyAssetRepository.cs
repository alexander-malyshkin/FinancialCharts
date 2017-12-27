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

            PopulateAssetList();
        }

        public DatabaseReadonlyAssetRepository(string connString, string provider)
        {
            _connString = connString;
            _provider = provider;
            PopulateAssetList();
        }

        private void PopulateAssetList()
        {
            using (var dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = "select Id, Name" +
                                          "from dbo.Asset;";
                    var dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            int id = dataReader.GetInt32(0);
                            string name = dataReader.GetString(1);
                            var asset = new Asset(){Id=id, Name=name};
                            Assets.Add(asset);
                        }
                    }
                }
            }
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
