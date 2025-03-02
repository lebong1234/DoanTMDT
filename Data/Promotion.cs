using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class Promotion
{
    public string PromotionId { get; set; } = null!;

    public string PromotionInfo { get; set; } = null!;

    public string Details { get; set; } = null!;

    public int DiscountAmount { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
