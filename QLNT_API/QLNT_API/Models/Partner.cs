using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Partner
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Logo { get; set; }

    public string? Url { get; set; }

    public byte? Orders { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Description { get; set; }

    public byte? Status { get; set; }

    public bool? Isdelete { get; set; }
}
