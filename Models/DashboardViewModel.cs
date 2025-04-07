namespace AspnetCoreMvcFull.Models
{
  public class DashboardViewModel
  {
    public User User { get; set; }
    public School School { get; set; }
    public Lab Lab { get; set; }
    public int AcademicLabCount { get; set; }
    public int BTECLabCount { get; set; }

    // Add more properties as needed


  }
}
