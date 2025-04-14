using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuanLyNoiThatHoangGia.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class BaseController : Controller, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var jwt = context.HttpContext.Session.GetString("JWT");

            if (string.IsNullOrEmpty(jwt))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { area = "Account", controller = "Account", action = "Login" }));
                return;
            }

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = null;

            try
            {
                token = handler.ReadJwtToken(jwt);
            }
            catch
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { area = "Account", controller = "Account", action = "Login" }));
                return;
            }

            var roleClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (roleClaim != "admin")
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { area = "Account", controller = "Account", action = "Login" }));
                return;
            }

            base.OnActionExecuting(context);
            //// Sau khi cố gắng lấy từ cookie mà vẫn không có thì redirect
            //if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("JWT")))
            //{
            //    context.Result = new RedirectToRouteResult(new RouteValueDictionary(
            //        new { Area = "Account", Controller = "Account", Action = "Login" }));
            //}

            //var sessionToken = context.HttpContext.Session.GetString("JWT");

            //// Nếu session chưa có token, thử lấy từ cookie
            //if (string.IsNullOrEmpty(sessionToken))
            //{
            //    var cookieToken = context.HttpContext.Request.Cookies["JWT"];

            //    if (!string.IsNullOrEmpty(cookieToken))
            //    {
            //        // Lưu token từ cookie vào session
            //        context.HttpContext.Session.SetString("JWT", cookieToken);

            //        var handler = new JwtSecurityTokenHandler();
            //        var jwtToken = handler.ReadJwtToken(cookieToken);
            //        var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            //        if (!string.IsNullOrEmpty(role))
            //        {
            //            context.HttpContext.Session.SetString("Role", role);
            //        }

            //        // Nếu là admin, gắn thêm cờ "AdminLogin" vào session (tuỳ bạn dùng cờ này ở đâu)
            //        if (role == "admin")
            //        {
            //            context.HttpContext.Session.SetString("AdminLogin", "true");
            //        }
            //    }
            //}
        }
        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    // Không cần xử lý sau action, có thể để trống
        //}
    }
}
