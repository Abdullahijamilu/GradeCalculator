using System;
using System.Collections.Generic;

namespace GradeCalculator.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CreditUnit { get; set; }

    public string? Grade { get; set; }
}
