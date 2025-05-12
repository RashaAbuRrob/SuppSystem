namespace AspnetCoreMvcFull.ViewModels
{
  public class UserViewModel
  {

    // Represents the User's information
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
    public int MinistrialNumber { get; set; }

    public bool Official { get; set; }

    // Represents the associated school (optional)
    public SchoolViewModel School { get; set; }

    // Additional data for dropdowns, etc.
    public List<RegionViewModel> Regions { get; set; }
    public List<DirectorateViewModel> Directorates { get; set; }
    public List<SchoolViewModel> Schools { get; set; }
  }

  // Optional: Define related view models for regions, directorates, and schools
  public class RegionViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }

  public class DirectorateViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int RegionId { get; set; }
  }

  public class SchoolViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int RegionId { get; set; }
    public int DirectorateId { get; set; }
    public int TechnicianId { get; set; }
  }
}

