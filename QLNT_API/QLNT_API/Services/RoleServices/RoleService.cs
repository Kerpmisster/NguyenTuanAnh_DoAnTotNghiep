using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLNT_API.DTO.Role;
using QLNT_API.Mapper;

namespace QLNT_API.Services.RoleServices
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(role => RoleMapper.ToRoleDto(role)).ToList();
        }

        public async Task<RoleDTO> CreateRoleAsync(CreateRoleDTO createRoleDto)
        {
            var role = RoleMapper.ToIdentityRole(createRoleDto);
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception("Không thể tạo quyền.");
            }
            return RoleMapper.ToRoleDto(role);
        }

        public async Task<RoleDTO> UpdateRoleAsync(string id, UpdateRoleDTO updateRoleDto)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                throw new Exception("Không tìm thấy quyền.");
            }

            role.Name = updateRoleDto.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception("Không thể cập nhật quyền.");
            }
            return RoleMapper.ToRoleDto(role);
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                throw new Exception("Không tìm thấy quyền.");
            }

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }
    }
}
