namespace QuanLyNoiThatHoangGia.Areas.Admins.Models
{
    public class PagedResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public List<T> Items { get; set; } = new List<T>();
    }
}
