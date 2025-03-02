using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class Color
{
    public int ColorId { get; set; }

    public string ColorName { get; set; } = null!;

    public string HexCode { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public virtual ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();
}
