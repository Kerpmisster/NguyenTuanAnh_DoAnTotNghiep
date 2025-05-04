using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyNoiThatHoangGia.Areas.Admins.Models;

namespace QuanLyNoiThatHoangGia.Areas.Admins.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7261/api/Category/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(content);
                foreach (var category in categories)
                {
                    category.Icon = "https://localhost:7261" + category.Icon;  // Thêm tiền tố URL
                }
                return View(categories);
            }

            return View(new List<CategoryViewModel>());
        }
    }
}
