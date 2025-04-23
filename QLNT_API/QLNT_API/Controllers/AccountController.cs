using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLNT_API.DTO.Account;
using QLNT_API.Services.AccountServices;
using System.Security.Claims;

namespace QLNT_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(int page = 1, int pageSize = 10)
        {
            var result = await _userService.GetPagedAsync(page, pageSize);
            return Ok(result);
        }

        // Hiển thị danh sách tài khoản
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await _userService.GetById(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // Đăng ký tài khoản
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                var user = await _userService.RegisterAsync(registerDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Đăng nhập
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var token = await _userService.LoginAsync(loginDto);
            return Ok(new { Token = token });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Chỉ admin mới được chỉnh sửa
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDTO updateUserDto)
        {
            var user = await _userService.UpdateUserAsync(id, updateUserDto);
            return Ok(user);
        }

        [HttpPut("{id}/roles")]
        public async Task<IActionResult> AssignRoles(string id, [FromBody] List<string> roles)
        {
            try
            {
                await _userService.AssignRolesToUserAsync(id, roles);
                return Ok(new { message = "Gán quyền thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Thêm endpoint đổi mật khẩu
        [HttpPost("change-password")]
        [Authorize] // Yêu cầu người dùng đã đăng nhập
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDto)
        {
            try
            {
                // Lấy userId từ token JWT
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Không thể xác định người dùng.");
                }

                await _userService.ChangePasswordAsync(userId, changePasswordDto);
                return Ok(new { Message = "Đổi mật khẩu thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                return result ? Ok("Tài khoản đã được xóa.") : BadRequest("Xóa tài khoản thất bại.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("confirm-email")]
        //public async Task<IActionResult> ConfirmEmail([FromBody] EmailConfirmationDTO dto)
        //{
        //    await _userService.ConfirmEmailAsync(dto);
        //    return Ok("Email xác nhận thành công.");
        //}

        //[HttpPost("cleanup-unconfirmed")]
        //public async Task<IActionResult> CleanupUnconfirmed()
        //{
        //    await _userService.DeleteUnconfirmedUsersAsync();
        //    return Ok("Đã dọn dẹp tài khoản chưa xác nhận.");
        //}


    }
}
