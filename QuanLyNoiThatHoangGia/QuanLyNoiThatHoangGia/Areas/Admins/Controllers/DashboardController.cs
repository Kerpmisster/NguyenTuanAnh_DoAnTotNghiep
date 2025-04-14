using Microsoft.AspNetCore.Mvc;

namespace QuanLyNoiThatHoangGia.Areas.Admins.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
