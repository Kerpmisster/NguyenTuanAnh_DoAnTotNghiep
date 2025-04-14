using QLNT_API.DTO.ProductExtension;
using QLNT_API.DTO.ProductImage;
using QLNT_API.DTO.ProductMaterial;

namespace QLNT_API.DTO.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int? Cid { get; set; }
        public string? Code { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public string? Slug { get; set; }
        public decimal? PriceOld { get; set; }
        public decimal? PriceNew { get; set; }
        public string? Size { get; set; }
        public int? Views { get; set; }
        public int? Likes { get; set; }
        public double? Star { get; set; }
        public byte? Home { get; set; }
        public byte? Hot { get; set; }
        public byte? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<ProductImageDto> ProductImages { get; set; } = new();
        public List<ProductExtensionDto> ProductExtensions { get; set; } = new();
        public List<ProductMaterialDto> ProductMaterials { get; set; } = new();
    }
}
