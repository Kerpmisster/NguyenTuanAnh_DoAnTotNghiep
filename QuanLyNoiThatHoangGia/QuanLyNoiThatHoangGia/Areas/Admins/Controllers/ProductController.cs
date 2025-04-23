using Microsoft.AspNetCore.Mvc;

namespace QuanLyNoiThatHoangGia.Areas.Admins.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
