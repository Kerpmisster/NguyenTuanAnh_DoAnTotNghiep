using QLNT_API.DTO.Role;

namespace QLNT_API.Services.RoleServices
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO> CreateRoleAsync(CreateRoleDTO createRoleDto);
        Task<RoleDTO> UpdateRoleAsync(string id, UpdateRoleDTO updateRoleDto);
        Task<bool> DeleteRoleAsync(string id);
    }
}
