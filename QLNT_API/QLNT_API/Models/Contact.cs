using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public byte? Status { get; set; }

    public bool? Isdelete { get; set; }
}
