using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Orderdetail
{
    public long Id { get; set; }

    public long? IdOrder { get; set; }

    public int? IdProduct { get; set; }

    public decimal? Price { get; set; }

    public int? Qty { get; set; }

    public decimal? Total { get; set; }

    public int? ReturnQty { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
