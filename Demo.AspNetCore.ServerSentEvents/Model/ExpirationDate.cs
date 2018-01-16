using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;

namespace FinancialCharts.Model
{
    public class ExpirationDate : IComparable<ExpirationDate>
    {
        public int Id { get; set; }
        public DateTime ExpDate { get; set; }
        public int AssetId { get; set; }

        public string DateString => ExpDate.ToShortDateString();

        public int CompareTo(ExpirationDate other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return ExpDate.CompareTo(other.ExpDate);
        }
    }

    public class ExpDatesModel
    {
        public string ExpDatesList { get; set; }
    }
}
