namespace AspnetCoreMvcFull.ViewModels
{
  public class PrinterViewModel
  {

    public int ID { get; set; }
    public int BrandID { get; set; }
    public int ModelID { get; set; }  
    public string? BrandName { get; set; }
    public string? ModelName { get; set; }
    public int LabID { get; set; }
    public string? SerialNumber { get; set; }
    public string? Ram { get; set; }
    public string? Processor { get; set; }
    public string? Barcode { get; set; }
    public IFormFile? Image { get; set; }
        
  }


}
