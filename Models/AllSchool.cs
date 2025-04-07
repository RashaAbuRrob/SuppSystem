using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class AllSchool
{
    public int? Id { get; set; }

    public string? Region { get; set; }

    public string? Directorate { get; set; }

    public int NationalId { get; set; }

    public string? Name { get; set; }

    public string? MinGrade { get; set; }

    public string? MaxGrade { get; set; }

    public int? LabsCount { get; set; }

    public int? AcadimicLabsCount { get; set; }

    public int? BteclabsCount { get; set; }

    public int? ComputersCountLab1 { get; set; }

    public int? ComputersCountLab2 { get; set; }

    public int? ComputersCountLab3 { get; set; }

    public int? ComputersCountLab4 { get; set; }

    public int? DirectorateId { get; set; }
}
