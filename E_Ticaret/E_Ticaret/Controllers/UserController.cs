﻿using E_Ticaret.Models;
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
using Newtonsoft.Json.Linq;

namespace E_Ticaret.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
            {
                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/User/Statistics"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        JObject jsonResponse = JObject.Parse(apiResponse);

                        ViewBag.Statistics = jsonResponse;
                    }
                }

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet("User/Settings")]
        public async Task<IActionResult> UserSettings()
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
            {
                var user = new User();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/User"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonSerializer.Deserialize<User>(apiResponse);
                    }
                }

                ViewBag.User = user;

                return View("Settings");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost("User/Settings")]
        public async Task<IActionResult> UserSettingsPost(UserModel Form)
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
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

                    using (var response = await httpClient.PostAsync(api_url + "/Api/User", httpContent))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost("User/Password")]
        public async Task<IActionResult> UserPasswordPost(PasswordModel Form)
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
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

                    using (var response = await httpClient.PostAsync(api_url + "/Api/User/Password", httpContent))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        return Ok(apiResponse);
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet("User/Address")]
        public async Task<IActionResult> Address()
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
            {
                var address = new List<Addresses>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/User/Address"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        address = JsonSerializer.Deserialize<List<Addresses>>(apiResponse);
                    }
                }

                ViewBag.Address = address;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet("User/Address/Add")]
        public async Task<IActionResult> AddressAdd()
        {
            Tools.CheckToken(HttpContext);

            if (User.Identity!.IsAuthenticated)
            {
                return View("Address_Add");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost("User/Address/Add")]
        public async Task<IActionResult> AddressAddP(AddressModel Form)
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
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

                    using (var response = await httpClient.PostAsync(api_url + "/Api/User/Address", httpContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            return Json(new { status = true, msg = "Adres Eklendi" + "<meta http-equiv='refresh' content='2;URL=/User/Address'>" });
                        }

                        return BadRequest(new { status = true, msg = "Api isteği sırasında hata oluştu." });
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet("User/Address/{id}")]
        public async Task<IActionResult> Address(int id = 0)
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
            {
                var address = new Addresses();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/User/Address/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        address = JsonSerializer.Deserialize<Addresses>(apiResponse);
                    }
                }

                ViewBag.Address = address;

                return View("Address_Edit");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost("User/Address/{id}")]
        public async Task<IActionResult> AddressEdit(AddressModel Form, int id = 0)
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
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

                    using (var response = await httpClient.PutAsync(api_url + "/Api/User/Address/" + id, httpContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            return Json(new { status = true, msg = "Adres Güncellendi" + "<meta http-equiv='refresh' content='2;URL=/User/Address'>" });
                        }

                        return BadRequest(new { status = true, msg = "Api isteği sırasında hata oluştu." });
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpDelete("User/Address/{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
            {
                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.DeleteAsync(api_url + "/Api/User/Address/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            return Json(new { status = true, msg = "Adres Silindi " + "<meta http-equiv='refresh' content='2;URL=/User/Address'>" });
                        }

                        return BadRequest(new { status = true, msg = "Api isteği sırasında hata oluştu." });
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet("User/Comments")]
        public async Task<IActionResult> Comments()
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
            {
                var comments = new List<Comment>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/User/Comments"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        comments = JsonSerializer.Deserialize<List<Comment>>(apiResponse);
                    }
                }

                ViewBag.Comments = comments;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet("User/Orders")]
        public async Task<IActionResult> Orders()
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
            {
                var orders = new List<Orders>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/User/Orders"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        orders = JsonSerializer.Deserialize<List<Orders>>(apiResponse);
                    }
                }

                ViewBag.Orders = orders;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet("User/Orders/{id}")]
        public async Task<IActionResult> Orders(int id = 0)
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (User.Identity!.IsAuthenticated)
            {
                var order = new Orders();
                var banks = new List<Bank>();
                var payNot = new List<PaymentNotification>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/User/Orders/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        order = JsonSerializer.Deserialize<Orders>(apiResponse);
                    }

                    using (var response = await httpClient.GetAsync(api_url + "/Api/Data/Banks/"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        banks = JsonSerializer.Deserialize<List<Bank>>(apiResponse);
                    }
                }

                ViewBag.Order = order;
                ViewBag.Banks = banks;

                return View("Order_View");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost("User/Orders/{id}")]
        public async Task<IActionResult> OrdersPost(IFormFile Receipt, PayNotModel Form, int id = 0)
        {
            Tools.CheckToken(HttpContext);

            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            if (Receipt != null)
            {
                var extent = Path.GetExtension(Receipt.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Receipt.CopyToAsync(stream);
                }

                Form.Receipt = randomName;

                var jsonModel = JsonSerializer.Serialize(Form);

                var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.PostAsync(api_url + "/Api/User/Orders/" + id, httpContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            return Json(new { status = true, msg = "Ödeme Talebiniz Alındı" + "<meta http-equiv='refresh' content='2'>" });
                        }

                        return BadRequest(new { status = true, msg = "Api isteği sırasında hata oluştu." });
                    }
                }
            }
            else
            {
                return Json(new { status = false, msg = "Dosya yüklenirken hata oluştu, lütfen daha sonra tekrar deneyiniz." });
            }
        }

        [HttpPost("User/Comment/{order_id}/{product_id}")]
        public async Task<IActionResult> OrdersComment(CommentModel Form, int order_id = 0, int product_id = 0)
        {
            Tools.CheckToken(HttpContext);

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

                using (var response = await httpClient.PostAsync(api_url + "/Api/User/Comment/" + order_id + "/" + product_id, httpContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(new { status = true, msg = "Yorumunuz Gönderildi" + "<meta http-equiv='refresh' content='2'>" });
                    }

                    return BadRequest(new { status = true, msg = "Api isteği sırasında hata oluştu." });
                }
            }
        }
    }
}
