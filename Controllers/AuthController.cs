using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AspnetCoreMvcFull.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Crypto.Generators;

namespace AspnetCoreMvcFull.Controllers
{
  public class AuthController : Controller
  {
    private readonly SuppDatabaseContext _context;
    private readonly ILogger<AuthController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthController(SuppDatabaseContext context, ILogger<AuthController> logger, IHttpContextAccessor httpContextAccessor)
    {
      _context = context;
      _logger = logger;
      _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult ForgotPasswordBasic() => View();
    public IActionResult LoginBasic() => View();



    [HttpGet]
    public IActionResult RegisterBasic()
    {
      var model = new RegistrationViewModel
      {
        Regions = _context.Regions.Select(r => new SelectListItem
        {
          Value = r.Id.ToString(),
          Text = r.Name
        }).ToList()
      };

      return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterBasic(RegistrationViewModel model)
    {
      if (ModelState.IsValid)
      {
        var existingUser = await _context.Users.AnyAsync(u => u.MinistrialNumber == model.MinistrialNumber);
        if (existingUser)
        {
          ModelState.AddModelError(string.Empty, "");
          ViewBag.ErrorMessage = "الرقم الوزراي موجود مسبقا";
          model.Regions = _context.Regions.Select(r => new SelectListItem
          {
            Value = r.Id.ToString(),
            Text = r.Name
          }).ToList();
          return View(model);
        }

        var user = new User
        {
          MinistrialNumber = model.MinistrialNumber,
          Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // Ensure the password is hashed
          Name = model.Name,
          Phone = model.Phone
        };

        // Add user to context
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Update School with TechnicianID
        var school = await _context.Schools.FirstOrDefaultAsync(s => s.NationalId == model.NationalID);
        if (school != null)
        {
          if (school.TechnicianId != null)
          {
            ModelState.AddModelError(string.Empty, "هذه المدرسة خاصة بمستخدم أخر الرجاء اختيار مدرسة أخرى");
            ViewBag.ErrorMessage = "هذه المدرسة خاصة بمستخدم أخر الرجاء اختيار مدرسة أخرى";
            // Repopulate Regions
            model.Regions = _context.Regions.Select(r => new SelectListItem
            {
              Value = r.Id.ToString(),
              Text = r.Name
            }).ToList();
            return View(model);
          }

          school.TechnicianId = user.MinistrialNumber;
          await _context.SaveChangesAsync();

          ViewBag.SuccessMessage = "تم تسجيل المستخدم الجديد بنجاح اذهب الى صفحة تسجيل الدخول";
          model.Regions = _context.Regions.Select(r => new SelectListItem
          {
            Value = r.Id.ToString(),
            Text = r.Name
          }).ToList();
          return View(model); // Stay on the same view and display success message
        }
        else
        {
          ModelState.AddModelError(string.Empty, "");
          ViewBag.ErrorMessage = "ألمدرسة غير موجودة";
        }
      }

      // Repopulate Regions in case of a form validation failure
      model.Regions = _context.Regions.Select(r => new SelectListItem
      {
        Value = r.Id.ToString(),
        Text = r.Name
      }).ToList();

      return View(model);
    }







    [HttpGet]
      public JsonResult GetDirectorates(int regionId)
      {
        var directorates = _context.Directorates
            .Where(d => d.RegionId == regionId)
            .Select(d => new { value = d.Id, text = d.Name })
            .ToList();
        return Json(directorates);
      }

      [HttpGet]
      public JsonResult GetSchools(int directorateId)
      {
        var schools = _context.Schools
            .Where(s => s.DirectorateId == directorateId)
            .Select(s => new { value = s.NationalId, text = s.Name })
            .ToList();
        return Json(schools);
      }


    [HttpPost]
    public async Task<IActionResult> LoginBasic(int minNo, string password)
    {
      _logger.LogInformation("Attempting login for ID: {ID}", minNo);

      // Fetching the user from the database
      var user = await _context.Users
          .FirstOrDefaultAsync(u => u.MinistrialNumber == minNo);

      // Verify the user and password
      if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
      {
        _logger.LogInformation("Login successful for MinNo: {minNo}", minNo);

        // Storing user information in session
        _httpContextAccessor.HttpContext.Session.SetInt32("UserMinNo", user.MinistrialNumber);
        _httpContextAccessor.HttpContext.Session.SetString("UserName", user.Name);
        _httpContextAccessor.HttpContext.Session.SetInt32("UserTypeID", (int)user.UserType);

        // Redirect based on UserType
        switch (user.UserType)
        {
          case 1: // Admin
            return RedirectToAction("AdminDashboard", "Admin");
          case 2: // Department Head
            return RedirectToAction("DepartmentDashboard", "DepartmentHead");
          case 3: // Lab Technician
            return RedirectToAction("Index", "LabTechnician");
          default: // Fallback for unknown UserType
            return RedirectToAction("Index", "Dashboards");
        }
      
    }

      // If login fails
      _logger.LogWarning("Login failed for MinNo: {minNo}", minNo);
      ViewBag.ErrorMessage = "الرقم الوزاري أو كلمة المرور غير صحيحة";
      return View("LoginBasic");
    }
  }
}
