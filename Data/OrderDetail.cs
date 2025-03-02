using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public string OrderId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }

    public string AdditionalInfo { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
