using System.ComponentModel.DataAnnotations;

namespace QuanLyNoiThatHoangGia.Areas.Account.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string? Email { get; set; }
    }
}
