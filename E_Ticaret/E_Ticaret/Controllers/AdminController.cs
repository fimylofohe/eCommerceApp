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
                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Datas"))
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
                var orders = new List<Orders>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Orders/10"))
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
                var orders = new List<Orders>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Orders/" + status))
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
                var orders = new Orders();
                var banks = new List<Bank>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Order/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        orders = JsonSerializer.Deserialize<Orders>(apiResponse);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Data/Banks/"))
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

        [HttpGet("Admin/Users")]
        public async Task<IActionResult> Users()
        {
            if (await CheckAdmin() == true)
            {
                var users = new List<User>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Users"))
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
                var user = new User();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/User/" + id))
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
                var jsonModel = JsonSerializer.Serialize(Form);

                var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.PutAsync("https://localhost:7279/Api/Admin/User/" + id, httpContent))
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
                var products = new List<DTO.Product>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Products"))
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
                var categories = new List<Category>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Categories"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        categories = JsonSerializer.Deserialize<List<Category>>(apiResponse);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Product/" + id))
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
                var jsonModel = JsonSerializer.Serialize(Form);

                var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.PutAsync("https://localhost:7279/Api/Admin/Product/" + id, httpContent))
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
                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Images/" + id))
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

                    using (var response = await httpClient.PostAsync("https://localhost:7279/Api/Admin/Image/" + id, httpContent))
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
                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.DeleteAsync("https://localhost:7279/Api/Admin/Image/" + id))
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
                var categories = new List<Category>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Categories"))
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
                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Category/" + id))
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
                var jsonModel = JsonSerializer.Serialize(Form);

                var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.PutAsync("https://localhost:7279/Api/Admin/Category/" + id, httpContent))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var api_data = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                        return Json(new { status = api_data.Status, msg = api_data.Msg });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/Comments")]
        public async Task<IActionResult> Comments()
        {
            if (await CheckAdmin() == true)
            {
                var comments = new List<Comment>();

                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Admin/Comments"))
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

        [HttpPost("Admin/Cart/{id}")]
        public async Task<IActionResult> CartDel(int id)
        {
            if (await CheckAdmin() == true)
            {
                using (var httpClient = new HttpClient())
                {
                    string token = Request.Cookies["token"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.DeleteAsync("https://localhost:7279/Api/Admin/Cart/" + id))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
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
