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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using E_Ticaret_API.Models;
using static Azure.Core.HttpHeader;

namespace E_Ticaret_API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CartController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public CartController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    var coupon = await _context.CouponHistorys
                        .Where(p => p.UserId == userInfo.UserId && p.Status == true)
                        .OrderByDescending(p => p.CouponHistoryId)
                        .Select(p => new CouponHistoryDTO
                        {
                            CouponHistoryId = p.CouponHistoryId,
                            CouponId = p.CouponId,
                            OrderId = p.OrderId,
                            Coupon = new CouponDTO
                            {
                                CouponId = p.Coupon.CouponId,
                                Name = p.Coupon.Name,
                                Type = p.Coupon.Type,
                                DiscountAmount = p.Coupon.DiscountAmount,
                                CouponCode = p.Coupon.CouponCode,
                                ValidityDate = p.Coupon.ValidityDate,
                                SingleUse = p.Coupon.SingleUse,
                                Status = p.Coupon.Status
                            },
                            Status = p.Status
                        })
                        .FirstOrDefaultAsync();

                    var carts = await _context.Carts
                                                  .Include(p => p.User)
                                                  .Include(p => p.Product)
                                                  .Where(p => p.UserId == userInfo.UserId && p.OrderId == null)
                                                  .Select(p => new CartDTO
                                                  {
                                                      CartId = p.CartId,
                                                      UserId = p.UserId,
                                                      ProductId = p.ProductId,
                                                      Quantity = p.Quantity,
                                                      AddDate = p.AddDate,
                                                      User = new UserDTO
                                                      {
                                                          Name = p.User.Name,
                                                          Surname = p.User.Surname,
                                                          Email = p.User.Email,
                                                          PhoneNumber = p.User.PhoneNumber
                                                      },
                                                      Product = new ProductDTO
                                                      {
                                                          ProductId = p.Product.ProductId,
                                                          CategoryId = p.Product.CategoryId,
                                                          SKU = p.Product.SKU,
                                                          Name = p.Product.Name,
                                                          Description = p.Product.Description,
                                                          Price = p.Product.Price,
                                                          Stock = p.Product.Stock,
                                                          Status = p.Product.Status,
                                                          Category = new CategoryDTO
                                                          {
                                                              CategoryId = p.Product.Category.CategoryId,
                                                              Name = p.Product.Category.Name,
                                                              Status = p.Product.Category.Status
                                                          },
                                                          Pictures = p.Product.Pictures.Select(pic => new PictureDTO
                                                          {
                                                              PictureId = pic.PictureId,
                                                              Path = pic.Path
                                                          }).ToList(),
                                                          Comments = p.Product.Comments.Select(comment => new CommentDTO
                                                          {
                                                              CommentId = comment.CommentId,
                                                              Text = comment.Text,
                                                              PublishedDate = comment.PublishedDate,
                                                              Status = comment.Status,
                                                              ProductId = comment.ProductId,
                                                              UserId = comment.UserId
                                                          }).ToList()
                                                      }
                                                  })
                                                  .ToListAsync();
                    int total_cart = 0;
                    double total_price = 0;

                    for (int i = 0; i < carts.Count; i++)
                    {
                        var cartc = carts[i];

                        double total_cc = (double)(cartc.Quantity * cartc.Product.Price);

                        carts[i].Total = total_cc;

                        total_price += total_cc;
                        total_cart += cartc.Quantity;
                    }

                    double total_cprice = 0;

                    if(coupon != null)
                    {
                        total_cprice = total_price;

                        int coupon_type = Int32.Parse(coupon.Coupon.Type);

                        if (coupon_type == 1)
                        {
                            if(total_cprice >= coupon.Coupon.DiscountAmount)
                            {
                                total_cprice = (double)(total_cprice - coupon.Coupon.DiscountAmount);

                                total_cprice = total_price - total_cprice;
                            }
                            else
                            {
                                total_cprice = total_price;
                            }
                        }

                        if (coupon_type == 2)
                        {
                            total_cprice = (double)((total_cprice / 100) * coupon.Coupon.DiscountAmount);
                        }
                    }                    

                    return Json(new { count = total_cart, total = total_price, ctotal = total_cprice, carts, coupon });
                }
            }

            string guestToken = Request.Headers["GuestToken"];
            if (!string.IsNullOrEmpty(guestToken))
            {
                var carts = await _context.Carts
                                              .Include(p => p.User)
                                              .Include(p => p.Product)
                                              .Where(p => p.GuestToken == guestToken && p.OrderId == null)
                                              .Select(p => new CartDTO
                                              {
                                                  CartId = p.CartId,
                                                  GuestToken = guestToken,
                                                  UserId = p.UserId,
                                                  ProductId = p.ProductId,
                                                  Quantity = p.Quantity,
                                                  AddDate = p.AddDate,
                                                  User = new UserDTO
                                                  {
                                                      Name = p.User.Name,
                                                      Surname = p.User.Surname,
                                                      Email = p.User.Email,
                                                      PhoneNumber = p.User.PhoneNumber
                                                  },
                                                  Product = new ProductDTO
                                                  {
                                                      ProductId = p.Product.ProductId,
                                                      CategoryId = p.Product.CategoryId,
                                                      SKU = p.Product.SKU,
                                                      Name = p.Product.Name,
                                                      Description = p.Product.Description,
                                                      Price = p.Product.Price,
                                                      Stock = p.Product.Stock,
                                                      Status = p.Product.Status,
                                                      Category = new CategoryDTO
                                                      {
                                                          CategoryId = p.Product.Category.CategoryId,
                                                          Name = p.Product.Category.Name,
                                                          Status = p.Product.Category.Status
                                                      },
                                                      Pictures = p.Product.Pictures.Select(pic => new PictureDTO
                                                      {
                                                          PictureId = pic.PictureId,
                                                          Path = pic.Path
                                                      }).ToList(),
                                                      Comments = p.Product.Comments.Select(comment => new CommentDTO
                                                      {
                                                          CommentId = comment.CommentId,
                                                          Text = comment.Text,
                                                          PublishedDate = comment.PublishedDate,
                                                          Status = comment.Status,
                                                          ProductId = comment.ProductId,
                                                          UserId = comment.UserId
                                                      }).ToList()
                                                  }
                                              })
                                              .ToListAsync();
                int total_cart = 0;
                double total_price = 0;

                for (int i = 0; i < carts.Count; i++)
                {
                    var cartc = carts[i];

                    double total_cc = (double)(cartc.Quantity * cartc.Product.Price);

                    carts[i].Total = total_cc;

                    total_price += total_cc;
                    total_cart += cartc.Quantity;
                }

                return Json(new { count = total_cart, total = total_price, carts });
            }

            return Unauthorized();
        }


        //[Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> AddToCart(int id, CartModel Form)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    int userId = userInfo.UserId;
                    int productId = id;
                    int quantity = Form.Quantity;

                    var cartItem = new Cart
                    {
                        UserId = userId,
                        ProductId = productId,
                        Quantity = quantity,
                        OrderId = null,
                        AddDate = DateTime.Now
                    };

                    _context.Carts.Add(cartItem);
                    await _context.SaveChangesAsync();

                    return Json(new { status = true, msg = "Sepete Eklendi" });
                }
            }

            string guestToken = Request.Headers["GuestToken"];
            if (!string.IsNullOrEmpty(guestToken))
            {
                int productId = id;
                int quantity = Form.Quantity;

                var cartItem = new Cart
                {
                    GuestToken = guestToken,
                    ProductId = productId,
                    Quantity = quantity,
                    OrderId = null,
                    AddDate = DateTime.Now
                };

                _context.Carts.Add(cartItem);
                await _context.SaveChangesAsync();

                return Json(new { status = true, msg = "Sepete Eklendi" });
            }

            return Unauthorized();
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditToCart(int id, CartModel Form)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    int userId = userInfo.UserId;
                    int cartId = id;

                    var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId && c.UserId == userId);

                    if (cartItem == null)
                    {
                        return NotFound();
                    }

                    if (Form.Quantity <= 0)
                    {
                        return Ok(new { status = true, msg = "Geçersiz ürün adeti" });
                    }

                    cartItem.Quantity = Form.Quantity;

                    _context.Carts.Update(cartItem);
                    await _context.SaveChangesAsync();

                    return Ok(new { status = true, msg = "Sepet güncellendi" });
                }
            }

            string guestToken = Request.Headers["GuestToken"];
            if (!string.IsNullOrEmpty(guestToken))
            {
                int cartId = id;

                var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId && c.GuestToken == guestToken);

                if (cartItem == null)
                {
                    return NotFound();
                }

                cartItem.Quantity = Form.Quantity;

                _context.Carts.Update(cartItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Sepet güncellendi" });
            }

            return Unauthorized();
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    int userId = userInfo.UserId;
                    int cartId = id;

                    var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId && c.UserId == userId);

                    if (cartItem == null)
                    {
                        return NotFound();
                    }

                    _context.Carts.Remove(cartItem);
                    await _context.SaveChangesAsync();

                    return Ok(new { status = true, msg = "Sepetten silindi" });
                }
            }

            string guestToken = Request.Headers["GuestToken"];
            if (!string.IsNullOrEmpty(guestToken))
            {
                int cartId = id;

                var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId && c.GuestToken == guestToken);

                if (cartItem == null)
                {
                    return NotFound();
                }

                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Sepetten silindi" });
            }

            return Unauthorized();
        }

        //[Authorize]
        [HttpPost("Coupon")]
        public async Task<IActionResult> AddToCoupon(CouponModel Form)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    int userId = userInfo.UserId;
                    string code = Form.Code;

                    var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == code);
                    if (coupon != null)
                    {
                        var couponHistory = await _context.CouponHistorys.FirstOrDefaultAsync(ch => ch.UserId == userId && ch.CouponId == coupon.CouponId);
                        if (couponHistory == null)
                        {
                            var newCouponHistory = new CouponHistory
                            {
                                UserId = userId,
                                CouponId = coupon.CouponId,
                                Status = true
                            };

                            _context.CouponHistorys.Add(newCouponHistory);
                            await _context.SaveChangesAsync();

                            return Json(new { status = true, msg = "Kupon başarıyla eklendi" });
                        }
                        else
                        {
                            return Json(new { status = false, msg = "Bu kupon kullanılmış" });
                        }
                    }
                    else
                    {
                        return Json(new { status = false, msg = "Geçersiz kupon kodu" });
                    }
                }
            }

            return Unauthorized();
        }

        //[Authorize]
        [HttpDelete("Coupon/{id}")]
        public async Task<IActionResult> RemoveFromCoupon(int id)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    int userId = userInfo.UserId;
                    int couponHistoryId = id;

                    var couponItem = await _context.CouponHistorys.FirstOrDefaultAsync(c => c.CouponHistoryId == couponHistoryId && c.UserId == userId);

                    if (couponItem == null)
                    {
                        return NotFound();
                    }

                    _context.CouponHistorys.Remove(couponItem);
                    await _context.SaveChangesAsync();

                    return Ok(new { status = true, msg = "Kupon silindi" });
                }
            }

            return Unauthorized();
        }

        //[Authorize]
        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(CheckoutModel Form)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    int userId = userInfo.UserId;
                    int AddressId = Form.AddressId;
                    string? OrderNote = Form.OrderNote;
                    int OrderPay = Form.OrderPay;

                    var address = await _context.Addresses.FirstOrDefaultAsync(c => c.AddressId == AddressId);
                    if (address != null)
                    {
                        if(OrderPay > 0 && OrderPay < 4)
                        {
                            string OrderKey = Guid.NewGuid().ToString();
                            int CouponHistoryId = 0;

                            var coupon = await _context.CouponHistorys
                                .Where(p => p.UserId == userInfo.UserId && p.Status == true)
                                .OrderByDescending(p => p.CouponHistoryId)
                                .Select(p => new CouponHistoryDTO
                                {
                                    CouponHistoryId = p.CouponHistoryId,
                                    CouponId = p.CouponId,
                                    OrderId = p.OrderId,
                                    Coupon = new CouponDTO
                                    {
                                        CouponId = p.Coupon.CouponId,
                                        Name = p.Coupon.Name,
                                        Type = p.Coupon.Type,
                                        DiscountAmount = p.Coupon.DiscountAmount,
                                        CouponCode = p.Coupon.CouponCode,
                                        ValidityDate = p.Coupon.ValidityDate,
                                        SingleUse = p.Coupon.SingleUse,
                                        Status = p.Coupon.Status
                                    },
                                    Status = p.Status
                                })
                                .FirstOrDefaultAsync();

                            var carts = await _context.Carts
                                .Include(p => p.User)
                                .Include(p => p.Product)
                                .Where(p => p.UserId == userInfo.UserId && p.OrderId == null)
                                .Select(p => new CartDTO
                                {
                                    CartId = p.CartId,
                                    UserId = p.UserId,
                                    ProductId = p.ProductId,
                                    Quantity = p.Quantity,
                                    AddDate = p.AddDate,
                                    User = new UserDTO
                                    {
                                        Name = p.User.Name,
                                        Surname = p.User.Surname,
                                        Email = p.User.Email,
                                        PhoneNumber = p.User.PhoneNumber
                                    },
                                    Product = new ProductDTO
                                    {
                                        ProductId = p.Product.ProductId,
                                        CategoryId = p.Product.CategoryId,
                                        SKU = p.Product.SKU,
                                        Name = p.Product.Name,
                                        Description = p.Product.Description,
                                        Price = p.Product.Price,
                                        Stock = p.Product.Stock,
                                        Status = p.Product.Status,
                                        Category = new CategoryDTO
                                        {
                                            CategoryId = p.Product.Category.CategoryId,
                                            Name = p.Product.Category.Name,
                                            Status = p.Product.Category.Status
                                        },
                                        Pictures = p.Product.Pictures.Select(pic => new PictureDTO
                                        {
                                            PictureId = pic.PictureId,
                                            Path = pic.Path
                                        }).ToList(),
                                        Comments = p.Product.Comments.Select(comment => new CommentDTO
                                        {
                                            CommentId = comment.CommentId,
                                            Text = comment.Text,
                                            PublishedDate = comment.PublishedDate,
                                            Status = comment.Status,
                                            ProductId = comment.ProductId,
                                            UserId = comment.UserId
                                        }).ToList()
                                    }
                                })
                                .ToListAsync();
                            int total_cart = 0;
                            double total_price = 0;

                            for (int i = 0; i < carts.Count; i++)
                            {
                                var cartc = carts[i];

                                double total_cc = (double)(cartc.Quantity * cartc.Product.Price);

                                carts[i].Total = total_cc;

                                total_price += total_cc;
                                total_cart += cartc.Quantity;
                            }

                            double total_cprice = 0;

                            if (coupon != null)
                            {
                                total_cprice = total_price;

                                int coupon_type = Int32.Parse(coupon.Coupon.Type);

                                if (coupon_type == 1)
                                {
                                    if (total_cprice >= coupon.Coupon.DiscountAmount)
                                    {
                                        total_cprice = (double)(total_cprice - coupon.Coupon.DiscountAmount);

                                        total_cprice = total_price - total_cprice;
                                    }
                                    else
                                    {
                                        total_cprice = total_price;
                                    }
                                }

                                if (coupon_type == 2)
                                {
                                    total_cprice = (double)((total_cprice / 100) * coupon.Coupon.DiscountAmount);
                                }

                                CouponHistoryId = coupon.CouponHistoryId;
                            }
                            double Amount = (total_price - total_cprice);

                            var newOrder = new Order
                            {
                                AddressId = AddressId,
                                UserId = userId,
                                OrderKey = OrderKey,
                                Amount = Amount,
                                CouponAmount = total_cprice,
                                TotalAmount = total_price,
                                CouponHistoryId = coupon == null ? null : CouponHistoryId,
                                OrderNote = OrderNote,
                                OrderPay = OrderPay,
                                OrderDate = DateTime.Now,
                                OrderStatus = 0,
                                Status = true
                            };

                            _context.Orders.Add(newOrder);
                            await _context.SaveChangesAsync();

                            var orderId = newOrder.OrderId;

                            if (orderId != 0)
                            {
                                if (coupon != null)
                                {
                                    var CouponHH = await _context.CouponHistorys.FirstOrDefaultAsync(c => c.CouponHistoryId == coupon.CouponHistoryId && c.UserId == userId);

                                    if (CouponHH == null)
                                    {
                                        return NotFound();
                                    }

                                    CouponHH.OrderId = orderId;
                                    CouponHH.Status = false;

                                    _context.CouponHistorys.Update(CouponHH);
                                    await _context.SaveChangesAsync();
                                }

                                var cartItems = await _context.Carts.Where(c => c.OrderId == null && c.UserId == userId).ToListAsync();

                                if (cartItems == null || cartItems.Count == 0)
                                {
                                    return NotFound();
                                }

                                foreach (var cartItem in cartItems)
                                {
                                    var productItem = await _context.Products.FirstOrDefaultAsync(pc => pc.ProductId == cartItem.ProductId);

                                    int get_stok = (int)productItem.Stock;
                                    int dus_adet = cartItem.Quantity;
                                    int hesap_son = (get_stok - dus_adet);

                                    productItem.Stock = hesap_son;
                                    _context.Products.Update(productItem);

                                    cartItem.OrderId = orderId;
                                    _context.Carts.Update(cartItem);
                                }

                                await _context.SaveChangesAsync();

                                if(OrderPay == 1)
                                {
                                    return Json(new { status = true, msg = "Sipariş Başarıyla Oluşturuldu" + "<meta http-equiv='refresh' content='1; URL=/Pay/" + orderId + "'>" });
                                }
                                else
                                {
                                    return Json(new { status = true, msg = "Sipariş Başarıyla Oluşturuldu" + "<meta http-equiv='refresh' content='1; URL=/User/Orders/" + orderId + "'>" });
                                }

                                
                            }
                            else
                            {
                                return Json(new { status = false, msg = "Sipariş Oluşturulamadı" });
                            }
                        }
                        else
                        {
                            return Json(new { status = false, msg = "Geçersiz ödeme yöntemi" });
                        }
                    }
                    else
                    {
                        return Json(new { status = false, msg = "Geçersiz adres" });
                    }
                }
            }

            return Unauthorized();
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
