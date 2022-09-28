
using FilteringExcelSheet_MVC.ViewModels;

namespace FilteringExcelSheet_MVC.Services
{
    public class FileServices
    {
        public async Task<List<Asset>> ReadAssetExcelFile(IFormFile file)
        {
            // validate if the file exists 
            if (file.Length<=0)
            {
                throw new Exception("File Doesn't Exist");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            List<Asset> output = new List<Asset>(); // the file that we create to output the list of Asset

            //convert to stream
            var Filestream = file.OpenReadStream();

            using var package = new ExcelPackage(Filestream);

            await package.LoadAsync(Filestream);

            var ws = package.Workbook.Worksheets[0]; // working on the first worksheet 

            // create two helper var

            int row = 2; //starting with my data which starts at row 2
            int col = 1; //starting with col 1

            while (string.IsNullOrWhiteSpace(ws.Cells[row,col].Value?.ToString()) == false )
            {
                Asset asset = new Asset();

                asset.Asset_Class_Id = ws.Cells[row, col].Value.ToString();
                asset.Asset_Class_Name = ws.Cells[row, col + 1].Value.ToString();
                asset.Firms_Id = ws.Cells[row, col + 2].Value.ToString();

                output.Add(asset);

                row++;

            } // i am starting at row=2, col=1 if the value NOT NULL then  // if there is no data in that given column and row then we are down there is no data 


            return output; //this going to return the list of the Asset >> from the Excel file

        }
        public async Task<List<Firm>> ReadFirmExcelFile(IFormFile file)
        {
            // validate if the file exists 
            if (file.Length <= 0)
            {
                throw new Exception("File Doesn't Exist");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            List<Firm> output = new List<Firm>(); // the file that we create to output the list of Asset

            //convert to stream
            var Filestream = file.OpenReadStream();

            using var package = new ExcelPackage(Filestream);

            await package.LoadAsync(Filestream);

            var ws = package.Workbook.Worksheets[0]; // working on the first worksheet 

            // create two helper var

            int row = 2; //starting with my data which starts at row 2
            int col = 1; //starting with col 1

            while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
            {
                Firm firm = new Firm();

                firm.Firm_Id = ws.Cells[row, col].Value.ToString();
                firm.Firm_Name = ws.Cells[row, col + 1].Value.ToString();

                output.Add(firm);

                row++;

            } // i am starting at row=2, col=1 if the value NOT NULL then  // if there is no data in that given column and row then we are down there is no data 


            return output; //this going to return the list of the Asset >> from the Excel file

        }
        public async Task<List<AssetFirmVM>> FilterExcelFile(List<Asset> assets, List<Firm> firms)
        {

            var output = new List<AssetFirmVM>();

            var assetsClassNames = assets.Select(x => x.Asset_Class_Name).Distinct().OrderBy(x =>x).ToList();
            var firmsClassNames = firms.Select(x => x.Firm_Name).Distinct().OrderBy(x => x).ToList();
            var firmsIds = firms.Select(x => x.Firm_Id).Distinct().OrderBy(x => x).ToList();
            var assetClassId = assets.Select(x => x.Asset_Class_Id).Distinct().OrderBy(x => x).ToList();
            var asset_InterestedFirmsId = assets.Select(x => x.Firms_Id).Distinct().OrderBy(x => x).ToList();

            int i = 0;
            while (i<assetsClassNames.Count && i<firmsClassNames.Count)
            {
                var assetFirmVM = new AssetFirmVM();

                
                assetFirmVM.Asset_Class_Name = assetsClassNames[i];
                assetFirmVM.Firm_Name = firmsClassNames[i];
                assetFirmVM.Firm_Id = firmsIds[i];
                assetFirmVM.Asset_Class_Id = assetClassId[i];
                assetFirmVM.Asset_InterestedFirmId = firmsIds[i];
                output.Add(assetFirmVM);
                i++;
            }

            var firmAssetMap = new Dictionary<string, String>();

            var interestedFirmId_FirmId = new Dictionary<String, String>();

            foreach(var assetFirm in output)
            {
                
                firmAssetMap.Add(assetFirm.Asset_Class_Id, assetFirm.Firm_Id);
            }
           

            return output;

        }



    }
}
