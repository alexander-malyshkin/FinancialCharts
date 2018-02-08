using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FinancialCharts.Controllers
{
    public class UserPreferenceController : Controller
    {
        public IActionResult SavePreference()
        {
            return View();
        }
    }
}