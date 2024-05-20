using E_Ticaret.Models;
using E_Ticaret.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Azure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using E_Ticaret.DTO;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace E_Ticaret.Controllers
{
    public class ShopController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShopController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("Shop")]
        public async Task<IActionResult> Index(int page = 1, int cat = 0)
        {
            Tools.GenerateGuestToken(HttpContext);
            Tools.CheckToken(HttpContext);

            var shop = new Shop();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Data/Products/" + page + "?cat=" + cat))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    shop = JsonSerializer.Deserialize<Shop>(apiResponse);
                }
            }

            var categories = new List<Category>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Data/Categories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<List<Category>>(apiResponse);
                }
            }

            ViewBag.Shop = shop;
            ViewBag.Categories = categories;
            ViewBag.Page = page;
            ViewBag.Cat = cat;

            return View();
        }
    }
}
