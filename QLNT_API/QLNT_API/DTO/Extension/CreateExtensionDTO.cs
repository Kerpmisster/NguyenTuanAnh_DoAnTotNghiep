namespace QLNT_API.DTO.Extension
{
    public class CreateExtensionDTO
    {
        public string? Title { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? Slug { get; set; }
        public int? Parentid { get; set; }
        public byte? Status { get; set; }

        public int ProductId { get; set; }
    }
}
