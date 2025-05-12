using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using AspnetCoreMvcFull.ViewModels;
using System.Linq;
using System.Security.Claims;

public class DashboardsController : Controller
{
  private readonly SuppDatabaseContext _context;
  private readonly IWebHostEnvironment _webHostEnvironment;

  public DashboardsController(SuppDatabaseContext context, IWebHostEnvironment webHostEnvironment)
  {
    _context = context;
    _webHostEnvironment = webHostEnvironment;
  }

  // Default GET method


  public async Task<IActionResult> GetDirectoratesByRegion(int regionId)
  {
    var directorates = await _context.Directorates
                                     .Where(d => d.RegionId == regionId)
                                     .Select(d => new { id = d.Id, name = d.Name })
                                     .ToListAsync();

    return Json(directorates);
  }

  public async Task<IActionResult> GetSchoolsByDirectorate(int directorateId)
  {
    var schools = await _context.Schools
                                .Where(s => s.DirectorateId == directorateId)
                                .Select(s => new { nationalId = s.NationalId, name = s.Name })
                                .ToListAsync();

    return Json(schools);
  }

}


    

   

    


    



