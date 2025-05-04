using Microsoft.AspNetCore.Mvc;

namespace QuanLyNoiThatHoangGia.Areas.User.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
