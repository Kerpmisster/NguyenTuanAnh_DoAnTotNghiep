using QLNT_API.DTO.Product;

namespace QLNT_API.DTO.ProductExtension
{
    public class ProductExtensionDto
    {
        public int Id { get; set; }
        public int? Pid { get; set; }
        public int? Eid { get; set; }
        public ProductDTO Product { get; set; }
    }
}
