using System;
using System.Collections.Generic;
using FinancialCharts.Model;

namespace FinancialCharts.Repositories
{
    public interface IReadonlyExpDatesRepository
    {
        IEnumerable<ExpirationDate> GetDates();
        ExpirationDate GetDateById(int id);
    }
}