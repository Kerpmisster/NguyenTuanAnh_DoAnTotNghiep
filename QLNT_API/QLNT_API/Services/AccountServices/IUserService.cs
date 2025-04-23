using QLNT_API.DTO.Account;
using QLNT_API.DTO.Extension;
using QLNT_API.Helper;

namespace QLNT_API.Services.AccountServices
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetById(string id);
        Task<bool> UserExists(string id);
        Task<PagedResult<UserDTO>> GetPagedAsync(int page, int pageSize);
        Task<UserDTO> RegisterAsync(RegisterDTO registerDto);
        Task<string> LoginAsync(LoginDTO loginDto);
        Task AssignRolesToUserAsync(string userId, List<string> roles);
        Task<UserDTO> UpdateUserAsync(string id, UpdateUserDTO updateUserDto);
        Task<bool> DeleteUserAsync(string userId);
        Task ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDto);
        //Task ConfirmEmailAsync(EmailConfirmationDTO dto);
        //Task DeleteUnconfirmedUsersAsync();
    }
}
