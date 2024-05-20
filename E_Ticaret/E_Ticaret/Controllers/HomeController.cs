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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            Tools.GenerateGuestToken(HttpContext);
            Tools.CheckToken(HttpContext);

            var products = new Shop();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Data/Products/1"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<Shop>(apiResponse);
                }
            }

            var slider = new List<Slider>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Data/Slider"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    slider = JsonSerializer.Deserialize<List<Slider>>(apiResponse);
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

            ViewBag.Products = products;
            ViewBag.Slider = slider;
            ViewBag.Categories = categories;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
