using Data;
using Microsoft.AspNetCore.Http;
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
        public CategoryController(ApplicationDbContext context, ILogger<CategoryController> logger){
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<CategoriesIndexRequest>>> GetAllCategoryAsync(){
            if(_context.Categories == null){
                return Problem();
            }
            var category = await _context.Categories.ToListAsync();
            var result = category.Select(c => new CategoriesIndexRequest{
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToList();
            return Ok(result);
        }
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoriesDetailRequest>> GetCategoryByIdAsync(string categoryId){
            if(!string.IsNullOrEmpty(categoryId)){
                if(_context.Categories == null){
                    return Problem();
                }
                var category = await _context.Categories.Where(c => c.CategoryId == categoryId).Include(c => c.Products).FirstOrDefaultAsync();
                if(category != null){
                    var result = new CategoriesDetailRequest{
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                        CategoryInfo = category.CategoryInfo,
                        Products = category.Products
                    };
                    return Ok(result);
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
