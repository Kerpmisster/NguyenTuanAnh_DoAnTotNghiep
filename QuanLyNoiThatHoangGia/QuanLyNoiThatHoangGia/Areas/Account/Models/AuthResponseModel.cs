namespace QuanLyNoiThatHoangGia.Areas.Account.Models
{
    public class AuthResponseModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
