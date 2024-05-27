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
using static Azure.Core.HttpHeader;
using System.Net;

namespace E_Ticaret_API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AdminController : Controller
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public AdminController(DataContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        [HttpGet("Datas")]
        public async Task<IActionResult> GetDatas()
        {
            if (await CheckAdmin() == true)
            {
                int products_count = await _context.Orders.CountAsync();
                int categories_count = await _context.Categories.CountAsync();
                int users_count = await _context.Users.CountAsync();
                int comments_count = await _context.Comments.CountAsync();
                int coupons_count = await _context.Coupons.CountAsync();

                DateTime today = DateTime.Today;
                DateTime startOfDay = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
                DateTime endOfDay = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);
                double dailyEarning = await CalculateEarning(startOfDay, endOfDay);
                DateTime startOfWeek = today.AddDays(-((int)today.DayOfWeek));
                DateTime endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1);
                double weeklyEarning = await CalculateEarning(startOfWeek, endOfWeek);
                DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1).AddDays(1).AddSeconds(-1);
                double monthlyEarning = await CalculateEarning(startOfMonth, endOfMonth);
                double totalEarning = await CalculateEarning(DateTime.MinValue, DateTime.MaxValue);

                var monthlyEarningsc = new List<double>();

                for (int i = 1; i <= 12; i++)
                {
                    DateTime startOfMonthc = new DateTime(DateTime.Today.Year, i, 1);
                    DateTime endOfMonthc = startOfMonthc.AddMonths(1).AddDays(-1).AddDays(1).AddSeconds(-1);
                    double monthlyEarningc = await CalculateEarning(startOfMonthc, endOfMonthc);
                    monthlyEarningsc.Add(monthlyEarningc);
                }

                var orderEarningsc = new List<double>();

                for (int i = 1; i <= 12; i++)
                {
                    DateTime startOfOrderc = new DateTime(DateTime.Today.Year, i, 1);
                    DateTime endOfOrderc = startOfOrderc.AddMonths(1).AddDays(-1).AddDays(1).AddSeconds(-1);
                    double orderEarningc = await CalculateOrder(startOfOrderc, endOfOrderc);
                    orderEarningsc.Add(orderEarningc);
                }

                return Json(new
                {
                    OrderCounts = products_count,
                    CategoriesCounts = categories_count,
                    UsersCounts = users_count,
                    CommentsCounts = comments_count,
                    CouponsCounts = coupons_count,
                    DailyEarning = dailyEarning,
                    WeeklyEarning = weeklyEarning,
                    MonthlyEarning = monthlyEarning,
                    TotalEarning = totalEarning,
                    ChartEarningsMonthly = monthlyEarningsc,
                    OrderMonthlyCounts = orderEarningsc
                });
            }

            return Unauthorized();
        }

        private async Task<double> CalculateEarning(DateTime startDate, DateTime endDate)
        {
            var orders = await _context.Orders
                            .Where(p => p.OrderDate >= startDate && p.OrderDate <= endDate)
                            .OrderBy(p => p.OrderId)
                            .Select(p => new OrderDTO
                            {
                                Amount = p.Amount
                            })
                            .ToListAsync();

            return (double)orders.Sum(o => o.Amount);
        }

        private async Task<double> CalculateOrder(DateTime startDate, DateTime endDate)
        {
            var orders = await _context.Orders
                            .Where(p => p.OrderDate >= startDate && p.OrderDate <= endDate)
                            .OrderBy(p => p.OrderId)
                            .CountAsync();

            return (int)orders;
        }

        [HttpGet("Orders/{status}")]
        public async Task<IActionResult> GetOrders(int status = 10)
        {
            if (await CheckAdmin() == true)
            {
                if(status == 10)
                {
                    var carts = await _context.Orders
                            .OrderBy(p => p.OrderId)
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
                                    Quantity = cart.Quantity,
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
                else
                {
                    var carts = await _context.Orders
                            .OrderBy(p => p.OrderId)
                            .Where(p => p.OrderStatus == status)
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
                                    Quantity = cart.Quantity,
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

        [HttpGet("Order/{id}")]
        public async Task<IActionResult> GetOrder(int id = 0)
        {
            if (await CheckAdmin() == true)
            {
                var carts = await _context.Orders
                            .OrderBy(p => p.OrderId)
                            .Where(p => p.OrderId == id)
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
                                    Quantity = cart.Quantity,
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
                            .FirstOrDefaultAsync();

                return Ok(carts);
            }

            return Unauthorized();
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            if (await CheckAdmin() == true)
            {
                var users = await _context.Users
                        .OrderBy(p => p.UserId)
                        .Select(p => new UserDTO
                        {
                            UserId = p.UserId,
                            Name = p.Name,
                            Surname = p.Surname,
                            Email = p.Email,
                            PhoneNumber = p.PhoneNumber,
                            Admin = p.Admin,
                            Status = p.Status,
                            Addresses = p.Addresses.Select(address => new AddressDTO
                            {
                                AddressId = address.AddressId,
                            }).ToList(),
                            Orders = p.Orders.Select(order => new OrderDTO
                            {
                                OrderId = order.OrderId,
                            }).ToList(),
                            Comments = p.Comments.Select(comment => new CommentDTO
                            {
                                CommentId = comment.CommentId,
                            }).ToList(),
                            Carts = p.Carts.Select(cart => new CartDTO
                            {
                                CartId = cart.CartId,
                            }).ToList()
                        })
                        .ToListAsync();

                return Ok(users);
            }

            return Unauthorized();
        }

        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (await CheckAdmin() == true)
            {
                var users = await _context.Users
                        .OrderBy(p => p.UserId)
                        .Where(p => p.UserId == id)
                        .Select(p => new UserDTO
                        {
                            UserId = p.UserId,
                            Name = p.Name,
                            Surname = p.Surname,
                            Email = p.Email,
                            PhoneNumber = p.PhoneNumber,
                            Admin = p.Admin,
                            Status = p.Status,
                            Addresses = p.Addresses.Where(address => address.UserId == p.UserId).Select(address => new AddressDTO
                            {
                                AddressId = address.AddressId,
                                AddressText = address.AddressText,
                                Province = address.Province,
                                District = address.District,
                                Country = address.Country,
                                PostalCode = address.PostalCode,
                            }).ToList(),
                            Orders = p.Orders.Where(order => order.UserId == p.UserId).Select(order => new OrderDTO
                            {
                                OrderId = order.OrderId,
                                AddressId = order.AddressId,
                                Address = new AddressDTO
                                {
                                    AddressId = order.Address.AddressId,
                                    AddressText = order.Address.AddressText,
                                    Province = order.Address.Province,
                                    District = order.Address.District,
                                    Country = order.Address.Country,
                                    PostalCode = order.Address.PostalCode,
                                },
                                UserId = order.UserId,
                                User = new UserDTO
                                {
                                    Name = order.User.Name,
                                    Surname = order.User.Surname,
                                    Email = order.User.Email,
                                    PhoneNumber = order.User.PhoneNumber,
                                },
                                OrderKey = order.OrderKey,
                                Amount = order.Amount,
                                CouponAmount = order.CouponAmount,
                                TotalAmount = order.TotalAmount,
                                CouponHistoryId = order.CouponHistoryId,
                                CouponHistory = order.CouponHistoryId == null ? null : new CouponHistoryDTO
                                {
                                    CouponHistoryId = order.CouponHistory.CouponHistoryId,
                                    CouponId = order.CouponHistory.CouponId,
                                    Coupon = new CouponDTO
                                    {
                                        CouponId = order.CouponHistory.Coupon.CouponId,
                                        Name = order.CouponHistory.Coupon.Name,
                                        Type = order.CouponHistory.Coupon.Type,
                                        DiscountAmount = order.CouponHistory.Coupon.DiscountAmount,
                                        CouponCode = order.CouponHistory.Coupon.CouponCode,
                                        ValidityDate = order.CouponHistory.Coupon.ValidityDate,
                                    },
                                },
                                OrderNote = order.OrderNote,
                                OrderPay = order.OrderPay,
                                OrderDate = order.OrderDate,
                                OrderStatus = order.OrderStatus,
                                Status = order.Status,
                                Carts = p.Carts.Where(cart => cart.OrderId == order.OrderId).Select(cart => new CartDTO
                                {
                                    CartId = cart.CartId,
                                    ProductId = cart.ProductId,
                                    Quantity = cart.Quantity,
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
                                        Pictures = cart.Product.Pictures.Where(pic => pic.ProductId == cart.ProductId).Select(pic => new PictureDTO
                                        {
                                            PictureId = pic.PictureId,
                                            Path = pic.Path
                                        }).ToList(),
                                        Comments = cart.Product.Comments.Where(comment => comment.ProductId == cart.ProductId).Select(comment => new CommentDTO
                                        {
                                            CommentId = comment.CommentId
                                        }).ToList()
                                    },
                                }).ToList(),
                                PaymentNotifications = order.PaymentNotifications.Where(payN => payN.OrderId == order.OrderId).Select(payN => new PaymentNotificationDTO
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
                            }).ToList(),
                            Comments = p.Comments.Where(comment => comment.UserId == p.UserId).Select(comment => new CommentDTO
                            {
                                CommentId = comment.CommentId,
                                Text = comment.Text,
                                PublishedDate = comment.PublishedDate,
                                Status = comment.Status,
                                ProductId = comment.ProductId,
                                UserId = comment.UserId,
                                Product = _context.Products.Where(product => product.ProductId == comment.ProductId).Select(product => new ProductDTO
                                {
                                    ProductId = product.ProductId,
                                    CategoryId = product.CategoryId,
                                    SKU = product.SKU,
                                    Name = product.Name,
                                    Description = product.Description,
                                    Price = product.Price,
                                    Stock = product.Stock,
                                    Status = product.Status,
                                    Category = new CategoryDTO
                                    {
                                        CategoryId = product.Category.CategoryId,
                                        Name = product.Category.Name,
                                        Status = product.Category.Status
                                    },
                                    Pictures = product.Pictures.Where(pic => pic.ProductId == comment.ProductId).Select(pic => new PictureDTO
                                    {
                                        PictureId = pic.PictureId,
                                        Path = pic.Path
                                    }).ToList(),
                                    Comments = product.Comments.Where(commentc => commentc.ProductId == comment.ProductId).Select(commentc => new CommentDTO
                                    {
                                        CommentId = commentc.CommentId
                                    }).ToList()
                                }).FirstOrDefault(),
                                User = _context.Users.Where(user => user.UserId == comment.UserId).Select(user => new UserDTO
                                {
                                    UserId = user.UserId,
                                    Name = user.Name,
                                    Surname = user.Surname,
                                    Email = user.Email,
                                    PhoneNumber = user.PhoneNumber,
                                }).FirstOrDefault()
                            }).ToList(),
                            Carts = p.Carts.Where(cart => cart.UserId == p.UserId && cart.OrderId == null).Select(cart => new CartDTO
                            {
                                CartId = cart.CartId,
                                ProductId = cart.ProductId,
                                Quantity = cart.Quantity,
                                AddDate = cart.AddDate,
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
                                    Pictures = cart.Product.Pictures.Where(pic => pic.ProductId == cart.ProductId).Select(pic => new PictureDTO
                                    {
                                        PictureId = pic.PictureId,
                                        Path = pic.Path
                                    }).ToList(),
                                    Comments = cart.Product.Comments.Select(comment => new CommentDTO
                                    {
                                        CommentId = comment.CommentId
                                    }).ToList()
                                },
                            }).ToList()
                        })
                        .FirstOrDefaultAsync();

                return Ok(users);
            }

            return Unauthorized();
        }

        [HttpPut("User/{id}")]
        public async Task<IActionResult> PostUser(UserModel Form, int id)
        {
            if (await CheckAdmin() == true)
            {
                var userItem = await _context.Users.FirstOrDefaultAsync(c => c.UserId == id);

                if (userItem == null)
                {
                    return NotFound();
                }

                if (Form.Password == null)
                {
                    userItem.Name = Form.Name;
                    userItem.Surname = Form.Surname;
                    userItem.Email = Form.Email;
                    userItem.PhoneNumber = Form.PhoneNumber;
                    userItem.Status = Form.Status;
                    userItem.Admin = Form.Admin;
                }
                else
                {
                    if (Form.Password == Form.TPassword)
                    {
                        userItem.Password = Form.Password;
                    }
                    else
                    {
                        return Ok(new { status = false, msg = "Yeni Şifreler Eşleşmiyor" });
                    }
                }

                _context.Users.Update(userItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Kullanıcı Bilgileri Güncellendi" });
            }

            return Unauthorized();
        }

        [HttpGet("Addresses")]
        public async Task<IActionResult> GetAddresses()
        {
            if (await CheckAdmin() == true)
            {
                var addresses = await _context.Addresses
                        .OrderBy(p => p.AddressId)
                        .Select(p => new AddressDTO
                        {
                            AddressId = p.AddressId,
                            UserId = p.UserId,
                            User = _context.Users.Where(c => c.UserId == p.UserId).Select(u => new UserDTO
                            {
                                UserId = u.UserId,
                                Name = u.Name,
                                Surname = u.Surname,
                                Email = u.Email,
                                PhoneNumber = u.PhoneNumber,
                                Status = u.Status,
                                Admin = u.Admin

                            }).FirstOrDefault(),
                            AddressText = p.AddressText,
                            Province = p.Province,
                            District = p.District,
                            Country = p.Country,
                            PostalCode = p.PostalCode,
                        })
                        .ToListAsync();

                return Ok(addresses);
            }

            return Unauthorized();
        }

        [HttpGet("Address/{id}")]
        public async Task<IActionResult> GetAddress(int id)
        {
            if (await CheckAdmin() == true)
            {
                var address = await _context.Addresses
                        .OrderBy(p => p.AddressId)
                        .Where(p => p.AddressId == id)
                        .Select(p => new AddressDTO
                        {
                            AddressId = p.AddressId,
                            UserId = p.UserId,
                            User = _context.Users.Where(c => c.UserId == p.UserId).Select(u => new UserDTO
                            {
                                UserId = u.UserId,
                                Name = u.Name,
                                Surname = u.Surname,
                                Email = u.Email,
                                PhoneNumber = u.PhoneNumber,
                                Status = u.Status,
                                Admin = u.Admin

                            }).FirstOrDefault(),
                            AddressText = p.AddressText,
                            Province = p.Province,
                            District = p.District,
                            Country = p.Country,
                            PostalCode = p.PostalCode,
                        })
                        .FirstOrDefaultAsync();

                return Ok(address);
            }

            return Unauthorized();
        }

        [HttpGet("Products")]
        public async Task<IActionResult> Products()
        {
            if (await CheckAdmin() == true)
            {
                var products = await _context.Products
                        .OrderBy(p => p.ProductId)
                        .Select(p => new ProductDTO
                        {
                            ProductId = p.ProductId,
                            CategoryId = p.CategoryId,
                            SKU = p.SKU,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            Stock = p.Stock,
                            Status = p.Status,
                            Category = new CategoryDTO
                            {
                                CategoryId = p.Category.CategoryId,
                                Name = p.Category.Name,
                                Status = p.Category.Status
                            },
                            Pictures = p.Pictures.Select(pic => new PictureDTO
                            {
                                PictureId = pic.PictureId,
                                Path = pic.Path
                            }).ToList(),
                            Comments = p.Comments.Select(comment => new CommentDTO
                            {
                                CommentId = comment.CommentId,
                                Text = comment.Text,
                                PublishedDate = comment.PublishedDate,
                                Status = comment.Status,
                                ProductId = comment.ProductId,
                                UserId = comment.UserId
                            }).ToList()
                        })
                        .ToListAsync();

                return Ok(products);
            }

            return Unauthorized();
        }

        [HttpGet("Product/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if (await CheckAdmin() == true)
            {
                var products = await _context.Products
                        .OrderBy(p => p.ProductId)
                        .Where(p => p.ProductId == id)
                        .Select(p => new ProductDTO
                        {
                            ProductId = p.ProductId,
                            CategoryId = p.CategoryId,
                            SKU = p.SKU,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            Stock = p.Stock,
                            Status = p.Status,
                            Category = new CategoryDTO
                            {
                                CategoryId = p.Category.CategoryId,
                                Name = p.Category.Name,
                                Status = p.Category.Status
                            },
                            Pictures = p.Pictures.Select(pic => new PictureDTO
                            {
                                PictureId = pic.PictureId,
                                Path = pic.Path
                            }).ToList(),
                            Comments = p.Comments.Select(comment => new CommentDTO
                            {
                                CommentId = comment.CommentId,
                                Text = comment.Text,
                                PublishedDate = comment.PublishedDate,
                                Status = comment.Status,
                                ProductId = comment.ProductId,
                                UserId = comment.UserId
                            }).ToList()
                        })
                        .FirstOrDefaultAsync();

                return Ok(products);
            }

            return Unauthorized();
        }

        [HttpPut("Product/{id}")]
        public async Task<IActionResult> EditProduct(ProductModel Form, int id)
        {
            if (await CheckAdmin() == true)
            {
                var productItem = await _context.Products.FirstOrDefaultAsync(c => c.ProductId == id);

                if (productItem == null)
                {
                    return NotFound();
                }

                productItem.Name = Form.Name;
                productItem.CategoryId = Form.CategoryId;
                productItem.Description = Form.Description;
                productItem.SKU = Form.Sku;
                productItem.Price = Form.Price;
                productItem.Stock = Form.Stock;
                productItem.Status = Form.Status;


                _context.Products.Update(productItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Ürün Bilgileri Güncellendi" });
            }

            return Unauthorized();
        }

        [HttpGet("Images/{id}")]
        public async Task<IActionResult> GetImages(int id)
        {
            if (await CheckAdmin() == true)
            {
                var products = await _context.Pictures
                        .OrderBy(p => p.PictureId)
                        .Where(p => p.ProductId == id)
                        .Select(p => new PictureDTO
                        {
                            PictureId = p.PictureId,
                            ProductId = p.ProductId,
                            Path = p.Path
                        })
                        .ToListAsync();

                return Ok(products);
            }

            return Unauthorized();
        }

        [HttpPost("Image/{id}")]
        public async Task<IActionResult> AddImage([FromBody] dynamic data, int id)
        {
            if (await CheckAdmin() == true)
            {
                var newImage = new Picture
                {
                    ProductId = id,
                    Path = data.file
                };

                _context.Pictures.Add(newImage);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Resim Yüklendi" });
            }

            return Unauthorized();
        }

        [HttpDelete("Image/{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            if (await CheckAdmin() == true)
            {
                var imageItem = await _context.Pictures.FirstOrDefaultAsync(c => c.PictureId == id);

                if (imageItem == null)
                {
                    return NotFound();
                }

                _context.Pictures.Remove(imageItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Resim Silindi" });
            }

            return Unauthorized();
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> Categories()
        {
            if (await CheckAdmin() == true)
            {
                var categories = await _context.Categories
                        .OrderBy(p => p.CategoryId)
                        .Select(p => new CategoryDTO
                        {
                            CategoryId = p.CategoryId,
                            Name = p.Name,
                            Status = p.Status,
                            Products = _context.Products.Where(c => c.CategoryId == p.CategoryId).Count()
                        })
                        .ToListAsync();

                return Ok(categories);
            }

            return Unauthorized();
        }

        [HttpGet("Category/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            if (await CheckAdmin() == true)
            {
                var users = await _context.Categories
                        .OrderBy(p => p.CategoryId)
                        .Where(p => p.CategoryId == id)
                        .Select(p => new CategoryDTO
                        {
                            CategoryId = p.CategoryId,
                            Name = p.Name,
                            Status = p.Status,
                            Products = _context.Products.Where(c => c.CategoryId == p.CategoryId).Count()
                        })
                        .FirstOrDefaultAsync();

                return Ok(users);
            }

            return Unauthorized();
        }

        [HttpPut("Category/{id}")]
        public async Task<IActionResult> PostCategory(CategoryModel Form, int id)
        {
            if (await CheckAdmin() == true)
            {
                var categoryItem = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

                if (categoryItem == null)
                {
                    return NotFound();
                }

                categoryItem.Name = Form.Name;
                categoryItem.Status = Form.Status;

                _context.Categories.Update(categoryItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Kategori Bilgileri Güncellendi" });
            }

            return Unauthorized();
        }

        [HttpGet("Comments")]
        public async Task<IActionResult> Comments()
        {
            if (await CheckAdmin() == true)
            {
                var comments = await _context.Comments.OrderBy(p => p.CommentId).Select(comment => new CommentDTO
                {
                    CommentId = comment.CommentId,
                    Text = comment.Text,
                    PublishedDate = comment.PublishedDate,
                    Status = comment.Status,
                    ProductId = comment.ProductId,
                    UserId = comment.UserId,
                    Product = _context.Products.Where(product => product.ProductId == comment.ProductId).Select(product => new ProductDTO
                    {
                        ProductId = product.ProductId,
                        CategoryId = product.CategoryId,
                        SKU = product.SKU,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Stock = product.Stock,
                        Status = product.Status,
                        Category = new CategoryDTO
                        {
                            CategoryId = product.Category.CategoryId,
                            Name = product.Category.Name,
                            Status = product.Category.Status
                        },
                        Pictures = product.Pictures.Where(pic => pic.ProductId == comment.ProductId).Select(pic => new PictureDTO
                        {
                            PictureId = pic.PictureId,
                            Path = pic.Path
                        }).ToList(),
                        Comments = product.Comments.Where(commentc => commentc.ProductId == comment.ProductId).Select(commentc => new CommentDTO
                        {
                            CommentId = commentc.CommentId
                        }).ToList()
                    }).FirstOrDefault(),
                    User = _context.Users.Where(user => user.UserId == comment.UserId).Select(user => new UserDTO
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                    }).FirstOrDefault()
                }).ToListAsync();

                return Ok(comments);
            }

            return Unauthorized();
        }

        [HttpGet("Comment/{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            if (await CheckAdmin() == true)
            {
                var comment = await _context.Comments.OrderBy(p => p.CommentId).Where(p => p.CommentId == id).Select(comment => new CommentDTO
                {
                    CommentId = comment.CommentId,
                    Text = comment.Text,
                    PublishedDate = comment.PublishedDate,
                    Status = comment.Status,
                    ProductId = comment.ProductId,
                    UserId = comment.UserId,
                    Product = _context.Products.Where(product => product.ProductId == comment.ProductId).Select(product => new ProductDTO
                    {
                        ProductId = product.ProductId,
                        CategoryId = product.CategoryId,
                        SKU = product.SKU,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Stock = product.Stock,
                        Status = product.Status,
                        Category = new CategoryDTO
                        {
                            CategoryId = product.Category.CategoryId,
                            Name = product.Category.Name,
                            Status = product.Category.Status
                        },
                        Pictures = product.Pictures.Where(pic => pic.ProductId == comment.ProductId).Select(pic => new PictureDTO
                        {
                            PictureId = pic.PictureId,
                            Path = pic.Path
                        }).ToList(),
                        Comments = product.Comments.Where(commentc => commentc.ProductId == comment.ProductId).Select(commentc => new CommentDTO
                        {
                            CommentId = commentc.CommentId
                        }).ToList()
                    }).FirstOrDefault(),
                    User = _context.Users.Where(user => user.UserId == comment.UserId).Select(user => new UserDTO
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Status = user.Status,
                        Admin = user.Admin
                    }).FirstOrDefault()
                }).FirstOrDefaultAsync();

                return Ok(comment);
            }

            return Unauthorized();
        }

        [HttpPut("Comment/{id}")]
        public async Task<IActionResult> EditComment(CommentModel Form, int id)
        {
            if (await CheckAdmin() == true)
            {
                var commentItem = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == id);

                if (commentItem == null)
                {
                    return NotFound();
                }

                commentItem.Text = Form.Text;
                commentItem.Status = Form.Status;

                _context.Comments.Update(commentItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Yorum Bilgileri Güncellendi" });
            }

            return Unauthorized();
        }

        [HttpDelete("Comment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (await CheckAdmin() == true)
            {
                var commentItem = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == id);

                if (commentItem == null)
                {
                    return NotFound();
                }

                _context.Comments.Remove(commentItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Yorum Silindi." + "<meta http-equiv='refresh' content='2;'>" });
            }

            return Unauthorized();
        }

        [HttpGet("Coupons")]
        public async Task<IActionResult> Coupons()
        {
            if (await CheckAdmin() == true)
            {
                var coupons = await _context.Coupons.OrderBy(p => p.CouponId).Select(coupon => new CouponDTO
                {
                    CouponId = coupon.CouponId,
                    Name = coupon.Name,
                    Type = coupon.Type,
                    DiscountAmount = coupon.DiscountAmount,
                    CouponCode = coupon.CouponCode,
                    ValidityDate = coupon.ValidityDate,
                    SingleUse = coupon.SingleUse,
                    Status = coupon.Status
                }).ToListAsync();

                return Ok(coupons);
            }

            return Unauthorized();
        }

        [HttpGet("Coupon/{id}")]
        public async Task<IActionResult> GetCoupon(int id)
        {
            if (await CheckAdmin() == true)
            {
                var coupon = await _context.Coupons.OrderBy(p => p.CouponId).Where(p => p.CouponId == id).Select(coupon => new CouponDTO
                {
                    CouponId = coupon.CouponId,
                    Name = coupon.Name,
                    Type = coupon.Type,
                    DiscountAmount = coupon.DiscountAmount,
                    CouponCode = coupon.CouponCode,
                    ValidityDate = coupon.ValidityDate,
                    SingleUse = coupon.SingleUse,
                    Status = coupon.Status
                }).FirstOrDefaultAsync();

                return Ok(coupon);
            }

            return Unauthorized();
        }

        [HttpPut("Coupon/{id}")]
        public async Task<IActionResult> EditCoupon(CouponsModel Form, int id)
        {
            if (await CheckAdmin() == true)
            {
                var couponItem = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);

                if (couponItem == null)
                {
                    return NotFound();
                }

                couponItem.Name = Form.Name;
                couponItem.Type = Form.Type;
                couponItem.DiscountAmount = Form.DiscountAmount;
                couponItem.CouponCode = Form.CouponCode;
                couponItem.ValidityDate = Form.ValidityDate;
                couponItem.SingleUse = Form.SingleUse;
                couponItem.Status = Form.Status;

                _context.Coupons.Update(couponItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Kupon Bilgileri Güncellendi" });
            }

            return Unauthorized();
        }

        [HttpGet("CouponHistory/{id}")]
        public async Task<IActionResult> GetCouponHistory(int id)
        {
            if (await CheckAdmin() == true)
            {
                var coupon_history = await _context.CouponHistorys.OrderBy(p => p.CouponHistoryId).Where(p => p.CouponId == id).Select(p => new CouponHistoryDTO
                {
                    CouponHistoryId = p.CouponHistoryId,
                    CouponId = p.CouponId,
                    UserId = p.UserId,
                    OrderId = p.OrderId,
                    Status = p.Status,
                    Coupon = _context.Coupons.OrderBy(c => c.CouponId).Where(c => c.CouponId == id).Select(c => new CouponDTO
                    {
                        CouponId = p.Coupon.CouponId,
                        Name = p.Coupon.Name,
                        Type = p.Coupon.Type,
                        DiscountAmount = p.Coupon.DiscountAmount,
                        CouponCode = p.Coupon.CouponCode,
                        ValidityDate = p.Coupon.ValidityDate,
                    }).FirstOrDefault(),
                    User = _context.Users.Where(user => user.UserId == p.UserId).Select(user => new UserDTO
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Status = user.Status,
                        Admin = user.Admin
                    }).FirstOrDefault()
                }).ToListAsync();

                return Ok(coupon_history);
            }

            return Unauthorized();
        }

        [HttpDelete("Cart/{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (await CheckAdmin() == true)
            {
                var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == id);

                if (cartItem == null)
                {
                    return NotFound();
                }

                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Ürün Sepetten Kaldırıldı." + "<meta http-equiv='refresh' content='1;'>" });
            }

            return Unauthorized();
        }

        [HttpGet("Sliders")]
        public async Task<IActionResult> GetSliders()
        {
            if (await CheckAdmin() == true)
            {
                var sliders = await _context.Slider
                    .OrderBy(p => p.SliderId)
                    .Select(p => new SliderDTO
                    {
                        SliderId = p.SliderId,
                        Title = p.Title,
                        SubTitle = p.SubTitle,
                        Description = p.Description,
                        ButtonTitle = p.ButtonTitle,
                        ButtonUrl = p.ButtonUrl,
                        BackgroundImg = p.BackgroundImg,
                        Status = p.Status
                    })
                    .ToListAsync();

                return Ok(sliders);
            }

            return Unauthorized();
        }

        [HttpGet("Slider/{id}")]
        public async Task<IActionResult> GetSlider(int id)
        {
            if (await CheckAdmin() == true)
            {
                var slider = await _context.Slider
                    .OrderBy(p => p.SliderId)
                    .Where(p => p.SliderId == id)
                    .Select(p => new SliderDTO
                    {
                        SliderId = p.SliderId,
                        Title = p.Title,
                        SubTitle = p.SubTitle,
                        Description = p.Description,
                        ButtonTitle = p.ButtonTitle,
                        ButtonUrl = p.ButtonUrl,
                        BackgroundImg = p.BackgroundImg,
                        Status = p.Status
                    })
                    .FirstOrDefaultAsync();

                return Ok(slider);
            }

            return Unauthorized();
        }

        [HttpPut("Slider/{id}")]
        public async Task<IActionResult> SetSlider(SliderModel Form, int id = 0)
        {
            if (await CheckAdmin() == true)
            {
                var sliderItem = await _context.Slider.FirstOrDefaultAsync(c => c.SliderId == id);

                if (sliderItem == null)
                {
                    return NotFound();
                }

                if (Form.BackgroundImg == null)
                {
                    sliderItem.Title = Form.Title;
                    sliderItem.SubTitle = Form.SubTitle;
                    sliderItem.Description = Form.Description;
                    sliderItem.ButtonTitle = Form.ButtonTitle;
                    sliderItem.ButtonUrl = Form.ButtonUrl;
                    sliderItem.Status = Form.Status;
                }
                else
                {
                    sliderItem.Title = Form.Title;
                    sliderItem.SubTitle = Form.SubTitle;
                    sliderItem.Description = Form.Description;
                    sliderItem.ButtonTitle = Form.ButtonTitle;
                    sliderItem.ButtonUrl = Form.ButtonUrl;
                    sliderItem.Status = Form.Status;
                    sliderItem.BackgroundImg = Form.BackgroundImg;
                }

                _context.Slider.Update(sliderItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Slider Bilgileri Güncellendi" + "<meta http-equiv='refresh' content='2;'>" });
            }

            return Unauthorized();
        }

        [HttpPost("Slider")]
        public async Task<IActionResult> AddSlider(SliderModel Form)
        {
            if (await CheckAdmin() == true)
            {
                var sliderItem = new Slider
                {
                    Title = Form.Title,
                    SubTitle = Form.SubTitle,
                    Description = Form.Description,
                    ButtonTitle = Form.ButtonTitle,
                    ButtonUrl = Form.ButtonUrl,
                    Status = Form.Status,
                    BackgroundImg = Form.BackgroundImg,
                };

                _context.Slider.Add(sliderItem);
                await _context.SaveChangesAsync();

                return Json(new { status = true, msg = "Slider Eklendi" + "<meta http-equiv='refresh' content='2;URL=/Admin/Sliders'>" });
            }

            return Unauthorized();
        }

        [HttpDelete("Slider/{id}")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            if (await CheckAdmin() == true)
            {
                var sliderItem = await _context.Slider.FirstOrDefaultAsync(c => c.SliderId == id);

                if (sliderItem == null)
                {
                    return NotFound();
                }

                _context.Slider.Remove(sliderItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Slider Silindi." + "<meta http-equiv='refresh' content='1;'>" });
            }

            return Unauthorized();
        }

        [HttpGet("PaymentNotifications")]
        public async Task<IActionResult> GetPaymentNotifications()
        {
            if (await CheckAdmin() == true)
            {
                var paymentNotifications = await _context.PaymentNotifications
                    .OrderBy(p => p.PaymentId)
                    .Select(p => new PaymentNotificationDTO
                    {
                        PaymentId = p.PaymentId,
                        OrderId = p.OrderId,
                        BankId = p.BankId,
                        NameSurname = p.NameSurname,
                        TotalAmount = p.TotalAmount,
                        Receipt = p.Receipt,
                        PayNote = p.PayNote,
                        PayDate = p.PayDate,
                        Status = p.Status,
                        Bank = _context.Banks.Where(bank => bank.BankId == p.BankId).Select(bank => new BankDTO
                        {
                            BankId = bank.BankId,
                            Name = bank.Name,
                            AccountName = bank.AccountName,
                            AccountNo = bank.AccountNo,
                            Branch = bank.Branch,
                            IBAN = bank.IBAN,
                            Status = bank.Status
                        }).FirstOrDefault(),
                        Order = _context.Orders.Where(order => order.OrderId == p.OrderId).Select(order => new OrderDTO
                        {
                            OrderId = order.OrderId,
                            AddressId = order.AddressId,
                            Address = new AddressDTO
                            {
                                AddressId = order.Address.AddressId,
                                AddressText = order.Address.AddressText,
                                Province = order.Address.Province,
                                District = order.Address.District,
                                Country = order.Address.Country,
                                PostalCode = order.Address.PostalCode,
                            },
                            UserId = order.UserId,
                            User = new UserDTO
                            {
                                Name = order.User.Name,
                                Surname = order.User.Surname,
                                Email = order.User.Email,
                                PhoneNumber = order.User.PhoneNumber,
                            },
                            OrderKey = order.OrderKey,
                            Amount = order.Amount,
                            CouponAmount = order.CouponAmount,
                            TotalAmount = order.TotalAmount,
                            CouponHistoryId = order.CouponHistoryId,
                            CouponHistory = order.CouponHistoryId == null ? null : new CouponHistoryDTO
                            {
                                CouponHistoryId = order.CouponHistory.CouponHistoryId,
                                CouponId = order.CouponHistory.CouponId,
                                Coupon = new CouponDTO
                                {
                                    CouponId = order.CouponHistory.Coupon.CouponId,
                                    Name = order.CouponHistory.Coupon.Name,
                                    Type = order.CouponHistory.Coupon.Type,
                                    DiscountAmount = order.CouponHistory.Coupon.DiscountAmount,
                                    CouponCode = order.CouponHistory.Coupon.CouponCode,
                                    ValidityDate = order.CouponHistory.Coupon.ValidityDate,
                                },
                            },
                            OrderNote = order.OrderNote,
                            OrderPay = order.OrderPay,
                            OrderDate = order.OrderDate,
                            OrderStatus = order.OrderStatus,
                            Status = order.Status,
                            Carts = order.Carts.Select(cart => new CartDTO
                            {
                                CartId = cart.CartId,
                                ProductId = cart.ProductId,
                                Quantity = cart.Quantity,
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
                                    Pictures = cart.Product.Pictures.Where(pic => pic.ProductId == cart.ProductId).Select(pic => new PictureDTO
                                    {
                                        PictureId = pic.PictureId,
                                        Path = pic.Path
                                    }).ToList(),
                                    Comments = cart.Product.Comments.Where(comment => comment.ProductId == cart.ProductId).Select(comment => new CommentDTO
                                    {
                                        CommentId = comment.CommentId
                                    }).ToList()
                                },
                            }).ToList(),
                            PaymentNotifications = order.PaymentNotifications.Where(payN => payN.OrderId == order.OrderId).Select(payN => new PaymentNotificationDTO
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
                        }).FirstOrDefault(),
                    }).ToListAsync();

                return Ok(paymentNotifications);
            }

            return Unauthorized();
        }

        [HttpGet("PaymentNotification/{id}")]
        public async Task<IActionResult> GetPaymentNotification(int id)
        {
            if (await CheckAdmin() == true)
            {
                var paymentNotification = await _context.PaymentNotifications
                    .OrderBy(p => p.PaymentId)
                    .Where(p => p.PaymentId == id)
                    .Select(p => new PaymentNotificationDTO
                    {
                        PaymentId = p.PaymentId,
                        OrderId = p.OrderId,
                        BankId = p.BankId,
                        NameSurname = p.NameSurname,
                        TotalAmount = p.TotalAmount,
                        Receipt = p.Receipt,
                        PayNote = p.PayNote,
                        PayDate = p.PayDate,
                        Status = p.Status,
                        Bank = _context.Banks.Where(bank => bank.BankId == p.BankId).Select(bank => new BankDTO
                        {
                            BankId = bank.BankId,
                            Name = bank.Name,
                            AccountName = bank.AccountName,
                            AccountNo = bank.AccountNo,
                            Branch = bank.Branch,
                            IBAN = bank.IBAN,
                            Status = bank.Status
                        }).FirstOrDefault(),
                        Order = _context.Orders.Where(order => order.OrderId == p.OrderId).Select(order => new OrderDTO
                        {
                            OrderId = order.OrderId,
                            AddressId = order.AddressId,
                            Address = new AddressDTO
                            {
                                AddressId = order.Address.AddressId,
                                AddressText = order.Address.AddressText,
                                Province = order.Address.Province,
                                District = order.Address.District,
                                Country = order.Address.Country,
                                PostalCode = order.Address.PostalCode,
                            },
                            UserId = order.UserId,
                            User = new UserDTO
                            {
                                Name = order.User.Name,
                                Surname = order.User.Surname,
                                Email = order.User.Email,
                                PhoneNumber = order.User.PhoneNumber,
                            },
                            OrderKey = order.OrderKey,
                            Amount = order.Amount,
                            CouponAmount = order.CouponAmount,
                            TotalAmount = order.TotalAmount,
                            CouponHistoryId = order.CouponHistoryId,
                            CouponHistory = order.CouponHistoryId == null ? null : new CouponHistoryDTO
                            {
                                CouponHistoryId = order.CouponHistory.CouponHistoryId,
                                CouponId = order.CouponHistory.CouponId,
                                Coupon = new CouponDTO
                                {
                                    CouponId = order.CouponHistory.Coupon.CouponId,
                                    Name = order.CouponHistory.Coupon.Name,
                                    Type = order.CouponHistory.Coupon.Type,
                                    DiscountAmount = order.CouponHistory.Coupon.DiscountAmount,
                                    CouponCode = order.CouponHistory.Coupon.CouponCode,
                                    ValidityDate = order.CouponHistory.Coupon.ValidityDate,
                                },
                            },
                            OrderNote = order.OrderNote,
                            OrderPay = order.OrderPay,
                            OrderDate = order.OrderDate,
                            OrderStatus = order.OrderStatus,
                            Status = order.Status,
                            Carts = order.Carts.Select(cart => new CartDTO
                            {
                                CartId = cart.CartId,
                                ProductId = cart.ProductId,
                                Quantity = cart.Quantity,
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
                                    Pictures = cart.Product.Pictures.Where(pic => pic.ProductId == cart.ProductId).Select(pic => new PictureDTO
                                    {
                                        PictureId = pic.PictureId,
                                        Path = pic.Path
                                    }).ToList(),
                                    Comments = cart.Product.Comments.Where(comment => comment.ProductId == cart.ProductId).Select(comment => new CommentDTO
                                    {
                                        CommentId = comment.CommentId
                                    }).ToList()
                                },
                            }).ToList(),
                            PaymentNotifications = order.PaymentNotifications.Where(payN => payN.OrderId == order.OrderId).Select(payN => new PaymentNotificationDTO
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
                        }).FirstOrDefault(),
                    }).FirstOrDefaultAsync();

                return Ok(paymentNotification);
            }

            return Unauthorized();
        }

        [HttpPut("PaymentNotification/{id}")]
        public async Task<IActionResult> SetPaymentNotification(StatusModel Form, int id = 0)
        {
            if (await CheckAdmin() == true)
            {
                var payItem = await _context.PaymentNotifications.FirstOrDefaultAsync(c => c.PaymentId == id);

                if (payItem == null)
                {
                    return NotFound();
                }

                payItem.Status = Form.Status;

                _context.PaymentNotifications.Update(payItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Ödeme Bilgileri Güncellendi" + "<meta http-equiv='refresh' content='2;'>" });
            }

            return Unauthorized();
        }

        [HttpDelete("PaymentNotification/{id}")]
        public async Task<IActionResult> DeletePaymentNotification(int id)
        {
            if (await CheckAdmin() == true)
            {
                var payItem = await _context.PaymentNotifications.FirstOrDefaultAsync(c => c.PaymentId == id);

                if (payItem == null)
                {
                    return NotFound();
                }

                _context.PaymentNotifications.Remove(payItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Ödeme Bildirimi Silindi." + "<meta http-equiv='refresh' content='1;'>" });
            }

            return Unauthorized();
        }

        [HttpGet("Banks")]
        public async Task<IActionResult> GetBanks()
        {
            if (await CheckAdmin() == true)
            {
                var banks = await _context.Banks
                    .OrderBy(p => p.BankId)
                    .Select(p => new BankDTO
                    {
                        BankId = p.BankId,
                        Name = p.Name,
                        AccountName = p.AccountName,
                        AccountNo = p.AccountNo,
                        Branch = p.Branch,
                        IBAN = p.IBAN,
                        Status = p.Status
                    })
                    .ToListAsync();

                return Ok(banks);
            }

            return Unauthorized();
        }

        [HttpGet("Bank/{id}")]
        public async Task<IActionResult> GetBanks(int id)
        {
            if (await CheckAdmin() == true)
            {
                var bank = await _context.Banks
                    .OrderBy(p => p.BankId)
                    .Where(p => p.BankId == id)
                    .Select(p => new BankDTO
                    {
                        BankId = p.BankId,
                        Name = p.Name,
                        AccountName = p.AccountName,
                        AccountNo = p.AccountNo,
                        Branch = p.Branch,
                        IBAN = p.IBAN,
                        Status = p.Status
                    })
                    .FirstOrDefaultAsync();

                return Ok(bank);
            }

            return Unauthorized();
        }

        [HttpPut("Bank/{id}")]
        public async Task<IActionResult> SetBank(BankModel Form, int id = 0)
        {
            if (await CheckAdmin() == true)
            {
                var bankItem = await _context.Banks.FirstOrDefaultAsync(c => c.BankId == id);

                if (bankItem == null)
                {
                    return NotFound();
                }

                bankItem.Name = Form.Name;
                bankItem.AccountName = Form.AccountName;
                bankItem.AccountNo = Form.AccountNo;
                bankItem.Branch = Form.Branch;
                bankItem.IBAN = Form.IBAN;
                bankItem.Status = Form.Status;

                _context.Banks.Update(bankItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Banka Bilgileri Güncellendi" + "<meta http-equiv='refresh' content='2;'>" });
            }

            return Unauthorized();
        }

        [HttpPost("Bank")]
        public async Task<IActionResult> AddBank(BankModel Form)
        {
            if (await CheckAdmin() == true)
            {
                var bankItem = new Bank
                {
                    Name = Form.Name,
                    AccountName = Form.AccountName,
                    AccountNo = Form.AccountNo,
                    Branch = Form.Branch,
                    IBAN = Form.IBAN,
                    Status = Form.Status
                };

                _context.Banks.Add(bankItem);
                await _context.SaveChangesAsync();

                return Json(new { status = true, msg = "Banka Hesabı Eklendi" + "<meta http-equiv='refresh' content='2;URL=/Admin/Banks'>" });
            }

            return Unauthorized();
        }

        [HttpDelete("Bank/{id}")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            if (await CheckAdmin() == true)
            {
                var bankItem = await _context.Banks.FirstOrDefaultAsync(c => c.BankId == id);

                if (bankItem == null)
                {
                    return NotFound();
                }

                _context.Banks.Remove(bankItem);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Banka Silindi." + "<meta http-equiv='refresh' content='1;'>" });
            }

            return Unauthorized();
        }

        [HttpPost("Settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] Dictionary<string, string> settings)
        {
            if (await CheckAdmin() == true)
            {
                if (settings == null || settings.Count == 0)
                {
                    return BadRequest("Invalid settings data.");
                }

                foreach (var setting in settings)
                {
                    var existingSetting = await _context.Settings.FirstOrDefaultAsync(s => s.Mkey == setting.Key);
                    if (existingSetting != null)
                    {
                        existingSetting.Mval = setting.Value;
                        _context.Settings.Update(existingSetting);
                    }
                    else
                    {
                        var newSetting = new Setting
                        {
                            Mkey = setting.Key,
                            Mval = setting.Value
                        };
                        await _context.Settings.AddAsync(newSetting);
                    }
                }

                await _context.SaveChangesAsync();

                return Ok(new { status = true, msg = "Ayarlar Güncellendi." + "<meta http-equiv='refresh' content='2;'>" });
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

        private async Task<bool> WhereAdmin(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (user != null)
            {
                if (user.Admin == true)
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private async Task<bool> CheckAdmin()
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string tokenWithoutBearer = token.Replace("Bearer ", "");
                var userInfo = ValidateToken(tokenWithoutBearer);
                if (userInfo != null)
                {
                    bool deger_get = await WhereAdmin(userInfo.UserId);

                    if (deger_get == true)
                    {
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }
    }
}
