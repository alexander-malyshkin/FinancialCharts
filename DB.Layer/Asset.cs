using System;
using System.Collections.Generic;

namespace DB.Layer
{
    public partial class Asset
    {
        public Asset()
        {
            Option = new HashSet<Option>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<Option> Option { get; set; }
    }
}
