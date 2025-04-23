using QLNT_API.DTO.ProductExtension;

namespace QLNT_API.DTO.Extension
{
    public class ExtensionDTO
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

        public List<ExtensionDTO> Children { get; set; } = new List<ExtensionDTO>();

        public List<ProductExtensionDto> ProductExtensions { get; set; } = new List<ProductExtensionDto>();
    }
}
