using QLNT_API.DTO.Product;

namespace QLNT_API.DTO.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Icon { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public string? Slug { get; set; }
        public int? Orders { get; set; }
        public int? ParentId { get; set; }
        public byte? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<CategoryDTO> Children { get; set; } = new List<CategoryDTO>();

        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }
}
