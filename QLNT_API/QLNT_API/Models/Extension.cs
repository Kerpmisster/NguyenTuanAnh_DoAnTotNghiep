using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Extension
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Icon { get; set; }

    public string? MetaTitle { get; set; }

    public string? MetaKeyword { get; set; }

    public string? MetaDescription { get; set; }

    public string? Slug { get; set; }

    public int? Orders { get; set; }

    public int? Parentid { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? AdminCreated { get; set; }

    public string? AdminUpdated { get; set; }

    public string? Notes { get; set; }

    public byte? Status { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Extension> InverseParent { get; set; } = new List<Extension>();

    public virtual Extension? Parent { get; set; }

    public virtual ICollection<ProductExtension> ProductExtensions { get; set; } = new List<ProductExtension>();
}
