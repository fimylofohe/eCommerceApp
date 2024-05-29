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
    public class BlogController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("Blog")]
        public async Task<IActionResult> Index(int page = 1)
        {
            Tools.GenerateGuestToken(HttpContext);
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            var blog = new Blogs();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/Api/Data/Blogs/" + page))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    blog = JsonSerializer.Deserialize<Blogs>(apiResponse);
                }
            }

            ViewBag.Blog = blog;
            ViewBag.Page = page;

            return View();
        }

        [HttpGet("Blog/{seo_url}")]
        public async Task<IActionResult> Index(string seo_url = null)
        {
            Tools.GenerateGuestToken(HttpContext);
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            var blog = new Blog();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/Api/Data/Blog/" + seo_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    blog = JsonSerializer.Deserialize<Blog>(apiResponse);
                }
            }

            ViewBag.Blog = blog;

            return View("Detail");
        }
    }
}
