using System;
using System.Linq;
using FinancialCharts.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialCharts.Controllers
{
    public class FinancialController : Controller
    {
        // GET: /<controller>/
        public IActionResult Chart()
        {
            //var assetModel = new AssetModel();
            //var datesModel = new OptionModel();
            //var assetDbRepo = new DatabaseReadonlyAssetRepository();
            //var datesDbRepo = new DatabaseReadonlyExpDatesRepository();
            //var dates = datesDbRepo.GetDates().ToList();
            //assetModel.AssetList = assetDbRepo.GetAssets().ToList();
            //datesModel.ExpDatesList = JsonConvert.SerializeObject(dates);

            var compositeModel = new CompositeModel();

            return View(compositeModel);
        }

        
    }
}
