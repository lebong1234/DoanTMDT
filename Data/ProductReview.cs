using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class ProductReview
{
    public string ReviewId { get; set; } = null!;

    public string CustomerId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int Rating { get; set; }

    public string ReviewText { get; set; } = null!;

    public DateOnly ReviewDate { get; set; }

    public string? EmployeeId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual Product Product { get; set; } = null!;
}
