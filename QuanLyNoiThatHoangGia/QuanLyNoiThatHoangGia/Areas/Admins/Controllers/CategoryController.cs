using Microsoft.AspNetCore.Mvc;

namespace QuanLyNoiThatHoangGia.Areas.Admins.Controllers
{
    public class CategoryController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
