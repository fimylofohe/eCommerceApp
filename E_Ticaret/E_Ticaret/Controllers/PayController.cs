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
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json.Nodes;

namespace E_Ticaret.Controllers
{
    public class PayController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PayController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("Pay/{id}")]
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
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

                var order = new Orders();
                var banks = new List<Bank>();
                var payNot = new List<PaymentNotification>();

                using (var httpClient = new System.Net.Http.HttpClient())
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
                }

                Options options = new Options();
                options.ApiKey = settings.iyzico_apikey;
                options.SecretKey = settings.iyzico_secretkey;
                if(settings.iyzico_test == "true")
                {
                    options.BaseUrl = "https://sandbox-api.iyzipay.com";
                }
                else
                {
                    options.BaseUrl = "https://api.iyzipay.com";
                }

                CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
                request.Locale = Locale.TR.ToString();
                request.ConversationId = Guid.NewGuid().ToString();
                request.Price = order.TotalAmount.ToString().Replace(",", ".");
                request.PaidPrice = order.Amount.ToString().Replace(",", ".");
                request.Currency = Currency.TRY.ToString();
                request.BasketId = order.OrderKey;
                request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
                request.CallbackUrl = site_url + "/Pay/" + order.OrderId;

                DateTime date = DateTime.ParseExact(DateTime.Now.ToString(), "dd.MM.yyyy HH:mm:ss", null);
                string outputDate = date.ToString("yyyy-MM-dd HH:mm:ss");

                Buyer buyer = new Buyer();
                buyer.Id = order.UserId.ToString();
                buyer.Name = order.User.Name;
                buyer.Surname = order.User.Surname;
                buyer.GsmNumber = order.User.PhoneNumber;
                buyer.Email = order.User.Email;
                buyer.IdentityNumber = Guid.NewGuid().ToString();
                buyer.LastLoginDate = outputDate;
                buyer.RegistrationDate = outputDate;
                buyer.RegistrationAddress = order.Address.AddressText;
                buyer.Ip = remoteIpAddress;
                buyer.City = order.Address.Province;
                buyer.Country = order.Address.Country;
                buyer.ZipCode = order.Address.PostalCode;
                request.Buyer = buyer;

                Address shippingAddress = new Address();
                shippingAddress.ContactName = order.User.NameSurname;
                shippingAddress.City = order.Address.Province;
                shippingAddress.Country = order.Address.Country;
                shippingAddress.Description = order.Address.AddressText;
                shippingAddress.ZipCode = order.Address.PostalCode;
                request.ShippingAddress = shippingAddress;

                Address billingAddress = new Address();
                billingAddress.ContactName = order.User.NameSurname;
                billingAddress.City = order.Address.Province;
                billingAddress.Country = order.Address.Country;
                billingAddress.Description = order.Address.AddressText;
                billingAddress.ZipCode = order.Address.PostalCode;
                request.BillingAddress = billingAddress;

                List<BasketItem> basketItems = new List<BasketItem>();
                foreach (var item in order.Carts)
                {
                    BasketItem basketItem = new BasketItem();
                    basketItem.Id = item.Product.ProductId.ToString();
                    basketItem.Name = item.Product.Name;
                    basketItem.Category1 = item.Product.Category.Name;
                    basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                    basketItem.Price = item.Product.Price.ToString().Replace(",", ".");
                    basketItems.Add(basketItem);
                }
                request.BasketItems = basketItems;

                CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);
                ViewBag.Iyzico = checkoutFormInitialize.CheckoutFormContent;

                ViewBag.Order = order;

                return View("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost("Pay/{id}")]
        public async Task<IActionResult> OrdersPost(RetrieveCheckoutFormRequest model, int id = 0)
        {
            dynamic settings = await Tools.SettingAsync();
            ViewBag.Setting = settings;
            string site_url = await Tools.GetUrl(HttpContext);
            ViewBag.SiteUrl = site_url;
            string api_url = settings.api_url;

            string data = "";
            Options options = new Options();
            options.ApiKey = settings.iyzico_apikey;
            options.SecretKey = settings.iyzico_secretkey;
            if (settings.iyzico_test == "true")
            {
                options.BaseUrl = "https://sandbox-api.iyzipay.com";
            }
            else
            {
                options.BaseUrl = "https://api.iyzipay.com";
            }
            data = model.Token;
            RetrieveCheckoutFormRequest request = new RetrieveCheckoutFormRequest();
            request.Token = data;
            CheckoutForm checkoutForm = CheckoutForm.Retrieve(request, options);
            if (checkoutForm.PaymentStatus == "SUCCESS")
            {
                if (id == null || id == 0)
                {
                    return BadRequest();
                }
                
                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var jsonModel = JsonSerializer.Serialize(model);

                    var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync(api_url + "/Api/User/Orders/" + id, httpContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var dataS = JsonSerializer.Deserialize<ApiStatus>(apiResponse);

                            if(dataS.Status == true)
                            {
                                return RedirectToAction("Success");
                            }
                            else
                            {
                                return RedirectToAction("Error");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet("Pay/Success")]
        public ActionResult Success()
        {
            return View("Success");
        }

        [HttpGet("Pay/Error")]
        public ActionResult Error()
        {
            return View("Error");
        }
    }
}
