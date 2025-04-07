using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Desktop
{
    public int Id { get; set; }

    public string? SerialNumber { get; set; }

    public int? BrandId { get; set; }

    public int? ModelId { get; set; }

    public int? LabId { get; set; }

    public int? SchoolId { get; set; }

    public string? Barcode { get; set; }

    public string? Ram { get; set; }

    public string? Processor { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Lab? Lab { get; set; }

    public virtual Modell? Modell { get; set; }

    public virtual School? School { get; set; }
}
