using System.Collections.Generic;

namespace AspnetCoreMvcFull.ViewModels
{
  public class NewAllDatumViewModel
  {
    public string? Region { get; set; }
    public string? Directorate { get; set; }
    public long SchoolId { get; set; }
    public string? SchoolName { get; set; }
    public string? MaxGrade { get; set; }
    public string? MinGrade { get; set; }
    public long? AllPcsCount { get; set; }
    public long? NotInLabPcsCount { get; set; }
    public long? AcadimicLab { get; set; }
    public long? Bteclab { get; set; }

    // Desktop Labs
    public long? DesktopLab1 { get; set; }
    public long? DesktopLab2 { get; set; }
    public long? DesktopLab3 { get; set; }
    public long? DesktopLab4 { get; set; }
    public long? DesktopLab5 { get; set; }
    public long? DesktopLab6 { get; set; }
    public long? DesktopLab7 { get; set; }

    // Not in Lab Printers
    public long? NotInLabPrinter { get; set; }

    // Printer Labs
    public long? PrinterLab1 { get; set; }
    public long? PrinterLab2 { get; set; }
    public long? PrinterLab3 { get; set; }
    public long? PrinterLab4 { get; set; }
    public long? PrinterLab5 { get; set; }
    public long? PrinterLab6 { get; set; }
    public long? PrinterLab7 { get; set; }

    // Data Show Labs
    public long? DataShowLab1 { get; set; }
    public long? DataShowLab2 { get; set; }
    public long? DataShowLab3 { get; set; }
    public long? DataShowLab4 { get; set; }
    public long? DataShowLab5 { get; set; }
    public long? DataShowLab6 { get; set; }
    public long? DataShowLab7 { get; set; }

    // Interactive Boards Labs
    public long? InteractiveBoardsLab1 { get; set; }
    public long? InteractiveBoardsLab2 { get; set; }
    public long? InteractiveBoardsLab3 { get; set; }
    public long? InteractiveBoardsLab4 { get; set; }
    public long? InteractiveBoardsLab5 { get; set; }
    public long? InteractiveBoardsLab6 { get; set; }
    public long? InteractiveBoardsLab7 { get; set; }

    // Not in Lab Laptops
    public long? NotInLabLaptop { get; set; }

    // Laptop Labs
    public long? LaptopLab1 { get; set; }
    public long? LaptopLab2 { get; set; }
    public long? LaptopLab3 { get; set; }
    public long? LaptopLab4 { get; set; }
    public long? LaptopLab5 { get; set; }
    public long? LaptopLab6 { get; set; }
    public long? LaptopLab7 { get; set; }

    public long? MultiSeat { get; set; }

    // Additional properties or methods for the view can be added here.
    // Totals for Desktops
    public long TotalDesktopLabs =>
        (DesktopLab1 ?? 0) + (DesktopLab2 ?? 0) + (DesktopLab3 ?? 0) +
        (DesktopLab4 ?? 0) + (DesktopLab5 ?? 0) + (DesktopLab6 ?? 0) + (DesktopLab7 ?? 0);

    // Totals for Printers
    public long TotalPrinterLabs =>
        (PrinterLab1 ?? 0) + (PrinterLab2 ?? 0) + (PrinterLab3 ?? 0) +
        (PrinterLab4 ?? 0) + (PrinterLab5 ?? 0) + (PrinterLab6 ?? 0) + (PrinterLab7 ?? 0);

    // Totals for Laptops
    public long TotalLaptopLabs =>
        (LaptopLab1 ?? 0) + (LaptopLab2 ?? 0) + (LaptopLab3 ?? 0) +
        (LaptopLab4 ?? 0) + (LaptopLab5 ?? 0) + (LaptopLab6 ?? 0) + (LaptopLab7 ?? 0);

    // Totals for Data Shows
    public long TotalDataShowLabs =>
        (DataShowLab1 ?? 0) + (DataShowLab2 ?? 0) + (DataShowLab3 ?? 0) +
        (DataShowLab4 ?? 0) + (DataShowLab5 ?? 0) + (DataShowLab6 ?? 0) + (DataShowLab7 ?? 0);

    // Totals for Interactive Boards
    public long TotalInteractiveBoardsLabs =>
        (InteractiveBoardsLab1 ?? 0) + (InteractiveBoardsLab2 ?? 0) + (InteractiveBoardsLab3 ?? 0) +
        (InteractiveBoardsLab4 ?? 0) + (InteractiveBoardsLab5 ?? 0) + (InteractiveBoardsLab6 ?? 0) + (InteractiveBoardsLab7 ?? 0);

    public long? SelectedRegionId { get; set; }
    public long? SelectedDirectorateId { get; set; }
    public int? SelectedSchoolId { get; set; }
  }
}
