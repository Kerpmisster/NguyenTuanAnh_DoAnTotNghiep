using System.ComponentModel.DataAnnotations;

namespace QLNT_API.DTO.Account
{
    public class EmailConfirmationDTO
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Code { get; set; }
    }
}
