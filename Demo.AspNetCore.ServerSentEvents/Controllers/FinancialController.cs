using System;
using FinancialCharts.Model;
using FinancialCharts.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialCharts.Controllers
{
    public class FinancialController : Controller
    {
        // GET: /<controller>/
        public IActionResult Chart()
        {
            var assetModel = new AssetModel();
            var datesModel = new ExpDatesModel();
            var assetDbRepo = new DatabaseReadonlyAssetRepository();
            var datesDbRepo = new DatabaseReadonlyExpDatesRepository();
            assetModel.AssetList = assetDbRepo.Assets;
            datesModel.ExpDatesList = datesDbRepo.DatesList;

            var compositeModel = new Tuple<AssetModel, ExpDatesModel>(assetModel, datesModel);

            return View(compositeModel);
        }

        
    }
}
