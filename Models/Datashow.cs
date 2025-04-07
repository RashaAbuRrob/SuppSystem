using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Datashow
{
    public int Id { get; set; }

    public int? BrandId { get; set; }

    public int? ModelId { get; set; }

    public int? SchoolId { get; set; }

    public int? LabId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Lab? Lab { get; set; }

    public virtual Modell? Model { get; set; }

    public virtual School? School { get; set; }
}
