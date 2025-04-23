namespace QLNT_API.DTO.Product
{
    public class UpdateProductDTO
    {
        //public int Id { get; set; }
        public int? Cid { get; set; }
        public string? Code { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public IFormFile? Image { get; set; } // <--- Upload ảnh
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? Slug { get; set; }
        public decimal? PriceOld { get; set; }
        public decimal? PriceNew { get; set; }
        public byte? Home { get; set; }
        public byte? Hot { get; set; }
        public byte? Status { get; set; }
    }
}
