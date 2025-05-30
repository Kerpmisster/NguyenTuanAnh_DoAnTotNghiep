﻿using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Material
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? Slug { get; set; }

    public int? Parentid { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public byte? Status { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Material> InverseParent { get; set; } = new List<Material>();

    public virtual Material? Parent { get; set; }

    public virtual ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
}
