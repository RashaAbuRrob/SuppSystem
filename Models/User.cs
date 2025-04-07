﻿using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class User
{
    public int MinistrialNumber { get; set; }

    public string? Name { get; set; }

    public bool? Official { get; set; }

    public bool? IsAdmin { get; set; }

    public string? Password { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<School> Schools { get; set; } = new List<School>();
}
