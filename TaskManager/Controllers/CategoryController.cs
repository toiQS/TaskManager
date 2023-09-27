using Data;
using ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ModelRequest.Categories;
using TaskManager.Models.ModelRequest.CategoriesModel;
using TaskManager.Models.ModelResponse;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ApplicationDbContext context, ILogger<CategoryController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<CategoriesIndexRequest>>> GetAllCategoryAsync()
        {
            if (_context.Categories == null)
            {
                return Problem("không thể truy cập dữ liệu");
            }
            var category = await _context.Categories.ToListAsync();
            var result = category.Select(c => new CategoriesIndexRequest
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToList();
            return Ok(result);
        }
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoriesDetailRequest>> GetCategoryByIdAsync(string categoryId)
        {
            if (!string.IsNullOrEmpty(categoryId))
            {
                if (_context.Categories == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var category = await _context.Categories.Where(c => c.CategoryId == categoryId).Include(c => c.Products).FirstOrDefaultAsync();
                if (category != null)
                {
                    var result = new CategoriesDetailRequest
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                        CategoryInfo = category.CategoryInfo,
                        Products = category.Products
                    };
                    return Ok(result);
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu nhập vào không đúng");
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(CategoriesResponse newcategory)
        {
            if (ModelState.IsValid)
            {
                if (_context.Categories == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                if(CheckCategoryExists(newcategory.CategoryId))
                    return Problem("dữ liệu đã tồn tại");
                var category = new Category
                {
                    CategoryId = newcategory.CategoryId,
                    CategoryName = newcategory.CategoryName,
                    CategoryInfo = newcategory.CategoryInfo,
                    Products = new List<Product>()
                };

                try
                {
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Problem($"không thể cập nhật dữ liệu; {ex.Message}");
                }
                return CreatedAtAction(nameof(GetCategoryByIdAsync), new { categoryId = newcategory.CategoryId }, newcategory);
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategoryAsync(string categoryId, CategoriesResponse newcategory)
        {
            if (string.IsNullOrEmpty(categoryId) && ModelState.IsValid)
            {
                if (_context.Categories == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var currentcategory = await _context.Categories.Where(c => c.CategoryId == categoryId).Include(c => c.Products).FirstOrDefaultAsync();
                if (currentcategory != null)
                {
                    currentcategory.CategoryInfo = newcategory.CategoryInfo;
                    currentcategory.CategoryName = newcategory.CategoryName;
                    currentcategory.CategoryId = newcategory.CategoryId;

                    try
                    {
                        _context.Categories.Update(currentcategory);
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
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryAsync(string categoryId)
        {
            if (!string.IsNullOrEmpty(categoryId))
            {
                if (_context.Categories == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var deletecategory = await _context.Categories.Where(c => c.CategoryId == categoryId).Include(c => c.Products).FirstOrDefaultAsync();
                if (deletecategory != null)
                {

                    try
                    {
                        _context.Categories.Remove(deletecategory);
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
        public bool CheckCategoryExists(string categoryId){
            return _context.Categories.Any(c => c.CategoryId == categoryId);
        }
    }
}
