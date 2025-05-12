using System;

namespace AspnetCoreMvcFull.ViewModels
{
  public class InteractiveBoardViewModel
  {
    public int Id { get; set; }

    public int? BrandId { get; set; }

    public string? BrandName { get; set; } // Optional: To display the brand name if needed

    public int? ModelId { get; set; }

    public string? ModelName { get; set; } // Optional: To display the model name if needed

    public int? SchoolId { get; set; }

    public string? SchoolName { get; set; } // Optional: To display the school name if needed

    public int? LabId { get; set; }

    public string? LabType { get; set; } // Optional: To display the lab type (e.g., Computer Lab, Science Lab)
  }
}
