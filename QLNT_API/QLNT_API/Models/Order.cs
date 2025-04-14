using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Order
{
    public long Id { get; set; }

    public long? IdOrders { get; set; }

    public long? IdCustomer { get; set; }

    public long? IdPayment { get; set; }

    public DateTime? OrdersDate { get; set; }

    public decimal? TotalMoney { get; set; }

    public string? Notes { get; set; }

    public string? NameReciver { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public byte? Isdelete { get; set; }

    public byte? Isactive { get; set; }

    public virtual Customer? IdCustomerNavigation { get; set; }

    public virtual Orderdetail? IdOrdersNavigation { get; set; }
}
