using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyNoiThatHoangGia.Areas.Admins.Models;
using System.Net.Http.Headers;
using System.Text;

namespace QuanLyNoiThatHoangGia.Areas.Admins.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Users()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Account/Account/Login");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("https://localhost:7261/api/Account/GetUsers");

            List<UserViewModel> users = new List<UserViewModel>();
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(jsonString);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                // Handle error case (optional: log or return empty list)
                ViewData["Error"] = $"Không thể tải danh sách người dùng. Mã lỗi: {response.StatusCode} - {errorContent}";
            }

            // Handle error case
            return View("Users", users);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://localhost:7261/api/Account/GetById/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Content("<div class='text-danger'>Không thể tải dữ liệu người dùng.</div>", "text/html");
            }

            var json = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserViewModel>(json);

            return PartialView("_UserDetailPartial", user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [FromBody] UserViewModel model)
        {
            if (string.IsNullOrEmpty(id) || id != model.Id)
            {
                return Json(new { success = false, message = "ID không hợp lệ" });
            }

            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { success = false, message = "Chưa đăng nhập" });
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Tạo payload khớp với UpdateUserDTO
                var payload = new
                {
                    userName = model.UserName,
                    email = model.Email,
                    fullname = model.Fullname,
                    address = model.Address,
                    avatar = model.Avatar ?? "",
                    isActive = model.IsActive
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"https://localhost:7261/api/Account/UpdateUser/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }

                var error = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = $"Lỗi từ API: {response.StatusCode}. Nội dung: {error}" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi server: {ex.Message}" });
            }
        }


    }
}
