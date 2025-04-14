using System.Net.Http.Headers;

namespace QuanLyNoiThatHoangGia.Handlers
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWT");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                // Log để debug (tùy chọn)
                System.Diagnostics.Debug.WriteLine("No token found in session");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
