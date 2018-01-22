using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB.Layer
{
    internal class Program
    {
        public static void Main()
        {
            DisplayOptions();
            Console.WriteLine();
        }

        private static void DisplayOptions()
        {
            using (var context = new FinancialChartsContext())
            {
                var optionsVar = context.Option.ToList();
                foreach (var opt in optionsVar)
                {
                    Console.WriteLine(opt.Name);
                    
                }
                Console.ReadKey();
            }
        }
    }
}
