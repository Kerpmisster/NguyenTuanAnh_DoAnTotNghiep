namespace QLNT_API.DTO.Account
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        //public bool EmailConfirmed { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        // Mở rộng từ Customer
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
