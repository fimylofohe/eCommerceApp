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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace E_Ticaret.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckoutController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            Tools.CheckToken(HttpContext);

            if (User.Identity!.IsAuthenticated)
            {
                var carts = new Cart();
                var address = new List<Addresses>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Cart"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        carts = JsonSerializer.Deserialize<Cart>(apiResponse);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/User/Address"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        address = JsonSerializer.Deserialize<List<Addresses>>(apiResponse);
                    }
                }

                ViewBag.Cart = carts;
                ViewBag.Address = address;

                if(carts.Total <= 0)
                {
                    return RedirectToAction("Index", "Cart");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutModel Form)
        {
            var jsonModel = JsonSerializer.Serialize(Form);

            var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                string token = Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    using (var response = await httpClient.PostAsync("https://localhost:7279/Api/Cart/Checkout", httpContent))
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
    }
}
