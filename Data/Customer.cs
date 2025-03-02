using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string CustomerTypeId { get; set; } = null!;

    public virtual CustomerType CustomerType { get; set; } = null!;

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
