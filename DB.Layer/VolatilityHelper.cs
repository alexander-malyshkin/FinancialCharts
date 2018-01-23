using System;
using System.Collections.Generic;
using System.Text;

namespace DB.Layer
{
    public static class VolatilityHelper
    {
        public static decimal GetVolatility(int optionId)
        {
            var rnd = new Random();
            return (decimal)rnd.NextDouble();
        }
    }
}
