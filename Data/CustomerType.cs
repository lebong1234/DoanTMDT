using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class CustomerType
{
    public string CustomerTypeId { get; set; } = null!;

    public string CustomerTypeName { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
