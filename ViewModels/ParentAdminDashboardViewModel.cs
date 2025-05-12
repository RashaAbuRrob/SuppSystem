namespace AspnetCoreMvcFull.ViewModels
{
  public class ParentAdminDashboardViewModel
  {
    public AdminDashboardViewModel AdminDashboard { get; set; }
    public NewAllDatumViewModel NewAllData { get; set; }

    // Add these properties to count all BTECLabs and AcadimicLabs across all schools
    public long TotalBTECLabsForAllSchools { get; set; }
    public long TotalAcadimicLabsForAllSchools { get; set; }
  }
}
