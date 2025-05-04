namespace QLNT_API.DTO.Product
{
    public class FilterDTO
    {
        public string? Title { get; set; }         // Tìm theo tên sản phẩm
        public int? CategoryId { get; set; }        // Lọc theo loại sản phẩm (Category)
        public decimal? PriceMin { get; set; }      // Giá từ
        public decimal? PriceMax { get; set; }      // Giá đến
    }
}
