using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class LogoutController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
            return Json(new { status = true, msg = "Başarıyla çıkış yapıldı" + "<meta http-equiv='refresh' content='1;URL=/'>" });
        }
    }
}
