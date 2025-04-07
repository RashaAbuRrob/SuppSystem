using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Modell
{
    public int Id { get; set; }

    public int? BrandId { get; set; }

    public string? Name { get; set; }

    public string? DeviceType { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<Datashow> Datashows { get; set; } = new List<Datashow>();

    public virtual ICollection<Desktop> Desktops { get; set; } = new List<Desktop>();

    public virtual ICollection<InteractiveBoard> InteractiveBoards { get; set; } = new List<InteractiveBoard>();

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();

    public virtual ICollection<Printer> Printers { get; set; } = new List<Printer>();
}
