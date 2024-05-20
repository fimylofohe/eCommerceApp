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
    }
}
