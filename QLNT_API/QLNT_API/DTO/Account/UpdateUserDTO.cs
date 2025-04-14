namespace QLNT_API.DTO.Account
{
    public class UpdateUserDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; } // Danh sách quyền mới để gán

        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public bool IsActive { get; set; }
    }
}
