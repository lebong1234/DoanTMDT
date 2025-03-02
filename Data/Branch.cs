using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class Branch
{
    public string BranchId { get; set; } = null!;

    public string BranchName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
