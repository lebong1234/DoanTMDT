using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class Assignment
{
    public int AssignmentId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string BranchId { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
