using E_Ticaret.DTO;
using E_Ticaret.Models;
using E_Ticaret.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace E_Ticaret.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            Tools.GenerateGuestToken(HttpContext);
            Tools.CheckToken(HttpContext);

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
        public async Task<IActionResult> Index(LoginModel Form)
        {
            Tools.CheckToken(HttpContext);

            if (User.Identity!.IsAuthenticated)
            {
                return BadRequest();
            }
            else
            {
                var jsonModel = JsonSerializer.Serialize(Form);

                var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync("https://localhost:7279/Api/User/Login", httpContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var login_data = JsonSerializer.Deserialize<Login>(apiResponse);

                            if (login_data.Token != null)
                            {
                                Response.Cookies.Append("token", login_data.Token);
                                var userClaims = new List<Claim>();

                                userClaims.Add(new Claim("Name", login_data.User.Name));
                                userClaims.Add(new Claim("Surname", login_data.User.Surname));
                                userClaims.Add(new Claim("NameSurname", login_data.User.NameSurname));
                                userClaims.Add(new Claim("Email", login_data.User.Email));
                                userClaims.Add(new Claim("PhoneNumber", login_data.User.PhoneNumber));

                                if (login_data.User.Admin == true)
                                {
                                    userClaims.Add(new Claim("Role", "Admin"));
                                }

                                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                                var authProperties = new AuthenticationProperties
                                {
                                    IsPersistent = true
                                };

                                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                                await HttpContext.SignInAsync(
                                    CookieAuthenticationDefaults.AuthenticationScheme,
                                    new ClaimsPrincipal(claimsIdentity),
                                    authProperties);

                                return Json(new { status = true, msg = "Başarıyla giriş yapıldı" + "<meta http-equiv='refresh' content='1;URL=/'>" });
                            }
                            else
                            {
                                return Json(new { status = false, msg = "Token değeri alınamadı." });
                            }
                        }

                        return Json(new { status = false, msg = "E-Posta adresiniz veya Şifreniz hatalı." });
                    }
                }
            }
        }
    }
}
