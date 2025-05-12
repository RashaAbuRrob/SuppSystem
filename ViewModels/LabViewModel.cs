using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.ViewModels
{
  public class LabViewModel
  {
    public int Id { get; set; }

    public string? Type { get; set; }

    public int? SchoolId { get; set; }

    public string? SchoolName { get; set; } // Optional: To display the school name if needed

    public int? LabNumberInSchool { get; set; }

    public List<DatashowViewModel> Datashows { get; set; } = new List<DatashowViewModel>();

    public List<DesktopViewModel> Desktops { get; set; } = new List<DesktopViewModel>();

    public List<InteractiveBoardViewModel> InteractiveBoards { get; set; } = new List<InteractiveBoardViewModel>();

    public List<LaptopViewModel> Laptops { get; set; } = new List<LaptopViewModel>();

    public List<PrinterViewModel> Printers { get; set; } = new List<PrinterViewModel>();
  }




  

  


}
