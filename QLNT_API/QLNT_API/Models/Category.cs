using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Category
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

    public byte? Status { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    public virtual Category? Parent { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
