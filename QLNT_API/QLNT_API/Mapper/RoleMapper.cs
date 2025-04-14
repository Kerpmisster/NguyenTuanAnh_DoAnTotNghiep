using Microsoft.AspNetCore.Identity;
using QLNT_API.DTO.Role;

namespace QLNT_API.Mapper
{
    public static class RoleMapper
    {
        public static RoleDTO ToRoleDto(IdentityRole role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static IdentityRole ToIdentityRole(CreateRoleDTO createRoleDto)
        {
            return new IdentityRole
            {
                Name = createRoleDto.Name
            };
        }
    }
}
