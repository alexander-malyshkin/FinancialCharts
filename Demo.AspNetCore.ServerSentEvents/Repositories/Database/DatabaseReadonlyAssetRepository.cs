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
            _connString = (string)ConfigHelper.GetConfigValue("DatabaseConnectionString");
            _provider = (string)ConfigHelper.GetConfigValue("DatabaseProviderName");
            Assets = new List<Asset>();
            PopulateAssetList();
        }

        public DatabaseReadonlyAssetRepository(string connString, string provider)
        {
            _connString = DatabaseHelper.GetConnStringAndProvider()["connString"];
            _provider = DatabaseHelper.GetConnStringAndProvider()["provider"];

            Assets = new List<Asset>();
            Assets = (List<Asset>)DatabaseHelper.GetDatabaseEntities();
        }

        private void PopulateAssetList()
        {
            using (var dbConnection = new SqlConnection(_connString))
            {
                try
                {
                    dbConnection.Open();
                    string query = "select Id, Name " +
                                   "from dbo.Asset ;";
                    using (var command = new SqlCommand(query, dbConnection))
                    {
                        command.CommandType = CommandType.Text;
                        //command.CommandText = "select Id, Name " +
                        //                      "from dbo.Asset;";
                        using (var dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int id = dataReader.GetInt32(0);
                                    string name = dataReader.GetValue(1).ToString();
                                    var asset = new Asset() {Id = id, Name = name};
                                    Assets.Add(asset);
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
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
