using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QLNT_API.DTO.Account;
using QLNT_API.DTO.Extension;
using QLNT_API.Helper;
using QLNT_API.Mapper;
using QLNT_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QLNT_API.Services.AccountServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly QuanlynoithatContext _context;


        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, QuanlynoithatContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList(); // Lấy danh sách users
            var userDtos = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); // Lấy danh sách quyền bất đồng bộ
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.UserId == user.Id);
                userDtos.Add(UserMapper.ToUserDto(user, roles, customer));
            }
            return userDtos;
        }

        public async Task<UserDTO?> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            return UserMapper.ToUserDto(user, roles, customer);
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                throw new Exception("Mật khẩu và mật khẩu xác nhận không khớp.");
            }

            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email đã tồn tại.");
            }

            var user = UserMapper.ToIdentityUser(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Đăng ký thất bại: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await _userManager.AddToRoleAsync(user, "user");

            var customer = new Customer
            {
                UserId = user.Id,
                Fullname = registerDto.Fullname,
                Address = registerDto.Address,
                Avatar = registerDto.Avatar,
                CreatedDate = DateTime.Now,
                Isactive = 1,
                Isdelete = 0
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var roles = await _userManager.GetRolesAsync(user);

            //// Tạo mã xác nhận
            //var code = new Random().Next(100000, 999999).ToString(); // hoặc dùng TokenProvider
            //var confirmation = new EmailConfirmation
            //{
            //    UserId = user.Id,
            //    Code = code,
            //    CreatedAt = DateTime.UtcNow
            //};

            //_context.EmailConfirmations.Add(confirmation);
            //await _context.SaveChangesAsync();

            //// Gửi email
            //await SendConfirmationEmailAsync(user.Email, code);


            return UserMapper.ToUserDto(user, new List<string>(),null);
        }

        public async Task<string> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new Exception("Email không tồn tại.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDto.Password, false, false);
            if (!result.Succeeded)
            {
                throw new Exception("Đăng nhập thất bại.");
            }
            var roles = await _userManager.GetRolesAsync(user);
            // Tạo JWT token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserDTO> UpdateUserAsync(string id, UpdateUserDTO updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("Không tìm thấy tài khoản.");
            }

            // Cập nhật thông tin cơ bản
            user.UserName = string.IsNullOrWhiteSpace(updateUserDto.UserName) ? user.UserName : updateUserDto.UserName;
            user.Email = string.IsNullOrWhiteSpace(updateUserDto.Email) ? user.Email : updateUserDto.Email;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                throw new Exception("Cập nhật tài khoản thất bại: " + string.Join(", ", updateResult.Errors.Select(e => e.Description)));
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == user.Id);
            if (customer != null)
            {
                // Nếu đã có, thì cập nhật
                customer.Fullname = string.IsNullOrWhiteSpace(updateUserDto.Fullname) ? customer.Fullname : updateUserDto.Fullname;
                customer.Address = string.IsNullOrWhiteSpace(updateUserDto.Address) ? customer.Address : updateUserDto.Address;
                customer.Avatar = string.IsNullOrWhiteSpace(updateUserDto.Avatar) ? customer.Avatar : updateUserDto.Avatar;
                customer.UpdateDate = DateTime.Now;
                customer.Isactive = updateUserDto.IsActive ? (byte)1 : (byte)0;

                _context.Customers.Update(customer);
            }
            else
            {
                // Nếu chưa có thì thêm mới
                customer = new Customer
                {
                    UserId = user.Id,
                    Fullname = updateUserDto.Fullname ?? "",
                    Address = updateUserDto.Address ?? "",
                    Avatar = updateUserDto.Avatar ?? "",
                    CreatedDate = DateTime.Now,
                    Isactive = updateUserDto.IsActive ? (byte)1 : (byte)0,
                    Isdelete = 0
                };

                _context.Customers.Add(customer);
            }

            await _context.SaveChangesAsync();

            // Xóa tất cả quyền hiện tại và gán lại quyền mới
            //if (updateUserDto.Roles != null && updateUserDto.Roles.Any())
            //{
            //    var currentRoles = await _userManager.GetRolesAsync(user);

            //    // So sánh danh sách roles cũ và mới (dạng chữ thường để tránh phân biệt hoa thường)
            //    var currentRolesNormalized = currentRoles.Select(r => r.ToLower()).OrderBy(r => r);
            //    var newRolesNormalized = updateUserDto.Roles.Select(r => r.ToLower()).OrderBy(r => r);

            //    // Nếu roles khác thì mới cập nhật
            //    if (!currentRolesNormalized.SequenceEqual(newRolesNormalized))
            //    {
            //        await _userManager.RemoveFromRolesAsync(user, currentRoles);
            //        var addRolesResult = await _userManager.AddToRolesAsync(user, updateUserDto.Roles);
            //        if (!addRolesResult.Succeeded)
            //        {
            //            throw new Exception("Gán quyền thất bại: " + string.Join(", ", addRolesResult.Errors.Select(e => e.Description)));
            //        }
            //    }
            //}

            // Lấy lại danh sách quyền sau khi cập nhật
            var updatedRoles = await _userManager.GetRolesAsync(user);
            return UserMapper.ToUserDto(user, updatedRoles, customer);
        }

        public async Task ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDto)
        {
            // Kiểm tra xác nhận mật khẩu mới
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
            {
                throw new Exception("Mật khẩu mới và mật khẩu xác nhận không khớp.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy tài khoản.");
            }

            // Thay đổi mật khẩu
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Đổi mật khẩu thất bại: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            // Tìm user trong Identity
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy tài khoản.");
            }

            // Xóa bản ghi mở rộng trong Customer (nếu có)
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == user.Id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync(); // Lưu lại trước khi xóa user để tránh khóa ngoại
            }

            // Xóa user trong Identity
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Xóa tài khoản thất bại: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return true;
        }

        public async Task<bool> UserExists(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user != null;
        }

        public async Task AssignRolesToUserAsync(string userId, List<string>? roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy tài khoản.");
            }

            if (roles == null || !roles.Any())
            {
                // Không gán quyền nếu không truyền gì cả (giữ nguyên quyền cũ)
                return;
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            var currentNormalized = currentRoles.Select(r => r.ToLower()).OrderBy(r => r);
            var newNormalized = roles.Select(r => r.ToLower()).OrderBy(r => r);

            if (!currentNormalized.SequenceEqual(newNormalized))
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    throw new Exception("Xóa quyền cũ thất bại: " + string.Join(", ", removeResult.Errors.Select(e => e.Description)));
                }

                var addResult = await _userManager.AddToRolesAsync(user, roles);
                if (!addResult.Succeeded)
                {
                    throw new Exception("Gán quyền mới thất bại: " + string.Join(", ", addResult.Errors.Select(e => e.Description)));
                }
            }
        }

        public async Task<PagedResult<UserDTO>> GetPagedAsync(int page, int pageSize)
        {
            var query = _userManager.Users.AsQueryable();

            var pagedResult = await query.ToPagedResultAsync(page, pageSize);

            var userDTOs = pagedResult.Items.Select(user => new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
                // Bạn có thể thêm các trường khác nếu cần
            }).ToList();

            return new PagedResult<UserDTO>
            {
                Items = userDTOs,
                TotalItems = pagedResult.TotalItems,
                PageNumber = page,
                PageSize = pageSize
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        //private async Task SendConfirmationEmailAsync(string email, string token)
        //{
        //    var confirmUrl = $"https://your-frontend.com/confirm-email?email={Uri.EscapeDataString(email)}&code={Uri.EscapeDataString(token)}";

        //    var subject = "Xác nhận đăng ký tài khoản";
        //    var body = $"Vui lòng nhấn vào liên kết sau để xác nhận tài khoản của bạn:\n{confirmUrl}";

        //    // Gửi email bằng SMTP hoặc dịch vụ bên ngoài
        //    await EmailHelper.SendAsync(email, subject, body); // Tạo lớp EmailHelper tùy bạn
        //}

        //public async Task ConfirmEmailAsync(EmailConfirmationDTO dto)
        //{
        //    var user = await _userManager.FindByEmailAsync(dto.Email);
        //    if (user == null)
        //        throw new Exception("Không tìm thấy người dùng.");

        //    var result = await _userManager.ConfirmEmailAsync(user, dto.Code);
        //    if (!result.Succeeded)
        //        throw new Exception("Mã xác nhận không hợp lệ hoặc đã hết hạn.");
        //}

        //public async Task<bool> ConfirmEmailAsync(string userId, string code)
        //{
        //    var confirmation = await _context.EmailConfirmations
        //        .FirstOrDefaultAsync(x => x.UserId == userId && x.Code == code);

        //    if (confirmation == null || confirmation.CreatedAt.AddMinutes(10) < DateTime.UtcNow)
        //        return false;

        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //        return false;

        //    user.EmailConfirmed = true;
        //    await _userManager.UpdateAsync(user);

        //    _context.EmailConfirmations.Remove(confirmation);
        //    await _context.SaveChangesAsync();

        //    return true;
        //}

        //public async Task CleanupExpiredUnconfirmedUsersAsync()
        //{
        //    var expiredConfirmations = await _context.EmailConfirmations
        //        .Where(ec => ec.CreatedAt.AddMinutes(10) < DateTime.UtcNow)
        //        .ToListAsync();

        //    foreach (var ec in expiredConfirmations)
        //    {
        //        var user = await _userManager.FindByIdAsync(ec.UserId);
        //        if (user != null && !user.EmailConfirmed)
        //        {
        //            await _userManager.DeleteAsync(user);
        //        }
        //        _context.EmailConfirmations.Remove(ec);
        //    }

        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteUnconfirmedUsersAsync()
        //{
        //    var users = _userManager.Users.Where(u => !u.EmailConfirmed);

        //    foreach (var user in users)
        //    {
        //        var createdDate = await _userManager.GetCreationDate(user); // cần custom lại User để lưu thời gian tạo
        //        if (createdDate.AddHours(24) < DateTime.UtcNow)
        //        {
        //            await _userManager.DeleteAsync(user);
        //        }
        //    }
        //}
    }
}
