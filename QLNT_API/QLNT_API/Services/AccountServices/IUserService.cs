using QLNT_API.DTO.Account;

namespace QLNT_API.Services.AccountServices
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> RegisterAsync(RegisterDTO registerDto);
        Task<string> LoginAsync(LoginDTO loginDto);
        Task<UserDTO> UpdateUserAsync(string id, UpdateUserDTO updateUserDto);
        Task<bool> DeleteUserAsync(string userId);
        Task ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDto);
        //Task ConfirmEmailAsync(EmailConfirmationDTO dto);
        //Task DeleteUnconfirmedUsersAsync();
    }
}
