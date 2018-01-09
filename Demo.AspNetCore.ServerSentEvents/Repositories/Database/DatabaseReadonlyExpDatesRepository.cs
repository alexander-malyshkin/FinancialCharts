using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.Model;
using FinancialCharts.Repositories.Database;

namespace FinancialCharts.Repositories
{
    public class DatabaseReadonlyExpDatesRepository : IReadonlyExpDatesRepository, IReadOnlyList<ExpirationDate>
    {
        private string _connString;
        private string _provider;

        public List<ExpirationDate> DatesList { get; }

        public DatabaseReadonlyExpDatesRepository()
        {
            _connString = DatabaseHelper.GetConnStringAndProvider()["connString"];
            _provider = DatabaseHelper.GetConnStringAndProvider()["provider"];

            DatesList = (List<ExpirationDate>)DatabaseHelper.GetDatabaseEntities(_connString, Entity.ExpirationDate);
        }


        public IEnumerable<ExpirationDate> GetDates()
        {
            return DatesList;
        }

        public ExpirationDate GetDateById(int id)
        {
            return DatesList.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerator<ExpirationDate> GetEnumerator()
        {
            return DatesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => DatesList.Count;

        public ExpirationDate this[int index] => DatesList[index];
    }
}