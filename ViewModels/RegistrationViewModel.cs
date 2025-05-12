using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.ViewModels
{
  public class RegistrationViewModel
  {
    [Required]
    public int MinistrialNumber { get; set; }

    

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public int RegionID { get; set; }

    [Required]
    public int DirectorateID { get; set; }

    [Required]
    public int NationalID { get; set; }

    public int UserTypeID { get; set; }

    public  IEnumerable<SelectListItem>? Regions { get; set; }
  }
}
