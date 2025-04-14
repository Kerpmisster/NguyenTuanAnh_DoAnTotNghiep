using Microsoft.AspNetCore.Identity;
using QLNT_API.DTO.Account;
using QLNT_API.Models;

namespace QLNT_API.Mapper
{
    public static class UserMapper
    {
        public static UserDTO ToUserDto(IdentityUser user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                //EmailConfirmed = user.EmailConfirmed,
                Roles = new List<string>() // Mặc định danh sách quyền rỗng
            };
        }

        public static UserDTO ToUserDto(IdentityUser user, IList<string> roles, Customer? customer)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                //EmailConfirmed = user.EmailConfirmed,
                Roles = roles.ToList(), // Chuyển IList<string> thành List<string>
                // Mở rộng từ Customer
                Fullname = customer?.Fullname,
                Address = customer?.Address,
                Avatar = customer?.Avatar,
                CreatedDate = customer?.CreatedDate,
                IsActive = customer?.Isactive == 1
            };
        }

        public static IdentityUser ToIdentityUser(RegisterDTO registerDto)
        {
            return new IdentityUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
        }
    }
}
