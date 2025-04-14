using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyNoiThatHoangGia.Areas.Account.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuanLyNoiThatHoangGia.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login() => PartialView("_LoginPartial");
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Trả về JSON chứa thông tin lỗi thay vì PartialView
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }

            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7261/api/Account/Login/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var authData = JsonConvert.DeserializeObject<AuthResponseModel>(responseContent);

                // Lưu vào cookie nếu RememberMe được bật
                if (model.RememberMe)
                {
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddDays(7),
                        HttpOnly = true
                    };
                    Response.Cookies.Append("JWT", authData.Token, cookieOptions);
                }
                else
                {
                    HttpContext.Session.SetString("JWT", authData.Token);
                }

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(authData.Token);
                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                var username = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("Username", username);

                // Trả về JSON với redirectUrl dựa trên role
                string redirectUrl = role switch
                {
                    "admin" => Url.Action("Index", "Dashboard", new { area = "Admins" }),
                    "user" => Url.Action("Index", "Home", new { area = "User" }),
                    _ => Url.Action("Index", "Home", new { area = "" })
                };

                return Json(new { success = true, redirectUrl = redirectUrl });

            }

            return Json(new { success = false, errors = new[] { "Đăng nhập thất bại" } });
        }

        [HttpGet]
        public IActionResult Register() => PartialView("_Register", new RegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Dữ liệu không hợp lệ: " + string.Join(", ", errors) });
            }

            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7261/api/Account/Register/register", content);


            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Đăng ký thành công!" });
            }
            else
            {
                var resultContent = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = resultContent });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
