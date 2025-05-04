using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyNoiThatHoangGia.Areas.Admins.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace QuanLyNoiThatHoangGia.Areas.Admins.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RoleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); // ✅ tạo HttpClient từ factory
            var response = await client.GetAsync("https://localhost:7261/api/Role/GetRoles");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(content);
                return View(roles);
            }

            return View(new List<RoleViewModel>());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_CreateRolePartial");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateRolePartial", model);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonContent = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7261/api/Role/CreateRole", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Tạo quyền thất bại!");
            return PartialView("_CreateRolePartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7261/api/Role/DeleteRole/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Xóa quyền thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Xóa quyền thất bại!" });
            }
        }
    }
}
