using E_Ticaret.Models;
using E_Ticaret.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Azure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using E_Ticaret.DTO;
using System.Net.Http.Headers;
using System.Text;
using E_Ticaret_API.Models;

namespace E_Ticaret.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            Tools.GenerateGuestToken(HttpContext);
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            var carts = new Cart();

            using (var httpClient = new HttpClient())
            {
                string token = Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                string guestToken = Request.Cookies["GuestToken"];
                if (!string.IsNullOrEmpty(guestToken))
                {
                    httpClient.DefaultRequestHeaders.Add("GuestToken", guestToken);
                }

                using (var response = await httpClient.GetAsync(api_url + "/Api/Cart"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    carts = JsonSerializer.Deserialize<Cart>(apiResponse);
                }
            }

            return View(carts);
        }

        public async Task<IActionResult> Get()
        {
            Tools.GenerateGuestToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            var carts = new Cart();

            using (var httpClient = new HttpClient())
            {
                string token = Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                string guestToken = Request.Cookies["GuestToken"];
                if (!string.IsNullOrEmpty(guestToken))
                {
                    httpClient.DefaultRequestHeaders.Add("GuestToken", guestToken);
                }

                using (var response = await httpClient.GetAsync(api_url + "/Api/Cart"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    carts = JsonSerializer.Deserialize<Cart>(apiResponse);
                }
            }

            return Ok(carts);
        }

        public async Task<IActionResult> Add(int? id, CartModel Form)
        {
            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (id == null || id == 0)
            {
                return BadRequest();
            }

            var jsonModel = JsonSerializer.Serialize(Form);

            var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                string token = Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                string guestToken = Request.Cookies["GuestToken"];
                if (!string.IsNullOrEmpty(guestToken))
                {
                    httpClient.DefaultRequestHeaders.Add("GuestToken", guestToken);
                }

                using (var response = await httpClient.PostAsync(api_url + "/Api/Cart/" + id, httpContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();

                        return Ok(apiResponse);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public async Task<IActionResult> Edit(int? id, CartModel Form)
        {
            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (id == null || id == 0)
            {
                return BadRequest();
            }

            var jsonModel = JsonSerializer.Serialize(Form);

            var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                string token = Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                string guestToken = Request.Cookies["GuestToken"];
                if (!string.IsNullOrEmpty(guestToken))
                {
                    httpClient.DefaultRequestHeaders.Add("GuestToken", guestToken);
                }

                using (var response = await httpClient.PutAsync(api_url + "/Api/Cart/" + id, httpContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();

                        return Ok(apiResponse);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (id == null || id == 0)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                string token = Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                string guestToken = Request.Cookies["GuestToken"];
                if (!string.IsNullOrEmpty(guestToken))
                {
                    httpClient.DefaultRequestHeaders.Add("GuestToken", guestToken);
                }

                using (var response = await httpClient.DeleteAsync(api_url + "/Api/Cart/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var res = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Ok(apiResponse);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public async Task<IActionResult> Coupon(CouponModel Form)
        {
            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            var jsonModel = JsonSerializer.Serialize(Form);

            var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                string token = Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    using (var response = await httpClient.PostAsync(api_url + "/Api/Cart/Coupon", httpContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();

                            return Ok(apiResponse);
                        }
                        else
                        {
                            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                        }
                    }
                }

                return Unauthorized();
            }
        }

        [HttpPost("Cart/Coupon/{id}")]
        public async Task<IActionResult> CouponDelete(int? id)
        {
            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (id == null || id == 0)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                string token = Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                using (var response = await httpClient.DeleteAsync(api_url + "/Api/Cart/Coupon/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var res = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Ok(apiResponse);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }
    }
}
