using FilteringExcelSheet_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilteringExcelSheet_MVC.Controllers
{
    public class AssetController : Controller
    {
        //inject the service class 

        private readonly FileServices _FileServices;

        public AssetController(FileServices fileServices)
        {
            _FileServices = fileServices;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Getting from the client the AssetExcelSheet
        public async Task<IActionResult> ReadExcelFile(IFormFile AssetExcelFile)
        {
            List<Asset> assets = new List<Asset>();

            assets = await _FileServices.ReadAssetExcelFile(AssetExcelFile);

            return View("Index", assets);
        }
    }
}
