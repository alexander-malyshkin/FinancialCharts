using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.Model;
using FinancialCharts.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.AspNetCore.ServerSentEvents.Controllers
{
    public class FinancialController : Controller
    {
        // GET: /<controller>/
        public IActionResult ChartTest()
        {
            var assetModel = new AssetModel();
            var assetDbRepo = new DatabaseReadonlyAssetRepository();
            assetModel.AssetList = assetDbRepo.Assets;
            return View(assetModel);
        }

        public IActionResult Test()
        {
            return View();
        }
        //public IActionResult Chart()
        //{
        //    return View();
        //}


        public IActionResult Chart()
        {
            var assetModel = new AssetModel();
            var assetDbRepo = new DatabaseReadonlyAssetRepository();
            assetModel.AssetList = assetDbRepo.Assets;
            return View(assetModel);
        }
    }
}
