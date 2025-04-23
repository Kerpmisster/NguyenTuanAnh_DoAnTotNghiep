using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public int? Cid { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public decimal? PriceNew { get; set; }

    public decimal? PriceOld { get; set; }

    public string? Slug { get; set; }

    public int? Views { get; set; }

    public int? Likes { get; set; }

    public byte? Home { get; set; }

    public byte? Hot { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public byte? Status { get; set; }

    public bool? Isdelete { get; set; }

    public virtual Category? CidNavigation { get; set; }

    public virtual ICollection<ImportReceiptDetail> ImportReceiptDetails { get; set; } = new List<ImportReceiptDetail>();

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual ICollection<ProductExtension> ProductExtensions { get; set; } = new List<ProductExtension>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();

    public virtual ICollection<WarehouseDetail> WarehouseDetails { get; set; } = new List<WarehouseDetail>();
}
