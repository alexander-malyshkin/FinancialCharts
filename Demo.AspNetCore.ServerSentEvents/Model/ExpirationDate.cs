using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCharts.Model
{
    public class ExpirationDate
    {
        public int Id { get; set; }
        public DateTime ExpDate { get; set; }
        public int AssetId { get; set; }
    }
}
