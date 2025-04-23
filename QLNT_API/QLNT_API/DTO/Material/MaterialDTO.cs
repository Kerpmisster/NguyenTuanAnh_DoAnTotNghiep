using QLNT_API.DTO.ProductMaterial;

namespace QLNT_API.DTO.Material
{
    public class MaterialDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? Slug { get; set; }
        public int? Parentid { get; set; }
        public byte? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<MaterialDTO> Children { get; set; } = new List<MaterialDTO>();

        public List<ProductMaterialDto> ProductMaterials { get; set; } = new List<ProductMaterialDto>();
    }
}
