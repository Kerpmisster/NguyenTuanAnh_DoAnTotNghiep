﻿using Microsoft.AspNetCore.Mvc;

namespace QuanLyNoiThatHoangGia.Areas.User.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
