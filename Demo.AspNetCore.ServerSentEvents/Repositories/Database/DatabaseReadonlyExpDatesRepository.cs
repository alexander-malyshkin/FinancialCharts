using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.Model;

namespace FinancialCharts.Repositories
{
    public class DatabaseReadonlyExpDatesRepository : IReadonlyExpDatesRepository, IReadOnlyList<ExpirationDate>
    {
        private string _connString;
        private string _provider;

        public List<ExpirationDate> DatesList { get; }

        public DatabaseReadonlyExpDatesRepository()
        {
            _connString = (string)ConfigHelper.GetConfigValue("DatabaseConnectionString");
            _provider = (string)ConfigHelper.GetConfigValue("DatabaseProviderName");
            DatesList = new List<ExpirationDate>();
            PopulateDatesList();
        }

        private void PopulateDatesList()
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
                                    var asset = new Asset() { Id = id, Name = name };
                                    DatesList.Add(asset);
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


        public IEnumerable<ExpirationDate> GetSeries()
        {
            throw new NotImplementedException();
        }

        public ExpirationDate GetExpDateById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ExpirationDate> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get; }

        public ExpirationDate this[int index]
        {
            get { throw new NotImplementedException(); }
        }
    }
}