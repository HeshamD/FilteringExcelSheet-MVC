using FilteringExcelSheet_MVC.Models;
using FilteringExcelSheet_MVC.Services;
using FilteringExcelSheet_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OfficeOpenXml;
using System.Diagnostics;
using System.Drawing;

namespace FilteringExcelSheet_MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly FileServices _FileServices;

        public HomeController(FileServices fileServices)
        {
            _FileServices = fileServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public IActionResult UploadExcelFiles()
        {
            return View("UploadExcelFiles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Getting from the client the AssetExcelSheet
        public async Task<IActionResult> ReadExcelFiles(IFormFile AssetExcelFile, IFormFile FirmExcelFile)
        {
            if(AssetExcelFile != null && FirmExcelFile != null)
            {
                List<AssetFirmVM> assetFirmVM = new List<AssetFirmVM>();
                List<Asset> assets = new List<Asset>();
                List<Firm> firms = new List<Firm>();

                assets = await _FileServices.ReadAssetExcelFile(AssetExcelFile);

                firms = await _FileServices.ReadFirmExcelFile(FirmExcelFile);

                assetFirmVM = await _FileServices.FilterExcelFile(assets, firms);

                return View("Results", assetFirmVM);
            }

            return RedirectToAction(nameof(Index));
            
        }


    }
}