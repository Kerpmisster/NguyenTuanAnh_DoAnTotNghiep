namespace QLNT_API.DTO.Category
{
    public class CreateCategoryDTO
    {
        public string? Title { get; set; }
        public IFormFile? Icon { get; set; } // dùng để upload ảnh
        public string? MetaTitle { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public string? Slug { get; set; }
        public int? Orders { get; set; }
        public int? ParentId { get; set; }
        public byte? Status { get; set; }
    }
}
