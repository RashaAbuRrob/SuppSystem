using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class School
{
    public int NationalId { get; set; }

    public string? Name { get; set; }

    public int? DirectorateId { get; set; }

    public string? MaxGrade { get; set; }

    public string? MinGrade { get; set; }

    public bool? InternetConnection { get; set; }

    public bool? LabTechnician { get; set; }

    public int? TechnicianId { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Datashow> Datashows { get; set; } = new List<Datashow>();

    public virtual ICollection<Desktop> Desktops { get; set; } = new List<Desktop>();

    public virtual Directorate? Directorate { get; set; }

    public virtual ICollection<InteractiveBoard> InteractiveBoards { get; set; } = new List<InteractiveBoard>();

    public virtual ICollection<Lab> Labs { get; set; } = new List<Lab>();

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();

    public virtual ICollection<Printer> Printers { get; set; } = new List<Printer>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
