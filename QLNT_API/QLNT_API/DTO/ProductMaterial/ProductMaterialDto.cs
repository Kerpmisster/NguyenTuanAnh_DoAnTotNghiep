using QLNT_API.DTO.Product;

namespace QLNT_API.DTO.ProductMaterial
{
    public class ProductMaterialDto
    {
        public int Id { get; set; }
        public int? Pid { get; set; }
        public int? Mid { get; set; }
        public ProductDTO Product { get; set; }

    }
}
