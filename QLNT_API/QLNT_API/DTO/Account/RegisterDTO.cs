using System.ComponentModel.DataAnnotations;

namespace QLNT_API.DTO.Account
{
    public class RegisterDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
    }
}
