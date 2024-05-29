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
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Microsoft.Net.Http.Headers;
using Iyzipay.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Iyzipay.Model.V2.Subscription;

namespace E_Ticaret.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Datas"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        JObject jsonResponse = JObject.Parse(apiResponse);
                        ViewBag.Datas = jsonResponse;
                    }
                }

                return View("Index");
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Orders")]
        public async Task<IActionResult> Orders()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var orders = new List<Orders>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Orders/10"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        orders = JsonSerializer.Deserialize<List<Orders>>(apiResponse);
                    }
                }

                ViewBag.Orders = orders;
                return View("Orders");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Orders/{status}")]
        public async Task<IActionResult> Orders(int status = 10)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var orders = new List<Orders>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Orders/" + status))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        orders = JsonSerializer.Deserialize<List<Orders>>(apiResponse);
                    }
                }

                ViewBag.Orders = orders;
                return View("Orders");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Order/{id}")]
        public async Task<IActionResult> GetOrders(int id = 0)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var orders = new Orders();
                var banks = new List<Bank>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Order/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        orders = JsonSerializer.Deserialize<Orders>(apiResponse);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Data/Banks/"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        banks = JsonSerializer.Deserialize<List<Bank>>(apiResponse);
                    }
                }

                ViewBag.Orders = orders;
                ViewBag.Banks = banks;
                return View("Order");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Order/{id}")]
        public async Task<IActionResult> EditOrders(OrderModel Form, int id = 0)
        {
            if (await CheckAdmin() == true)
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
                    }

                    using (var response = await httpClient.PutAsync(api_url + "/Api/Admin/Order/" + id, httpContent))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Users")]
        public async Task<IActionResult> Users()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var users = new List<User>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Users"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        users = JsonSerializer.Deserialize<List<User>>(apiResponse);
                    }
                }

                ViewBag.Users = users;
                return View("Users");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/User/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var user = new User();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/User/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonSerializer.Deserialize<User>(apiResponse);
                    }
                }

                ViewBag.User = user;
                return View("User");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/User/{id}")]
        public async Task<IActionResult> PostUser(UserModel Form, int id)
        {
            if (await CheckAdmin() == true)
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
                    }

                    using (var response = await httpClient.PutAsync(api_url + "/Api/Admin/User/" + id, httpContent))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Products")]
        public async Task<IActionResult> Products()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var products = new List<DTO.Product>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Products"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        products = JsonSerializer.Deserialize<List<DTO.Product>>(apiResponse);
                    }
                }

                ViewBag.Products = products;
                return View("Products");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Product/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var categories = new List<Category>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Categories"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        categories = JsonSerializer.Deserialize<List<Category>>(apiResponse);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Product/" + id))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var product = JsonSerializer.Deserialize<DTO.Product>(apiResponse);

                        ViewBag.Product = product;
                        ViewBag.Categories = categories;
                        return View("Product");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Product/{id}")]
        public async Task<IActionResult> PostProduct(ProductModel Form, int id)
        {
            if (await CheckAdmin() == true)
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
                    }

                    using (var response = await httpClient.PutAsync(api_url + "/Api/Admin/Product/" + id, httpContent))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Images/{id}")]
        public async Task<IActionResult> GetImages(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Images/" + id))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var picture = JsonSerializer.Deserialize<List<Picture>>(apiResponse);

                        return Ok(picture);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Image/{id}")]
        public async Task<IActionResult> SetImages(IFormFile file, int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var extent = Path.GetExtension(file.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var jsonModel = JsonSerializer.Serialize(new { file = randomName });

                var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.PostAsync(api_url + "/Api/Admin/Image/" + id, httpContent))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpDelete("Admin/Image/{id}")]
        public async Task<IActionResult> DeleteImages(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.DeleteAsync(api_url + "/Api/Admin/Image/" + id))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Categories")]
        public async Task<IActionResult> Categories()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var categories = new List<Category>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Categories"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        categories = JsonSerializer.Deserialize<List<Category>>(apiResponse);
                    }
                }

                ViewBag.Categories = categories;
                return View("Categories");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Category/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Category/" + id))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var category = JsonSerializer.Deserialize<Category>(apiResponse);

                        ViewBag.Category = category;
                        return View("Category");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Category/{id}")]
        public async Task<IActionResult> PostCategory(CategoryModel Form, int id)
        {
            if (await CheckAdmin() == true)
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
                    }

                    using (var response = await httpClient.PutAsync(api_url + "/Api/Admin/Category/" + id, httpContent))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Addresses")]
        public async Task<IActionResult> Addresses()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var addresses = new List<Addresses>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Addresses"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        addresses = JsonSerializer.Deserialize<List<Addresses>>(apiResponse);
                    }
                }

                ViewBag.Addresses = addresses;
                return View("Addresses");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Address/{id}")]
        public async Task<IActionResult> Address(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var address = new Addresses();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Address/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        address = JsonSerializer.Deserialize<Addresses>(apiResponse);
                    }
                }

                ViewBag.Address = address;
                return View("Address");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Comments")]
        public async Task<IActionResult> Comments()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var comments = new List<Comment>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Comments"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        comments = JsonSerializer.Deserialize<List<Comment>>(apiResponse);
                    }
                }

                ViewBag.Comments = comments;
                return View("Comments");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Comment/{id}")]
        public async Task<IActionResult> Comment(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var comment = new Comment();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Comment/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        comment = JsonSerializer.Deserialize<Comment>(apiResponse);
                    }
                }

                ViewBag.Comment = comment;
                return View("Comment");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Comment/{id}")]
        public async Task<IActionResult> SetComment(CommentModel Form, int id)
        {
            if (await CheckAdmin() == true)
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
                    }

                    using (var response = await httpClient.PutAsync(api_url + "/Api/Admin/Comment/" + id, httpContent))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpDelete("Admin/Comment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.DeleteAsync(api_url + "/Api/Admin/Comment/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Coupons")]
        public async Task<IActionResult> Coupons()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var coupons = new List<Coupon>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Coupons"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        coupons = JsonSerializer.Deserialize<List<Coupon>>(apiResponse);
                    }
                }

                ViewBag.Coupons = coupons;
                return View("Coupons");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Coupon/{id}")]
        public async Task<IActionResult> Coupon(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var coupon = new Coupon();
                var couponHistory = new List<CouponHistory>(); 

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Coupon/" + id))
                    {
                        var coupon_histor = await httpClient.GetAsync(api_url + "/Api/Admin/CouponHistory/" + id);
                        string coupon_historc = await coupon_histor.Content.ReadAsStringAsync();
                        couponHistory = JsonSerializer.Deserialize<List<CouponHistory>>(coupon_historc);

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        coupon = JsonSerializer.Deserialize<Coupon>(apiResponse);
                    }
                }

                ViewBag.Coupon = coupon;
                ViewBag.CouponHistory = couponHistory;
                return View("Coupon");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Coupon/{id}")]
        public async Task<IActionResult> SetCoupon(CouponsModel Form, int id)
        {
            if (await CheckAdmin() == true)
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
                    }

                    using (var response = await httpClient.PutAsync(api_url + "/Api/Admin/Coupon/" + id, httpContent))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Cart/{id}")]
        public async Task<IActionResult> CartDel(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.DeleteAsync(api_url + "/Api/Admin/Cart/" + id))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Sliders")]
        public async Task<IActionResult> Sliders()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var sliders = new List<Slider>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Sliders"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        sliders = JsonSerializer.Deserialize<List<Slider>>(apiResponse);
                    }
                }

                ViewBag.Sliders = sliders;
                return View("Sliders");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Slider/Add")]
        public async Task<IActionResult> AddSlider()
        {
            if (await CheckAdmin() == true)
            {
                return View("SliderAdd");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Slider/Add")]
        public async Task<IActionResult> AddSliderc(IFormFile BackgroundImg, SliderModel Form)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                if (BackgroundImg != null)
                {
                    var extent = Path.GetExtension(BackgroundImg.FileName);
                    var randomName = ($"{Guid.NewGuid()}{extent}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await BackgroundImg.CopyToAsync(stream);
                    }

                    Form.BackgroundImg = randomName;

                    var jsonModel = JsonSerializer.Serialize(Form);

                    var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        string token = Request.Cookies["token"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        }

                        using (var response = await httpClient.PostAsync(api_url + "/Api/Admin/Slider", httpContent))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                            return Json(new { status = api_data.Status, msg = api_data.Msg });
                        }
                    }
                }
                else
                {
                    Form.BackgroundImg = "default-slider.jpg";

                    var jsonModel = JsonSerializer.Serialize(Form);

                    var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        string token = Request.Cookies["token"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        }

                        using (var response = await httpClient.PostAsync(api_url + "/Api/Admin/Slider", httpContent))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                            return Json(new { status = api_data.Status, msg = api_data.Msg });
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Slider/{id}")]
        public async Task<IActionResult> GetSlider(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var slider = new Slider();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Slider/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        slider = JsonSerializer.Deserialize<Slider>(apiResponse);
                    }
                }

                ViewBag.Slider = slider;
                return View("Slider");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Slider/{id}")]
        public async Task<IActionResult> SetSlider(IFormFile BackgroundImg, SliderModel Form, int id = 0)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                if (BackgroundImg != null)
                {
                    var extent = Path.GetExtension(BackgroundImg.FileName);
                    var randomName = ($"{Guid.NewGuid()}{extent}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await BackgroundImg.CopyToAsync(stream);
                    }

                    Form.BackgroundImg = randomName;

                    var jsonModel = JsonSerializer.Serialize(Form);

                    var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        string token = Request.Cookies["token"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        }

                        using (var response = await httpClient.PutAsync(api_url + "/Api/Admin/Slider/" + id, httpContent))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                            return Json(new { status = api_data.Status, msg = api_data.Msg });
                        }
                    }
                }
                else
                {
                    var jsonModel = JsonSerializer.Serialize(Form);

                    var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        string token = Request.Cookies["token"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        }

                        using (var response = await httpClient.PutAsync(api_url + "/Api/Admin/Slider/" + id, httpContent))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                            return Json(new { status = api_data.Status, msg = api_data.Msg });
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpDelete("Admin/Slider/{id}")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.DeleteAsync(api_url + "/Api/Admin/Slider/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/PaymentNotifications")]
        public async Task<IActionResult> PaymentNotifications()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var paymentNotifications = new List<PaymentNotification>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/PaymentNotifications"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        paymentNotifications = JsonSerializer.Deserialize<List<PaymentNotification>>(apiResponse);
                    }
                }

                ViewBag.PaymentNotifications = paymentNotifications;
                return View("PaymentNotifications");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/PaymentNotification/{id}")]
        public async Task<IActionResult> GetPaymentNotification(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var paymentNotification = new PaymentNotification();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/PaymentNotification/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        paymentNotification = JsonSerializer.Deserialize<PaymentNotification>(apiResponse);
                    }
                }

                ViewBag.PaymentNotification = paymentNotification;
                return View("PaymentNotification");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/PaymentNotification/{id}")]
        public async Task<IActionResult> SetPaymentNotification(StatusModel Form, int id = 0)
        {
            if (await CheckAdmin() == true)
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
                    }

                    using (var response = await httpClient.PutAsync(api_url + "/Api/Admin/PaymentNotification/" + id, httpContent))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpDelete("Admin/PaymentNotification/{id}")]
        public async Task<IActionResult> DeletePaymentNotification(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.DeleteAsync(api_url + "/Api/Admin/PaymentNotification/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Banks")]
        public async Task<IActionResult> Banks()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var banks = new List<Bank>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Banks"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        banks = JsonSerializer.Deserialize<List<Bank>>(apiResponse);
                    }
                }

                ViewBag.Banks = banks;
                return View("Banks");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Bank/Add")]
        public async Task<IActionResult> AddBank()
        {
            if (await CheckAdmin() == true)
            {
                return View("BankAdd");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Bank/Add")]
        public async Task<IActionResult> AddBank(BankModel Form)
        {
            if (await CheckAdmin() == true)
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
                    }

                    using (var response = await httpClient.PostAsync(api_url + "/Api/Admin/Bank", httpContent))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Bank/{id}")]
        public async Task<IActionResult> GetBank(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var bank = new Bank();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Admin/Bank/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        bank = JsonSerializer.Deserialize<Bank>(apiResponse);
                    }
                }

                ViewBag.Bank = bank;
                return View("Bank");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Bank/{id}")]
        public async Task<IActionResult> SetBank(BankModel Form, int id = 0)
        {
            if (await CheckAdmin() == true)
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
                    }

                    using (var response = await httpClient.PutAsync(api_url + "/Api/Admin/Bank/" + id, httpContent))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpDelete("Admin/Bank/{id}")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.DeleteAsync(api_url + "/Api/Admin/Bank/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Settings")]
        public async Task<IActionResult> Settings()
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                return View("Settings");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Admin/Settings")]
        public async Task<IActionResult> SetSettings(IFormCollection formCollection)
        {
            if (await CheckAdmin() == true)
            {
                dynamic settings = await Tools.SettingAsync();
                ViewBag.Setting = settings;
                string site_url = await Tools.GetUrl(HttpContext);
                ViewBag.SiteUrl = site_url;
                string api_url = settings.api_url;

                var json_obj = new JObject();

                foreach (var post_data in formCollection)
                {
                    if(post_data.Key != "__RequestVerificationToken")
                    {
                        json_obj[post_data.Key] = post_data.Value[0];
                    }
                }

                // JSON string'ine dönüştürme
                var jsonModel = json_obj.ToString();

                var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.PutAsync(api_url + "/Api/Admin/Settings", httpContent))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }


        private async Task<bool> CheckAdmin()
        {
            if (User.Identity!.IsAuthenticated)
            {
                string Role = User.Claims.FirstOrDefault(x => x.Type == "Role")?.Value;

                if (Role == "Admin")
                {
                    return true;
                }

                return false;
            }
            
            return false;
        }
    }
}
