using System;
using System.Collections.Generic;
using System.Globalization;

namespace DB.Layer
{
    public partial class Option
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Strike { get; set; }
        public DateTime ExpDate { get; set; }
        public int BaseAssetId { get; set; }
        public bool Type { get; set; }

        public Asset BaseAsset { get; set; }

        public string DateString
        {
            get
            {
                CultureInfo cltr = new CultureInfo("ru-RU");
                return ExpDate.ToString("D", cltr);
            }
        }
        
    }
}
