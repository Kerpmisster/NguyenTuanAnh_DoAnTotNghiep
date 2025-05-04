namespace QuanLyNoiThatHoangGia.Areas.Admins.Models
{
    public class CategoryViewModel
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
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<CategoryViewModel> Children { get; set; } = new();
    }
}
