using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Customer
{
    public long Id { get; set; }

    public string? UserId { get; set; }

    public string? Fullname { get; set; }

    public string? Address { get; set; }

    public string? Avatar { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public byte? Isdelete { get; set; }

    public byte? Isactive { get; set; }

    public virtual AspNetUser? User { get; set; }
}
