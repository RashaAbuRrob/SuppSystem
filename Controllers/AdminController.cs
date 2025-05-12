using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.ViewModels;
using Microsoft.AspNetCore.Hosting;

public class AdminController : Controller
{
  private readonly SuppDatabaseContext _context;
  private readonly IWebHostEnvironment _webHostEnvironment;

  public AdminController(SuppDatabaseContext context, IWebHostEnvironment webHostEnvironment)
  {
    _context = context;
    _webHostEnvironment = webHostEnvironment;
  }

  public async Task<IActionResult> AdminDashboard()
  {

    var userId = HttpContext.Session.GetInt32("UserId");

    if (userId.HasValue)
    {
      var user = _context.Users.FirstOrDefault(u => u.MinistrialNumber == userId.Value);

      if (user != null)
      {
        var model = new UserViewModel
        {
          IsAdmin = (bool)user.IsAdmin
        };

        return View(model);
      }
    }

    {
      var ministrialNumber = HttpContext.Session.GetInt32("UserMinNo");

      if (!ministrialNumber.HasValue)
      {
        return RedirectToAction("LoginBasic", "Auth");
      }

      var user = await _context.Users.FirstOrDefaultAsync(u => u.MinistrialNumber == ministrialNumber.Value);
      var school = user != null ? await _context.Schools.FirstOrDefaultAsync(s => s.TechnicianId == user.MinistrialNumber) : null;

      if (user == null)
      {
        return NotFound();
      }

      ViewBag.Regions = await _context.Regions.ToListAsync();
      ViewBag.Directorates = await _context.Directorates.ToListAsync();
      ViewBag.Schools = await _context.Schools.ToListAsync();
      ViewBag.SelectedRegionId = null;
      ViewBag.SelectedDirectorateId = null;
      ViewBag.SelectedSchoolId = null;
      ViewBag. UserType = "Admin";

      var viewModel = new AdminDashboardViewModel
      {
        User = user,
        School = school,
        TotalSchools = await _context.Schools.CountAsync(),
        TotalAcadimicLabs = await _context.Labs.Where(l => l.Type == "Academic").CountAsync(),
        TotalBTECLabs = await _context.Labs.Where(l => l.Type == "BTEC").CountAsync(),
        TotalDesktopsNotInLabs = await _context.Desktops.Where(d => d.LabId == null).CountAsync(),
        TotalDesktops = await _context.Desktops.CountAsync(),
        TotalLaptops = await _context.Laptops.CountAsync(),
        TotalPrinters = await _context.Printers.CountAsync(),
        TotalDatashows = await _context.Datashows.CountAsync(),
        TotalInteractiveBoards = await _context.InteractiveBoards.CountAsync(),
        Schools = await _context.Schools.ToListAsync(),
        Labs = await _context.Labs.ToListAsync(),
        Users = await _context.Users.ToListAsync(),
        Regions = await _context.Regions.ToListAsync(),
        Directorates = await _context.Directorates.ToListAsync()
      };

      ViewData["Title"] = "اللوحة الرئيسية";
      return View(viewModel);
    }
  }

  [HttpPost]
  public async Task<IActionResult> AdminDashboard(int? RegionId, int? DirectorateId, int? SchoolId)
  {
    var ministrialNumber = HttpContext.Session.GetInt32("UserMinNo");

    if (!ministrialNumber.HasValue)
    {
      return RedirectToAction("LoginBasic", "Auth");
    }

    var user = await _context.Users.FirstOrDefaultAsync(u => u.MinistrialNumber == ministrialNumber.Value);
    var school = user != null ? await _context.Schools.FirstOrDefaultAsync(s => s.TechnicianId == user.MinistrialNumber) : null;

    if (user == null)
    {
      return NotFound();
    }

    var schoolsQuery = _context.Schools.AsQueryable();
    var labsQuery = _context.Labs.AsQueryable();
    var desktopsQuery = _context.Desktops.AsQueryable();
    var laptopsQuery = _context.Laptops.AsQueryable();
    var printersQuery = _context.Printers.AsQueryable();
    var datashowsQuery = _context.Datashows.AsQueryable();
    var interactiveBoardsQuery = _context.InteractiveBoards.AsQueryable();

    if (RegionId.HasValue)
    {
      var directoratesInRegion = await _context.Directorates
                                              .Where(d => d.RegionId == RegionId)
                                              .Select(d => d.Id)
                                              .ToListAsync();

      schoolsQuery = schoolsQuery.Where(s => directoratesInRegion.Contains((int)s.DirectorateId));
      labsQuery = labsQuery.Where(l => directoratesInRegion.Contains((int)l.School.DirectorateId));
      desktopsQuery = desktopsQuery.Where(d => directoratesInRegion.Contains((int)d.School.DirectorateId));
      laptopsQuery = laptopsQuery.Where(l => directoratesInRegion.Contains((int)l.School.DirectorateId));
      printersQuery = printersQuery.Where(p => directoratesInRegion.Contains((int)p.School.DirectorateId));
      datashowsQuery = datashowsQuery.Where(d => directoratesInRegion.Contains((int)d.School.DirectorateId));
      interactiveBoardsQuery = interactiveBoardsQuery.Where(ib => directoratesInRegion.Contains((int)ib.School.DirectorateId));
    }

    if (DirectorateId.HasValue)
    {
      schoolsQuery = schoolsQuery.Where(s => s.DirectorateId == DirectorateId);
      labsQuery = labsQuery.Where(l => l.School.DirectorateId == DirectorateId);
      desktopsQuery = desktopsQuery.Where(d => d.School.DirectorateId == DirectorateId);
      laptopsQuery = laptopsQuery.Where(l => l.School.DirectorateId == DirectorateId);
      printersQuery = printersQuery.Where(p => p.School.DirectorateId == DirectorateId);
      datashowsQuery = datashowsQuery.Where(d => d.School.DirectorateId == DirectorateId);
      interactiveBoardsQuery = interactiveBoardsQuery.Where(ib => ib.School.DirectorateId == DirectorateId);
    }

    if (SchoolId.HasValue)
    {
      labsQuery = labsQuery.Where(l => l.SchoolId == SchoolId);
      desktopsQuery = desktopsQuery.Where(d => d.SchoolId == SchoolId);
      laptopsQuery = laptopsQuery.Where(l => l.SchoolId == SchoolId);
      printersQuery = printersQuery.Where(p => p.SchoolId == SchoolId);
      datashowsQuery = datashowsQuery.Where(d => d.SchoolId == SchoolId);
      interactiveBoardsQuery = interactiveBoardsQuery.Where(ib => ib.SchoolId == SchoolId);
    }

    ViewBag.Regions = await _context.Regions.ToListAsync();
    ViewBag.Directorates = await _context.Directorates.ToListAsync();
    ViewBag.Schools = await _context.Schools.ToListAsync();
    ViewBag.SelectedRegionId = RegionId;
    ViewBag.SelectedDirectorateId = DirectorateId;
    ViewBag.SelectedSchoolId = SchoolId;

    var viewModel = new AdminDashboardViewModel
    {
      User = user,
      School = school,
      TotalSchools = await schoolsQuery.CountAsync(),
      TotalAcadimicLabs = await labsQuery.Where(l => l.Type == "Academic").CountAsync(),
      TotalBTECLabs = await labsQuery.Where(l => l.Type == "BTEC").CountAsync(),
      TotalDesktopsNotInLabs = await desktopsQuery.Where(d => d.LabId == null).CountAsync(),
      TotalDesktops = await desktopsQuery.CountAsync(),
      TotalLaptops = await laptopsQuery.CountAsync(),
      TotalPrinters = await printersQuery.CountAsync(),
      TotalDatashows = await datashowsQuery.CountAsync(),
      TotalInteractiveBoards = await interactiveBoardsQuery.CountAsync(),
      Schools = await _context.Schools.ToListAsync(),
      Labs = await labsQuery.ToListAsync(),
      Users = await _context.Users.ToListAsync(),
      Regions = await _context.Regions.ToListAsync(),
      Directorates = await _context.Directorates.ToListAsync()
    };

    ViewData["Title"] = "اللوحة الرئيسية";
    return View(viewModel);
  }

  [HttpPost]
  public async Task<IActionResult> EditUser(User user)
  {
    if (ModelState.IsValid)
    {
      _context.Users.Update(user);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(AdminDashboard));
    }

    // Handle validation errors and return the view with error messages
    return View(user);
  }
  // GET: EditDesktop
  public IActionResult EditDesktop(int id)
  {
    var desktop = _context.Desktops.Find(id);
    if (desktop == null)
    {
      return NotFound();
    }

    var viewModel = new DesktopViewModel
    {
      ID = desktop.Id,
      BrandID = desktop.BrandId ?? default(int),  // Handle nullable int
      ModelID = desktop.ModelId ?? default(int),  // Handle nullable int
      LabID = desktop.LabId ?? default(int),      // Handle nullable int
      SerialNumber = desktop.SerialNumber,
      Ram = desktop.Ram,
      Processor = desktop.Processor,
      Barcode = desktop.Barcode
    };

    ViewBag.Brands = _context.Brands.ToList();
    ViewBag.Models = _context.Modells.ToList();

    return View(viewModel);
  }

  // POST: EditDesktop
  [HttpPost]
  public IActionResult EditDesktop(DesktopViewModel model)
  {
    if (ModelState.IsValid)
    {
      var desktop = _context.Desktops.Find(model.ID);
      if (desktop == null)
      {
        return NotFound();
      }

      // Update properties
      desktop.BrandId = model.BrandID;
      desktop.ModelId = model.ModelID;
      desktop.LabId = model.LabID;
      desktop.SerialNumber = model.SerialNumber;
      desktop.Ram = model.Ram;
      desktop.Processor = model.Processor;
      desktop.Barcode = model.Barcode;

      // Save changes
      _context.SaveChanges();

      ViewBag.SuccessMessage = "تم تعديل الجهاز بنجاح";
      return RedirectToAction("Desktop"); // Redirect back to the desktop list view
    }

    ViewBag.Brands = _context.Brands.ToList();
    ViewBag.Models = _context.Modells.ToList();
    // If model state is not valid, return the same view with the model
    return View(model);
  }
  
  // POST: AddLab
  [HttpPost]
  public async Task<IActionResult> AddLab(LabViewModel model)
  {
    if (ModelState.IsValid)
    {
      var userId = HttpContext.Session.GetInt32("UserMinNo");
      if (userId.HasValue)
      {
        var user = await _context.Users.FindAsync(userId.Value);
        if (user != null)
        {
          // Retrieve the school based on TechnicianID in the School table
          var school = await _context.Schools
              .FirstOrDefaultAsync(s => s.TechnicianId == user.MinistrialNumber);

          if (school != null)
          {
            var lab = new Lab
            {
              Id = model.Id,
              Type = model.Type,
              SchoolId = school.NationalId, // Use the School's NationalId
              LabNumberInSchool = model.LabNumberInSchool // Add the LabNumberInSchool field
            };


            _context.Labs.Add(lab);
            await _context.SaveChangesAsync();

            ViewBag.SuccessMessage = "تمت إضافة المختبر بنجاح.";
          }
          else
          {
            ViewBag.ErrorMessage = "تعذر العثور على بيانات المدرسة";
          }
        }
        else
        {
          ViewBag.ErrorMessage = "تعذر العثور على بيانات المستخدم";
        }
      }
      else
      {
        ViewBag.ErrorMessage = "المستخدم غير مسجل دخولاً";
      }
    }

    ViewBag.LabTypes = new List<string> { "أكاديمي", "BTEC" }; // Reload the dropdown options
    return View(model);
  }


  // GET: AddDesktop
  [HttpGet]
  public IActionResult AddDesktop()
  {
    ViewBag.Brands = _context.Brands.ToList();
    ViewBag.Models = _context.Modells.ToList();
    return View();
  }

  // POST: AddDesktop
  [HttpPost]
  public async Task<IActionResult> AddDesktop(DesktopViewModel model)
  {
    if (ModelState.IsValid)
    {
      var userId = HttpContext.Session.GetInt32("UserMinNo");
      if (userId.HasValue)
      {
        var user = await _context.Users.FindAsync(userId.Value);
        if (user != null)
        {
          // Retrieve the school based on TechnicianID in the School table
          var school = await _context.Schools
              .FirstOrDefaultAsync(s => s.TechnicianId == user.MinistrialNumber);

          if (school != null)
          {
            var desktop = new Desktop
            {
              BrandId = model.BrandID,
              ModelId = model.ModelID,
              LabId = model.LabID,
              SerialNumber = model.SerialNumber,
              Ram = model.Ram,
              Processor = model.Processor,
              Barcode = model.Barcode,
              SchoolId = school.NationalId // Use the School's NationalId
            };

            // Handle image upload
            if (model.Image != null)
            {
              var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
              var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
              var filePath = Path.Combine(uploadsFolder, uniqueFileName);

              using (var fileStream = new FileStream(filePath, FileMode.Create))
              {
                await model.Image.CopyToAsync(fileStream);
              }

              // Store the file path in the Barcode property
              desktop.Barcode = "/images/" + uniqueFileName;
            }

            _context.Desktops.Add(desktop);
            await _context.SaveChangesAsync();

            ViewBag.SuccessMessage = "تمت إضافة الجهاز بنجاح.";
          }
          else
          {
            ViewBag.ErrorMessage = "تعذر العثور على بيانات المدرسة";
          }
        }
        else
        {
          ViewBag.ErrorMessage = "تعذر العثور على بيانات المستخدم";
        }
      }
      else
      {
        ViewBag.ErrorMessage = "المستخدم غير مسجل دخولاً";
      }
    }

    ViewBag.Brands = _context.Brands.ToList();
    ViewBag.Models = _context.Modells.ToList();
    return View(model);
  }
  [HttpGet]
  public IActionResult AddLaptop()
  {
    ViewBag.Brands = _context.Brands.ToList();
    ViewBag.Models = _context.Modells.ToList();
    return View();
  }
  [HttpPost]
  [Route("Dashboards/AddLaptop")]
  public async Task<IActionResult> AddLaptop(LaptopViewModel model)
  {
    // Validate the model
    if (ModelState.IsValid)
    {
      // Retrieve the logged-in user's ID from the session
      var userId = HttpContext.Session.GetInt32("UserMinNo");
      if (userId.HasValue)
      {
        // Find the user in the database
        var user = await _context.Users.FindAsync(userId.Value);
        if (user != null)
        {
          // Retrieve the school based on the TechnicianID in the School table
          var school = await _context.Schools
              .FirstOrDefaultAsync(s => s.TechnicianId == user.MinistrialNumber);

          if (school != null)
          {
            // Create a new Laptop object
            var laptop = new Laptop
            {
              BrandId = model.BrandID,
              ModelId = model.ModelID,
              LabId = model.LabID,
              SerialNumber = model.SerialNumber,
              Ram = model.Ram,
              Processor = model.Processor,
              Barcode = model.Barcode,
              SchoolId = school.NationalId // Use the School's NationalId
            };

            // Add the laptop to the database
            _context.Laptops.Add(laptop);
            await _context.SaveChangesAsync();

            // Set success message
            ViewBag.SuccessMessage = "تمت إضافة الجهاز بنجاح.";
          }
          else
          {
            // Error: School not found
            ViewBag.ErrorMessage = "تعذر العثور على بيانات المدرسة";
          }
        }
        else
        {
          // Error: User not found
          ViewBag.ErrorMessage = "تعذر العثور على بيانات المستخدم";
        }
      }
      else
      {
        // Error: User is not logged in
        ViewBag.ErrorMessage = "المستخدم غير مسجل دخولاً";
      }
    }

    // Populate dropdown lists for the view
    ViewBag.Brands = _context.Brands.ToList();
    ViewBag.Models = _context.Modells.ToList();

    // Return the view with the model
    return View(model);
  }

  // GET: Index (Dashboard View)
  public async Task<IActionResult> Index()
  {
    var userId = HttpContext.Session.GetInt32("UserMinNo");
    if (userId.HasValue)
    {
      var user = await _context.Users.FindAsync(userId.Value);
      if (user != null)
      {
        // Retrieve the school based on TechnicianID
        var school = await _context.Schools.FirstOrDefaultAsync(s => s.TechnicianId == user.MinistrialNumber);

        if (school != null)
        {
          // Get the count of BTEC labs
          var bTECLabCount = await _context.Labs
                .Where(l => l.Type == "BTEC" && l.SchoolId == school.NationalId)
                .CountAsync();

          // Get the count of academic labs
          var academicLabCount = await _context.Labs
                .Where(l => l.Type == "أكاديمي" && l.SchoolId == school.NationalId)
                .CountAsync();

          var viewModel = new DashboardViewModel
          {
            User = user,
            School = school,
            AcademicLabCount = academicLabCount,
            BTECLabCount = bTECLabCount
            // Initialize other properties as needed
          };

          return View(viewModel);
        }
        else
        {
          ViewBag.ErrorMessage = "تعذر العثور على بيانات المدرسة";
        }
      }
      else
      {
        ViewBag.ErrorMessage = "تعذر العثور على بيانات المستخدم";
      }
    }
    else
    {
      ViewBag.ErrorMessage = "المستخدم غير مسجل دخولاً";
    }

    return View(new DashboardViewModel());
  }




  // GET: Desktop (List of Desktops)
  public async Task<IActionResult> Desktop()
  {
    var userId = HttpContext.Session.GetInt32("UserMinNo");
    Console.WriteLine($"UserMinNo from session: {userId}");

    if (userId.HasValue)
    {
      var user = await _context.Users.FindAsync(userId.Value);
      if (user != null)
      {
        Console.WriteLine($"User found: {user.MinistrialNumber}");

        // Retrieve the school based on TechnicianID in the School table
        var school = await _context.Schools
            .FirstOrDefaultAsync(s => s.TechnicianId == user.MinistrialNumber);

        if (school != null)
        {
          Console.WriteLine($"School found: {school.Name}, NationalId: {school.NationalId}");

          var desktops = await _context.Desktops
              .Include(d => d.Brand)
              .Include(d => d.Modell)
              .Where(d => d.SchoolId == school.NationalId)
              .ToListAsync();

          var desktopViewModels = desktops.Select(d => new DesktopViewModel
          {
            ID = d.Id,
            BrandName = d.Brand?.Name,
            ModelName = d.Modell?.Name,
            LabID = d.LabId ?? 0, // Provide a default value if LabId is null
            SerialNumber = d.SerialNumber,
            Ram = d.Ram,
            Processor = d.Processor,
            Barcode = d.Barcode
          }).ToList();

          return View(desktopViewModels);
        }
        else
        {
          Console.WriteLine("No school found for this technician.");
          ViewBag.ErrorMessage = "تعذر العثور على بيانات المدرسة";
        }
      }
      else
      {
        Console.WriteLine("User not found.");
        ViewBag.ErrorMessage = "تعذر العثور على بيانات المستخدم";
      }
    }
    else
    {
      Console.WriteLine("UserMinNo not found in session.");
      ViewBag.ErrorMessage = "المستخدم غير مسجل دخولاً";
    }

    return View(new List<DesktopViewModel>()); // Return an empty list to avoid null reference
  }
  //GET the list of laptops
  public async Task<IActionResult> Laptop()
  {
    var userId = HttpContext.Session.GetInt32("UserMinNo");
    if (userId.HasValue)
    {
      var user = await _context.Users.FindAsync(userId.Value);
      if (user != null)
      {
        // Retrieve the school based on the TechnicianID in the School table
        var school = await _context.Schools
            .FirstOrDefaultAsync(s => s.TechnicianId == user.MinistrialNumber);

        if (school != null)
        {
          // Fetch all laptops for the school
          var laptops = await _context.Laptops
              .Where(l => l.SchoolId == school.NationalId)
              .Select(l => new LaptopViewModel
              {
                ID = l.Id,
                BrandName = l.Brand.Name,
                ModelName = l.Model.Name,
                LabID = (int)l.LabId,
                SerialNumber = l.SerialNumber,
                Ram = l.Ram,
                Processor = l.Processor,
                Barcode = l.Barcode
              })
              .ToListAsync();

          return View(laptops);
        }
      }
    }

    return View(new List<LaptopViewModel>()); // Return an empty list if no laptops can be fetched
  }
  // GET: Lab (List of Labs)
  public async Task<IActionResult> Lab()
  {
    var userId = HttpContext.Session.GetInt32("UserMinNo");
    if (userId.HasValue)
    {
      var user = await _context.Users.FindAsync(userId.Value);
      if (user != null)
      {
        // Retrieve the school based on TechnicianID in the School table
        var school = await _context.Schools
            .FirstOrDefaultAsync(s => s.TechnicianId == user.MinistrialNumber);

        if (school != null)
        {
          // Fetch all labs for the school
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
        else
        {
          ViewBag.ErrorMessage = "تعذر العثور على بيانات المدرسة";
        }
      }
      else
      {
        ViewBag.ErrorMessage = "تعذر العثور على بيانات المستخدم";
      }
    }
    else
    {
      ViewBag.ErrorMessage = "المستخدم غير مسجل دخولاً";
    }

    return View(new List<LabViewModel>()); // Return an empty list if no labs can be fetched
  }
  // Other actions

  public IActionResult Datashow() => View();
  public IActionResult Printer() => View();

  public IActionResult StandAloneTest() => View();
}


