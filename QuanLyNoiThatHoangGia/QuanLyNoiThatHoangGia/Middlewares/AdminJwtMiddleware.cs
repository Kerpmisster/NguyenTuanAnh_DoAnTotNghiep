using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuanLyNoiThatHoangGia.Middlewares
{
    public class AdminJwtMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminJwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            // Chỉ áp dụng middleware cho area Admins
            if (path != null && path.StartsWith("/admins"))
            {
                var token = context.Session.GetString("JWT") ?? context.Request.Cookies["JWT"];

                if (string.IsNullOrEmpty(token))
                {
                    // Nếu chưa có session thì thử lấy từ cookie
                    var cookieJwt = context.Request.Cookies["JWT"];
                    if (!string.IsNullOrEmpty(cookieJwt))
                    {
                        context.Session.SetString("JWT", cookieJwt);
                    }
                    else
                    {
                        // Redirect về trang đăng nhập
                        context.Response.Redirect("/Account/Account/Login");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
