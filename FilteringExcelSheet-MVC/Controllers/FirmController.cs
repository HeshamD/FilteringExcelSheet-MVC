using FilteringExcelSheet_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilteringExcelSheet_MVC.Controllers
{
    public class FirmController : Controller
    {
        //inject the service class 

        private readonly FileServices _FileServices;

        public FirmController(FileServices fileServices)
        {
            _FileServices = fileServices;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //Getting from the client the AssetExcelSheet
        public async Task<IActionResult> ReadExcelFile(IFormFile FirmExcelFile)
        {
            List<Firm> firms = new List<Firm>();

            firms = await _FileServices.ReadFirmExcelFile(FirmExcelFile);

            return View("Index", firms);
        }
    }
}
