namespace FilteringExcelSheet_MVC.ViewModels
{
    // Acted as Join Table and View Model 
    // btw it's violating one of the most critical rules of Object-Oriented Design >> Single Responsibility Principle 
    // but since it's a small demo and I demonstrated the correct way of representing this data 
    public class AssetFirmVM
    {
        public string Asset_Class_Name { get; set; }
        public string Firm_Name { get; set; }
        public string Asset_Class_Id { get; set; }
        public string Firm_Id { get; set; }
        public string Asset_InterestedFirmId { get; set; }


    }
}
