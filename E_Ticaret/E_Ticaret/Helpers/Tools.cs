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
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace E_Ticaret.Helpers
{
    public static class Tools
    {
        public static void GenerateGuestToken(HttpContext httpContext)
        {
            if (!httpContext.Request.Cookies.ContainsKey("GuestToken"))
            {
                string token = Guid.NewGuid().ToString();
                httpContext.Response.Cookies.Append("GuestToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.Now.AddDays(1)
                });
            }
        }

        public static void CheckToken(HttpContext httpContext)
        {
            if (!httpContext.Request.Cookies.ContainsKey("token"))
            {
                foreach (var cookie in httpContext.Request.Cookies)
                {
                    if (cookie.Key != "GuestToken")
                    {
                        httpContext.Response.Cookies.Delete(cookie.Key);
                    }
                }
            }
        }

        public static async Task<dynamic> SettingAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7279/Api/Data/Setting"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var settings = JsonSerializer.Deserialize<Dictionary<string, string>>(apiResponse);

                    var expandoObject = new ExpandoObject() as IDictionary<string, object>;

                    foreach (var setting in settings)
                    {
                        expandoObject[setting.Key] = setting.Value;
                    }

                    return expandoObject;
                }
            }
        }

        public static async Task<string> GetUrl(HttpContext httpContext)
        {
            var request = httpContext.Request;
            var protocol = request.IsHttps ? "https://" : "http://";
            var siteUrl = $"{protocol}{request.Host}";

            return siteUrl;
        }
    }
}
