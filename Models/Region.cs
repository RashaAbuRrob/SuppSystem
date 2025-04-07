using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Region
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Directorate> Directorates { get; set; } = new List<Directorate>();
}
