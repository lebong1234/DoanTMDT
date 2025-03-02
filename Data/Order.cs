using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public DateOnly OrderDate { get; set; }

    public int TotalAmount { get; set; }

    public string OrderStatus { get; set; } = null!;

    public string? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Revenue> Revenues { get; set; } = new List<Revenue>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
