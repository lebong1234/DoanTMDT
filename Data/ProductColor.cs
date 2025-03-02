using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class ProductColor
{
    public int ProductColorId { get; set; }

    public string ProductId { get; set; } = null!;

    public int ColorId { get; set; }

    public virtual Color Color { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
