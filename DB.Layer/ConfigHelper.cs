using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FinancialCharts
{
    public static class ConfigHelper
    {
        public static object GetConfigValue(string configName)
        {
            var builder = new ConfigurationBuilder();

            //builder.SetBasePath(Directory.GetCurrentDirectory())
             //   .AddJsonFile("appsettings.json");
            var configDictionary = builder.Build();

            return configDictionary[configName];
        }
    }
}
