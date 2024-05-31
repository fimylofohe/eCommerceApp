using E_Ticaret.Helpers;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace E_Ticaret.Controllers
{
    public class RegisterController : Controller
    {
        public async Task<IActionResult> Index()
        {
            Tools.GenerateGuestToken(HttpContext);
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;

            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterModel model)
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
            {
                return BadRequest();
            }
            else
            {
                var jsonModel = JsonSerializer.Serialize(model);

                var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync(api_url + "/Api/User/Register", httpContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            return Json(new { status = true, msg = "Başarıyla kayıt olundu" + "<meta http-equiv='refresh' content='1;URL=/'>" });
                        }

                        return BadRequest("Api isteği sırasında hata oluştu.");
                    }
                }
            }
        }
    }
}
