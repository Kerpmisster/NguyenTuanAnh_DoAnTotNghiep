using Microsoft.AspNetCore.Mvc;

namespace QuanLyNoiThatHoangGia.Areas.User.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
