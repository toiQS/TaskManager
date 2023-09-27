using Data;
using ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ModelRequest.BrandModel;
using TaskManager.Models.ModelResponse;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BrandController> _logger;
        public BrandController(ApplicationDbContext context, ILogger<BrandController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<BrandIndexRequest>>> GetAllBrandAsync()
        {
            if (_context.Brands == null)
            {
                return Problem("không thể truy cập dữ liệu");
            }
            var brand = await _context.Brands.ToListAsync();
            var result = brand.Select(b => new BrandIndexRequest
            {
                BrandId = b.BrandId,
                BrandName = b.BrandName
            }).ToList();
            return Ok(result);
        }
        [HttpGet("{brandId}")]
        public async Task<ActionResult<BrandDetailRequest>> GetBrandByIdAsync(string brandId)
        {
            if (!string.IsNullOrEmpty(brandId))
            {
                if (_context.Brands != null)
                {
                    var brand = await _context.Brands.Where(b => b.BrandId == brandId).Include(b => b.Products).FirstOrDefaultAsync();
                    if (brand != null)
                    {
                        var result = new BrandDetailRequest
                        {
                            BrandId = brand.BrandId,
                            BrandName = brand.BrandName,
                            BrandInfo = brand.BrandInfo,
                            Products = brand.Products
                        };
                        return Ok(result);
                    }
                    return NotFound("Không tìm thấy thương hiệu");
                }
                return Problem("Không thể truy câp dữ liệu");
            }
            return BadRequest("dữ liệu nhập vào không đúng");
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrandAsync(BrandResponse newbrand)
        {
            if (ModelState.IsValid)
            {
                if (_context.Brands != null)
                {
                    if(CheckBrandExists(newbrand.BrandId))
                        return Problem("đã tồn tại mã thương hiệu");
                    var brand = new Brand
                    {
                        BrandId = newbrand.BrandId,
                        BrandName = newbrand.BrandName,
                        BrandInfo = newbrand.BrandInfo,
                        Products = new List<Product>()
                    };

                    try
                    {
                        _context.Brands.Add(brand);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                       return Problem($"không thể cập nhật dữ liệu; {ex.Message}");
                    }
                    return CreatedAtAction(nameof(GetBrandByIdAsync), new { brandId = newbrand.BrandId }, newbrand);
                }
                return Problem("không thể truy cập dữ liệu");
            }
            return BadRequest("dữ liệu nhập vào không đúng");
        }
        [HttpPut("{brandId}")]
        public async Task<IActionResult> UpdateBrandAsync(string brandId, BrandResponse newbrand)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(brandId))
            {
                if (_context.Brands == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var currentbrand = await _context.Brands.Where(b => b.BrandId == brandId).Include(b => b.Products).FirstOrDefaultAsync();
                if (currentbrand != null)
                {
                    currentbrand.BrandId = newbrand.BrandId;
                    currentbrand.BrandName = newbrand.BrandName;
                    currentbrand.BrandInfo = newbrand.BrandInfo;

                    try
                    {
                        _context.Brands.Update(currentbrand);
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
            return BadRequest("dữ liệu nhập vào không đúng");
        }
        [HttpDelete("{brandId}")]
        public async Task<IActionResult> DeleteBrandAsync(string brandId)
        {
            if (!string.IsNullOrEmpty(brandId))
            {
                if (_context.Brands == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var deletebrand = await _context.Brands.Where(b => b.BrandId == brandId).Include(b => b.Products).FirstOrDefaultAsync();
                if (deletebrand != null)
                {

                    try
                    {
                        _context.Brands.Remove(deletebrand);
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
            return BadRequest("dữ liệu nhập vào không đúng");
        }
        private bool CheckBrandExists(string brandId){
            return _context.Brands.Any(e => e.BrandId == brandId);
        }
    }
}
