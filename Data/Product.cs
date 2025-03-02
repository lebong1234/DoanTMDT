using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int StockQuantity { get; set; }

    public int OriginalPrice { get; set; }

    public int DiscountedPrice { get; set; }

    public string Status { get; set; } = null!;
    public string? ImageUrl { get; set; } // Nullable
    public string? CategoryId { get; set; } // Nullable
    public string? SupplierId { get; set; } // Nullable
    public string? BrandId { get; set; } // Đã nullable rồi

    public virtual Brand? Brand { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual Supplier Supplier { get; set; } = null!;
}
