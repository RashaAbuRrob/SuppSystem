using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.ViewModels
{
  public class AdminDashboardViewModel
  {
    public User User { get; set; }
    public School School { get; set; }
    public int TotalSchools { get; set; }
    public int TotalAcadimicLabs { get; set; }
    public int TotalBTECLabs { get; set; }
    public int TotalDesktopsNotInLabs { get; set; }
    public int TotalDesktops { get; set; }
    public int TotalLaptops { get; set; }
    public int TotalPrinters { get; set; }
    public int TotalDatashows { get; set; }
    public int TotalInteractiveBoards { get; set; }
    public IEnumerable<School> Schools { get; set; }
    public IEnumerable<Lab> Labs { get; set; }
    public IEnumerable<User> Users { get; set; }
    public IEnumerable<Region> Regions { get; set; }
    public IEnumerable<Directorate> Directorates { get; set; }

    // Add these properties
    public int? SelectedRegionId { get; set; }
    public int? SelectedDirectorateId { get; set; }
    public int? SelectedSchoolId { get; set; }
  }
}
