using E_Ticaret_API.Data;
using E_Ticaret_API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using static Azure.Core.HttpHeader;

namespace E_Ticaret_API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class DataController : Controller
    {
        private readonly DataContext _context;

        public DataController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Setting")]
        public async Task<IActionResult> Setting()
        {
            var settings = await _context.Settings.ToListAsync();
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;

            foreach (var setting in settings)
            {
                expandoObject[setting.Mkey] = setting.Mval;
            }

            return Ok(expandoObject);
        }

        [HttpGet("Slider")]
        public async Task<IActionResult> Slider()
        {
            var products = await _context.Slider
                                         .Where(p => p.Status)
                                         .Select(p => new SliderDTO
                                         {
                                             SliderId = p.SliderId,
                                             Title = p.Title,
                                             SubTitle = p.SubTitle,
                                             Description = p.Description,
                                             ButtonTitle = p.ButtonTitle,
                                             ButtonUrl = p.ButtonUrl,
                                             BackgroundImg = p.BackgroundImg
                                         })
                                         .ToListAsync();

            return Ok(products);
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> Categories()
        {
            var products = await _context.Categories
                                         .Where(p => p.Status)
                                         .Select(p => new CategoryDTO
                                         {
                                             CategoryId = p.CategoryId,
                                             Name = p.Name,
                                             Status = p.Status,
                                             Products = p.Products.Select(pro => new ProductDTO
                                             {
                                                 ProductId = pro.ProductId,
                                                 CategoryId = pro.CategoryId,
                                                 SKU = pro.SKU,
                                                 Name = pro.Name,
                                                 SeoURL = pro.SeoURL,
                                                 Description = pro.Description,
                                                 Price = pro.Price,
                                                 Stock = pro.Stock,
                                                 Status = pro.Status
                                             }).ToList().Count,
                                         })
                                         .ToListAsync();

            return Ok(products);
        }

        [HttpGet("Products/{page}")]
        public async Task<IActionResult> GetProducts(int page = 1, int cat = 0)
        {
            int page_size = 10;

            if(cat == 0)
            {
                int products_count = await _context.Products
                                         .Where(p => p.Status)
                                         .CountAsync();

                var products = await _context.Products
                                         .Include(p => p.Pictures)
                                         .Include(p => p.Category)
                                         .Include(p => p.Comments)
                                         .Where(p => p.Status)
                                         .OrderBy(p => p.ProductId)
                                         .Skip((page - 1) * page_size)
                                         .Take(page_size)
                                         .Select(p => new ProductDTO
                                         {
                                             ProductId = p.ProductId,
                                             CategoryId = p.CategoryId,
                                             SKU = p.SKU,
                                             Name = p.Name,
                                             SeoURL = p.SeoURL,
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
                return Json(new { TotalProduct = products_count, ProductPerPage = page_size, TotalPages = (int)Math.Ceiling((decimal)products_count / page_size), products });
            }
            else
            {
                int products_count = await _context.Products
                                         .Where(p => p.Status)
                                         .Where(p => p.CategoryId == cat)
                                         .CountAsync();

                var products = await _context.Products
                                         .Include(p => p.Pictures)
                                         .Include(p => p.Category)
                                         .Include(p => p.Comments)
                                         .Where(p => p.Status)
                                         .Where(p => p.CategoryId == cat)
                                         .OrderBy(p => p.ProductId)
                                         .Skip((page - 1) * page_size)
                                         .Take(page_size)
                                         .Select(p => new ProductDTO
                                         {
                                             ProductId = p.ProductId,
                                             CategoryId = p.CategoryId,
                                             SKU = p.SKU,
                                             Name = p.Name,
                                             SeoURL = p.SeoURL,
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
                return Json(new { TotalProduct = products_count, ProductPerPage = page_size, TotalPages = (int)Math.Ceiling((decimal)products_count / page_size), products });
            }
        }

        [HttpGet("Product/{seo_url}")]
        public async Task<IActionResult> GetProduct(string? seo_url)
        {
            if (seo_url == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                                         .Include(p => p.Pictures)
                                         .Include(p => p.Category)
                                         .Include(p => p.Comments)
                                         .Where(p => p.Status)
                                         .Where(p => p.SeoURL == seo_url)
                                         .Select(p => new ProductDTO
                                         {
                                             ProductId = p.ProductId,
                                             CategoryId = p.CategoryId,
                                             SKU = p.SKU,
                                             Name = p.Name,
                                             SeoURL = p.SeoURL,
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
                                             Comments = _context.Comments.Where(comment => comment.ProductId == p.ProductId && comment.Status == true).Select(comment => new CommentDTO
                                             {
                                                 CommentId = comment.CommentId,
                                                 Text = comment.Text,
                                                 PublishedDate = comment.PublishedDate,
                                                 Status = comment.Status,
                                                 ProductId = comment.ProductId,
                                                 UserId = comment.UserId,
                                                 User = _context.Users.Where(user => user.UserId == comment.UserId).Select(user => new UserDTO
                                                 {
                                                     Name = user.Name,
                                                     Surname = user.Surname,
                                                 }).FirstOrDefault()
                                             }).ToList()
                                         })
                                         .FirstOrDefaultAsync();

            return Ok(products);
        }

        [HttpGet("Blogs/{page}")]
        public async Task<IActionResult> GetBlogs(int page = 1)
        {
            int page_size = 10;

            int blogs_count = await _context.Blogs
                                        .Where(p => p.Status)
                                        .CountAsync();

            var blogs = await _context.Blogs
                                        .Where(p => p.Status)
                                        .OrderBy(p => p.BlogId)
                                        .Skip((page - 1) * page_size)
                                        .Take(page_size)
                                        .Select(p => new BlogDTO
                                        {
                                            BlogId = p.BlogId,
                                            Title = p.Title,
                                            SeoURL = p.SeoURL,
                                            Text = p.Text,
                                            Picture = p.Picture,
                                            PublishedDate = p.PublishedDate,
                                            Status = p.Status,
                                        })
                                        .ToListAsync();
            return Json(new { TotalProduct = blogs_count, ProductPerPage = page_size, TotalPages = (int)Math.Ceiling((decimal)blogs_count / page_size), blogs });
        }

        [HttpGet("Blog/{seo_url}")]
        public async Task<IActionResult> GetBlog(string seo_url)
        {
            var blog = await _context.Blogs.Where(p => p.SeoURL == seo_url).Select(p => new BlogDTO
            {
                BlogId = p.BlogId,
                Title = p.Title,
                SeoURL = p.SeoURL,
                Text = p.Text,
                PublishedDate = p.PublishedDate,
                Picture = p.Picture
            }).FirstOrDefaultAsync();

            return Ok(blog);
        }

        [HttpGet("Banks")]
        public async Task<IActionResult> Banks()
        {
            var products = await _context.Banks
                .Where(p => p.Status)
                .Select(p => new BankDTO
                {
                    BankId = p.BankId,
                    Name = p.Name,
                    AccountName = p.AccountName,
                    AccountNo = p.AccountNo,
                    Branch = p.Branch,
                    IBAN = p.IBAN,
                    Status = p.Status,
                })
                .ToListAsync();

            return Ok(products);
        }
    }
}
