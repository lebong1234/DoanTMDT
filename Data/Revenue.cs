using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class Revenue
{
    public int RevenueId { get; set; }

    public DateOnly RevenueDate { get; set; }

    public int RevenueWeek { get; set; }

    public int RevenueMonth { get; set; }

    public int RevenueYear { get; set; }

    public decimal TotalRevenue { get; set; }

    public int TotalOrders { get; set; }

    public DateOnly OrderDate { get; set; }

    public virtual Order OrderDateNavigation { get; set; } = null!;
}
