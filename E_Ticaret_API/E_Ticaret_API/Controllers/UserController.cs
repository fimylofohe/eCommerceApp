using System;
using System.Security.Cryptography;
using E_Ticaret_API.Models;
using E_Ticaret_API.Data;
using E_Ticaret_API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace E_Ticaret_API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController : Controller
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public UserController(DataContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                return Ok(new { token = GenerateJWT(user), user = new UserDTO { 
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Admin = user.Admin,
                } });
            }

            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUserMail = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUserMail != null)
            {
                return BadRequest("Bu e-posta adresiyle zaten bir kullanıcı kayıtlı!");
            }

            var existingUserPhone = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);
            if (existingUserPhone != null)
            {
                return BadRequest("Bu telefon numarasıyla zaten bir kullanıcı kayıtlı!");
            }

            var user = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Status = true
            };

            var userc = await _context.Users.AddAsync(user);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { status = true, msg = "Kayıt Başarılı" });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("Address")]
        public async Task<IActionResult> Address()
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    var carts = await _context.Addresses
                                                  .Where(p => p.UserId == userInfo.UserId)
                                                  .Select(p => new AddressDTO
                                                  {
                                                      AddressId = p.AddressId,
                                                      UserId = p.UserId,
                                                      AddressText = p.AddressText,
                                                      Province = p.Province,
                                                      District = p.District,
                                                      Country = p.Country,
                                                      PostalCode = p.PostalCode
                                                  })
                                                  .ToListAsync();

                    return Ok(carts);
                }
            }

            return Unauthorized();
        }

        [HttpGet("Address/{id}")]
        public async Task<IActionResult> Address(int id = 0)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    var carts = await _context.Addresses
                                                  .Where(p => p.AddressId == id && p.UserId == userInfo.UserId)
                                                  .Select(p => new AddressDTO
                                                  {
                                                      AddressId = p.AddressId,
                                                      UserId = p.UserId,
                                                      AddressText = p.AddressText,
                                                      Province = p.Province,
                                                      District = p.District,
                                                      Country = p.Country,
                                                      PostalCode = p.PostalCode
                                                  })
                                                  .FirstOrDefaultAsync();

                    return Ok(carts);
                }
            }

            return Unauthorized();
        }

        [HttpGet("Orders")]
        public async Task<IActionResult> Orders()
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    var carts = await _context.Orders
                        .Where(p => p.UserId == userInfo.UserId)
                        .Select(p => new OrderDTO
                        {
                            OrderId = p.OrderId,
                            AddressId = p.AddressId,
                            Address = new AddressDTO
                            {
                                AddressId = p.Address.AddressId,
                                AddressText = p.Address.AddressText,
                                Province = p.Address.Province,
                                District = p.Address.District,
                                Country = p.Address.Country,
                                PostalCode = p.Address.PostalCode,
                            },
                            UserId = p.UserId,
                            User = new UserDTO
                            {
                                Name = p.User.Name,
                                Surname = p.User.Surname,
                                Email = p.User.Email,
                                PhoneNumber = p.User.PhoneNumber,
                            },
                            OrderKey = p.OrderKey,
                            Amount = p.Amount,
                            CouponAmount = p.CouponAmount,
                            TotalAmount = p.TotalAmount,
                            CouponHistoryId = p.CouponHistoryId,
                            CouponHistory = p.CouponHistoryId == null ? null : new CouponHistoryDTO
                            {
                                CouponHistoryId = p.CouponHistory.CouponHistoryId,
                                CouponId = p.CouponHistory.CouponId,
                                Coupon = new CouponDTO
                                {
                                    CouponId = p.CouponHistory.Coupon.CouponId,
                                    Name = p.CouponHistory.Coupon.Name,
                                    Type = p.CouponHistory.Coupon.Type,
                                    DiscountAmount = p.CouponHistory.Coupon.DiscountAmount,
                                    CouponCode = p.CouponHistory.Coupon.CouponCode,
                                    ValidityDate = p.CouponHistory.Coupon.ValidityDate,
                                },
                            },
                            OrderNote = p.OrderNote,
                            OrderPay = p.OrderPay,
                            OrderDate = p.OrderDate,
                            OrderStatus = p.OrderStatus,
                            Status = p.Status,
                            Carts = p.Carts.Select(cart => new CartDTO
                            {
                                CartId = cart.CartId,
                                ProductId = cart.ProductId,
                                Product = new ProductDTO
                                {
                                    ProductId = cart.Product.ProductId,
                                    CategoryId = cart.Product.CategoryId,
                                    SKU = cart.Product.SKU,
                                    Name = cart.Product.Name,
                                    Description = cart.Product.Description,
                                    Price = cart.Product.Price,
                                    Stock = cart.Product.Stock,
                                    Status = cart.Product.Status,
                                    Category = new CategoryDTO
                                    {
                                        CategoryId = cart.Product.Category.CategoryId,
                                        Name = cart.Product.Category.Name,
                                        Status = cart.Product.Category.Status
                                    },
                                    Pictures = cart.Product.Pictures.Select(pic => new PictureDTO
                                    {
                                        PictureId = pic.PictureId,
                                        Path = pic.Path
                                    }).ToList(),
                                    Comments = cart.Product.Comments.Select(comment => new CommentDTO
                                    {
                                        CommentId = comment.CommentId,
                                        Text = comment.Text,
                                        PublishedDate = comment.PublishedDate,
                                        Status = comment.Status,
                                        ProductId = comment.ProductId,
                                        UserId = comment.UserId
                                    }).ToList()
                                },
                            }).ToList(),
                            PaymentNotifications = p.PaymentNotifications.Select(payN => new PaymentNotificationDTO
                            {
                                PaymentId = payN.PaymentId,
                                OrderId = payN.OrderId,
                                BankId = payN.BankId,
                                Bank = new BankDTO
                                {
                                    Name = payN.Bank.Name,
                                    AccountName = payN.Bank.AccountName,
                                    AccountNo = payN.Bank.AccountNo,
                                    Branch = payN.Bank.Branch,
                                    IBAN = payN.Bank.IBAN,

                                },
                                NameSurname = payN.NameSurname,
                                TotalAmount = payN.TotalAmount,
                                Receipt = payN.Receipt,
                                PayNote = payN.PayNote,
                                PayDate = payN.PayDate,
                                Status = payN.Status,
                            }).ToList(),
                        })
                        .ToListAsync();

                    return Ok(carts);
                }
            }

            return Unauthorized();
        }

        [HttpGet("Orders/{id}")]
        public async Task<IActionResult> Orders(int id = 0)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    var carts = await _context.Orders
                        .Where(p => p.UserId == userInfo.UserId && p.OrderId == id)
                        .Select(p => new OrderDTO
                        {
                            OrderId = p.OrderId,
                            AddressId = p.AddressId,
                            Address = new AddressDTO
                            {
                                AddressId = p.Address.AddressId,
                                AddressText = p.Address.AddressText,
                                Province = p.Address.Province,
                                District = p.Address.District,
                                Country = p.Address.Country,
                                PostalCode = p.Address.PostalCode,
                            },
                            UserId = p.UserId,
                            User = new UserDTO
                            {
                                Name = p.User.Name,
                                Surname = p.User.Surname,
                                Email = p.User.Email,
                                PhoneNumber = p.User.PhoneNumber,
                            },
                            OrderKey = p.OrderKey,
                            Amount = p.Amount,
                            CouponAmount = p.CouponAmount,
                            TotalAmount = p.TotalAmount,
                            CouponHistoryId = p.CouponHistoryId,
                            CouponHistory = p.CouponHistoryId == null ? null : new CouponHistoryDTO
                            {
                                CouponHistoryId = p.CouponHistory.CouponHistoryId,
                                CouponId = p.CouponHistory.CouponId,
                                Coupon = new CouponDTO
                                {
                                    CouponId = p.CouponHistory.Coupon.CouponId,
                                    Name = p.CouponHistory.Coupon.Name,
                                    Type = p.CouponHistory.Coupon.Type,
                                    DiscountAmount = p.CouponHistory.Coupon.DiscountAmount,
                                    CouponCode = p.CouponHistory.Coupon.CouponCode,
                                    ValidityDate = p.CouponHistory.Coupon.ValidityDate,
                                },
                            },
                            OrderNote = p.OrderNote,
                            OrderPay = p.OrderPay,
                            OrderDate = p.OrderDate,
                            OrderStatus = p.OrderStatus,
                            Status = p.Status,
                            Carts = p.Carts.Select(cart => new CartDTO
                            {
                                CartId = cart.CartId,
                                ProductId = cart.ProductId,
                                Product = new ProductDTO
                                {
                                    ProductId = cart.Product.ProductId,
                                    CategoryId = cart.Product.CategoryId,
                                    SKU = cart.Product.SKU,
                                    Name = cart.Product.Name,
                                    Description = cart.Product.Description,
                                    Price = cart.Product.Price,
                                    Stock = cart.Product.Stock,
                                    Status = cart.Product.Status,
                                    Category = new CategoryDTO
                                    {
                                        CategoryId = cart.Product.Category.CategoryId,
                                        Name = cart.Product.Category.Name,
                                        Status = cart.Product.Category.Status
                                    },
                                    Pictures = cart.Product.Pictures.Select(pic => new PictureDTO
                                    {
                                        PictureId = pic.PictureId,
                                        Path = pic.Path
                                    }).ToList(),
                                    Comments = cart.Product.Comments.Select(comment => new CommentDTO
                                    {
                                        CommentId = comment.CommentId,
                                        Text = comment.Text,
                                        PublishedDate = comment.PublishedDate,
                                        Status = comment.Status,
                                        ProductId = comment.ProductId,
                                        UserId = comment.UserId
                                    }).ToList()
                                },
                                Quantity = cart.Quantity,
                                AddDate = cart.AddDate,
                            }).ToList(),
                            PaymentNotifications = p.PaymentNotifications.Select(payN => new PaymentNotificationDTO
                            {
                                PaymentId = payN.PaymentId,
                                OrderId = payN.OrderId,
                                BankId = payN.BankId,
                                Bank = new BankDTO
                                {
                                    Name = payN.Bank.Name,
                                    AccountName = payN.Bank.AccountName,
                                    AccountNo = payN.Bank.AccountNo,
                                    Branch = payN.Bank.Branch,
                                    IBAN = payN.Bank.IBAN,

                                },
                                NameSurname = payN.NameSurname,
                                TotalAmount = payN.TotalAmount,
                                Receipt = payN.Receipt,
                                PayNote = payN.PayNote,
                                PayDate = payN.PayDate,
                                Status = payN.Status,
                            }).ToList(),
                        })
                        .FirstOrDefaultAsync();

                    return Ok(carts);
                }
            }

            return Unauthorized();
        }

        [HttpPost("Orders/{id}")]
        public async Task<IActionResult> OrdersPay(PayNotModel Form, int id = 0)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    var payNot = new PaymentNotification
                    {
                        OrderId = Form.OrderId,
                        BankId = Form.BankId,
                        NameSurname = Form.NameSurname,
                        TotalAmount = Form.TotalAmount,
                        Receipt = Form.Receipt,
                        PayNote = Form.PayNote,
                        PayDate = DateTime.Now,
                    };

                    var payNotc = await _context.PaymentNotifications.AddAsync(payNot);
                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new { status = true, msg = "Kayıt Başarılı" });
                    }
                    catch (DbUpdateException ex)
                    {
                        return BadRequest(new { status = true, msg = ex.InnerException.Message });
                    }
                }
            }

            return Unauthorized();
        }

        [HttpPut("Orders/{id}")]
        public async Task<IActionResult> OPay(int id = 0)
        {
            var orderItem = await _context.Orders.FirstOrDefaultAsync(c => c.OrderId == id);

            if (orderItem == null)
            {
                return NotFound();
            }

            orderItem.OrderStatus = 1;

            _context.Orders.Update(orderItem);
            await _context.SaveChangesAsync();

            return Ok(new { status = true, msg = "Ödeme Onaylandı" });
        }

        private string GenerateJWT(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? "");

            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Name", user.Name ?? ""),
                new Claim("Surname", user.Surname ?? ""),
                new Claim("NameSurname", user.NameSurname ?? ""),
                new Claim("Email", user.Email ?? ""),
                new Claim("PhoneNumber", user.PhoneNumber ?? ""),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "si4.net"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private User ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? "");

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = "si4.net",
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var UserId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
            var Name = jwtToken.Claims.FirstOrDefault(x => x.Type == "Name")?.Value;
            var Surname = jwtToken.Claims.FirstOrDefault(x => x.Type == "Surname")?.Value;
            var NameSurname = jwtToken.Claims.FirstOrDefault(x => x.Type == "NameSurname")?.Value;
            var Email = jwtToken.Claims.FirstOrDefault(x => x.Type == "Email")?.Value;
            var PhoneNumber = jwtToken.Claims.FirstOrDefault(x => x.Type == "PhoneNumber")?.Value;

            return new User { UserId = UserId, Name = Name, Surname = Surname, Email = Email, PhoneNumber = PhoneNumber };
        }
    }
}
