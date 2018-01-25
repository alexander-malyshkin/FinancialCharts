using System;
using System.Collections.Generic;
using System.Text;

namespace DB.Layer
{
    public class OptionComparer : IComparer<Option>
    {
        public int Compare(Option x, Option y)
        {
            int res;
            if (x == null || y == null)
                res = 0;
            else
            {
                res = x.Strike.CompareTo(y.Strike);
            }

            return res;
        }
    }
}