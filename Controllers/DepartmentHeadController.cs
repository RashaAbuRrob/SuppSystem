using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.ViewModels;

public class DepartmentHeadController : Controller
{
  private readonly SuppDatabaseContext _context;
  private readonly IWebHostEnvironment _webHostEnvironment;


  public DepartmentHeadController(SuppDatabaseContext context, IWebHostEnvironment webHostEnvironment)
  {
    _context = context;
  }

 /* public async Task<IActionResult> DepartmentDashboard()
  {
    ViewBag.UserType = "DepartmentHead";
  }*/


  public async Task<IActionResult> Desktop()
  {
    var userId = HttpContext.Session.GetInt32("UserMinNo");

    if (userId.HasValue)
    {
      var user = await _context.Users.FindAsync(userId.Value);
      var school = user != null
          ? await _context.Schools.FirstOrDefaultAsync(s => s.TechnicianId == user.MinistrialNumber)
          : null;

      if (school != null)
      {
        var desktops = await _context.Desktops
            .Include(d => d.Brand)
            .Include(d => d.Modell)
            .Where(d => d.SchoolId == school.NationalId)
            .Select(d => new DesktopViewModel
            {
              ID = d.Id,
              BrandName = d.Brand.Name,
              ModelName = d.Modell.Name,
              LabID = d.LabId ?? default(int),
              SerialNumber = d.SerialNumber,
              Ram = d.Ram,
              Processor = d.Processor,
              Barcode = d.Barcode
            })
            .ToListAsync();

        return View(desktops);
      }
    }

    return View(new List<DesktopViewModel>());
  }

  public async Task<IActionResult> Lab()
  {
    var userId = HttpContext.Session.GetInt32("UserMinNo");

    if (userId.HasValue)
    {
      var user = await _context.Users.FindAsync(userId.Value);
      var school = user != null
          ? await _context.Schools.FirstOrDefaultAsync(s => s.TechnicianId == user.MinistrialNumber)
          : null;

      if (school != null)
      {
        var labs = await _context.Labs
            .Where(l => l.SchoolId == school.NationalId)
            .Select(l => new LabViewModel
            {
              Id = l.Id,
              Type = l.Type,
              LabNumberInSchool = l.LabNumberInSchool
            })
            .ToListAsync();

        return View(labs);
      }
    }

    return View(new List<LabViewModel>());
  }
}
