using System.ComponentModel.DataAnnotations;

namespace QLNT_API.DTO.Account
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
