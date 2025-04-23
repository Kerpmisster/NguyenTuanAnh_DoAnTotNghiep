namespace QLNT_API.DTO.Category
{
    public class UpdateCategoryDTO
    {
        public string? Title { get; set; }
        public IFormFile? Icon { get; set; } // upload mới nếu muốn thay ảnh
        public string? MetaTitle { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public string? Slug { get; set; }
        public int? Orders { get; set; }
        public int? ParentId { get; set; }
        public byte? Status { get; set; }

    }
}
