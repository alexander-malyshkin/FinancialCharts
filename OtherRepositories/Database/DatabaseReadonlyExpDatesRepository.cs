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

        private List<ExpirationDate> datesList;

        public DatabaseReadonlyExpDatesRepository()
        {
            _connString = DatabaseHelper.GetConnStringAndProvider()["connString"];
            _provider = DatabaseHelper.GetConnStringAndProvider()["provider"];

            datesList = (List<ExpirationDate>)DatabaseHelper.GetDatabaseEntities(_connString, Entity.ExpirationDate);
            datesList.Sort();
        }


        public IEnumerable<ExpirationDate> GetDates()
        {
            return datesList;
        }

        public ExpirationDate GetDateById(int id)
        {
            return datesList.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerator<ExpirationDate> GetEnumerator()
        {
            return datesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => datesList.Count;

        public ExpirationDate this[int index] => datesList[index];
    }
}