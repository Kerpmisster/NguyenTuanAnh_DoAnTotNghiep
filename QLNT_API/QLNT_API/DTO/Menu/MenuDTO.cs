namespace QLNT_API.DTO.Menu
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public int? ParentId { get; set; }
        public int? Position { get; set; }
        public bool? IsActive { get; set; }
        public List<MenuDTO>? Children { get; set; } = new List<MenuDTO>();
    }
}
