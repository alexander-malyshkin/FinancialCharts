using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB.Layer;
using Newtonsoft.Json;

namespace FinancialCharts.Model
{
    public class UserPreferenceModel
    {
        public List<UserPreference> PreferenceList { get; set; }

        public UserPreferenceModel()
        {
            try
            {
                using (var ctx = new FinancialChartsContext())
                {
                    PreferenceList = ctx.UserPreferences.ToList();
                }
            }
            catch
            { }
        }

        public string PreferencesJsonString => JsonConvert.SerializeObject(PreferenceList);
    }
}
