using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Introduction
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? Slug { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public byte? Status { get; set; }

    public bool? Isdelete { get; set; }
}
