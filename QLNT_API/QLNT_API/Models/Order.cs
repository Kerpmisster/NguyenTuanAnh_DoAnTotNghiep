using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime? OrdersDate { get; set; }

    public string? UserId { get; set; }

    public int? Idpayment { get; set; }

    public decimal? TotalMoney { get; set; }

    public string? NameReciver { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public byte? Isdelete { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual AspNetUser? User { get; set; }
}
