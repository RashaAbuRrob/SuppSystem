﻿using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Directorate
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? RegionId { get; set; }

    public virtual Region? Region { get; set; }

    public virtual ICollection<School> Schools { get; set; } = new List<School>();
}
