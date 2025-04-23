using Microsoft.AspNetCore.Mvc;

namespace QuanLyNoiThatHoangGia.Areas.User.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
