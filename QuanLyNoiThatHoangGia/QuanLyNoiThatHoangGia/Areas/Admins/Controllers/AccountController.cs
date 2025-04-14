using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyNoiThatHoangGia.Areas.Admins.Models;

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
    }
}
