using System;
using System.Collections.Generic;
using System.Text;

namespace DB.Layer
{
    public class UserPreference
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string OpenTabs { get; set; }
        public string OpenDates { get; set; }
    }
}
