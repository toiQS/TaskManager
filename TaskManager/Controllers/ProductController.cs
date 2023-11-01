using Data;
using ENTITY;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ImageModel;
using TaskManager.Models.ProductModel;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }
        // GET: api/<ProductController>
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ICollection<ProductIndexRequest>>> GetAllProductAsync()
        {
            if (_context.Products == null)
            {
                return Problem("không thể truy cập dữ liệu");
            }
            var product = await _context.Products.ToListAsync();
            var result = product.Select(p => new ProductIndexRequest
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName
            }).ToList();
            return Ok(result);
        }

        // GET api/<ProductController>/5
        [HttpGet("{productId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ProductDetailRequest>> GetProductByIdAsync(string productId)
        {
            if (_context.Products == null)
            {
                return Problem("không thể truy cập dữ liệu");
            }
            if (!string.IsNullOrEmpty(productId))
            {
                var product = await _context.Products.Where(p => p.ProductId == productId).Include(i => i.ProductImage).FirstOrDefaultAsync();
                if (product != null)
                {
                    var result = new ProductDetailRequest
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        ProductInfo = product.ProductInfo,
                        ProductImage = product.ProductImage.Select(x => new ImageIndexRequest
                        {
                            ImageId = x.ImageId,
                            ImageName = x.ImageName,
                            ProductId = product.ProductId,
                        }).ToList(),
                        BrandId = product.BrandId,
                        CategoryId = product.CategoryId,
                    };
                    return Ok(result);
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(ProductCreateResponse newproduct)
        {
            if (ModelState.IsValid)
            {
                if(_context.Products == null){
                    return Problem("không truy cập được dữ liệu");
                } 
                if(CheckProductExists(newproduct.ProductId)){
                    return Problem("dữ liệu đã tồn tại");
                }   
                var product = new Product
                {
                    ProductId = newproduct.ProductId,
                    ProductImage = new List<Image>(),
                    BrandId = newproduct.BrandId,
                    ProductName = newproduct.ProductName,
                    CategoryId = newproduct.CategoryId,
                    ProductInfo = newproduct.ProductInfo
                };
                try
                {
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Problem($"không thể cập nhật dữ liệu; {ex.Message}");
                }
                return CreatedAtAction(nameof(GetProductByIdAsync),new {productId = newproduct.ProductId},product);
            }
            return BadRequest("dữ liệu nhập vào không đúng");
        }

        // PUT api/<ProductController>/5
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductAsync(string productId, ProductUpdateResponse newproduct)
        {
            if (!string.IsNullOrEmpty(productId) && ModelState.IsValid)
            {
                if (_context.Products == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var currentproduct = await _context.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
                if (currentproduct != null)
                { 
                    currentproduct.ProductName = newproduct.ProductName;
                    currentproduct.ProductInfo = newproduct.ProductInfo;
                    currentproduct.BrandId = newproduct.BrandId;
                    currentproduct.CategoryId = newproduct.CategoryId;

                    try
                    {
                        _context.Products.Update(currentproduct);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return Problem($"không thể cập nhật dữ liệu; {ex.Message}");
                    }
                    return NoContent();
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        // DELETE api/<ProductController>/5
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductAsync(string productId)
        {
            if (!string.IsNullOrEmpty(productId))
            {
                if (_context.Products == null)
                {
                    return Problem();
                }
                var deleteproduct = await _context.Products.Where(p => p.ProductId == productId).Include(i => i.ProductImage).FirstOrDefaultAsync();
                if (deleteproduct != null)
                {

                    try
                    {
                        _context.Products.Remove(deleteproduct);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return Problem($"không thể cập nhật dữ liệu; {ex.Message}");
                    }
                    return NoContent();
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        private bool CheckProductExists(string productId){
            return _context.Products.Any(e => e.ProductId == productId);
        }
        [HttpGet("Action")]
        public IActionResult Action()
        {
            _logger.LogInformation("Thông điệp log thông tin"); // Ghi thông tin
            _logger.LogWarning("Thông điệp log cảnh báo");        // Ghi cảnh báo
            _logger.LogError("Thông điệp log lỗi");              // Ghi lỗi
                                                                 // Các cấp độ và phong cách ghi log khác...
            return Ok();
        }
    }
}
