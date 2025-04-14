using Microsoft.AspNetCore.Mvc;
using QLNT_API.DTO.Role;
using QLNT_API.Services.RoleServices;

namespace QLNT_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO createRoleDto)
        {
            var role = await _roleService.CreateRoleAsync(createRoleDto);
            return Ok(role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] UpdateRoleDTO updateRoleDto)
        {
            var role = await _roleService.UpdateRoleAsync(id, updateRoleDto);
            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (result)
            {
                return Ok("Xóa quyền thành công.");
            }
            return BadRequest("Không thể xóa quyền.");
        }
    }
}
