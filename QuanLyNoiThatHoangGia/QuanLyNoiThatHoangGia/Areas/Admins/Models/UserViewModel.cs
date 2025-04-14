﻿using Newtonsoft.Json;

namespace QuanLyNoiThatHoangGia.Areas.Admins.Models
{
    public class UserViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("roles")]
        public List<string> Roles { get; set; } // Sửa từ Role (string) thành Roles (List<string>)

        // Thuộc tính để hiển thị quyền trong View (lấy quyền đầu tiên hoặc nối các quyền)
        [JsonIgnore]
        public string DisplayRole => Roles != null && Roles.Count > 0 ? string.Join(", ", Roles) : "Không có quyền";
    }
}
