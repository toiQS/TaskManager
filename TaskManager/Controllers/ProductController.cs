using Data;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.ModelRequest.ProductModel;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ModelResponse;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ENTITY;
using System.Linq.Expressions;
using Microsoft.Build.Construction;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger){
            _context = context;
            _logger = logger;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<ICollection<ProductIndexModel>>> GetAllProductAsync(){
            if(_context.Products == null){
                return Problem();
            }
            var product = await _context.Products.ToListAsync();
            var result = product.Select(p => new ProductIndexModel{
               ProductId = p.ProductId,
               ProductName = p.ProductName 
            }).ToList();
            return Ok(result);
        }

        // GET api/<ProductController>/5
        [HttpGet("{productId}")]
        // POST api/<ProductController>
        public async Task<ActionResult<ProductDetailRequest>> GetProductByIdAsync(string productId){
            if(_context.Products == null){
                return Problem();
            }
            if(!string.IsNullOrEmpty(productId)){
                var product = await _context.Products.Where(p => p.ProductId == productId).Include(i => i.ProductImage).FirstOrDefaultAsync();
                if(product != null){
                    var result = new ProductDetailRequest{
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        ProductInfo = product.ProductInfo,
                        ProductImage = product.ProductImage
                    };
                    return Ok(result);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(ProductResponse newproduct){
            if(ModelState.IsValid){
                if(_context.Products != null){
                    var product = new Product{
                        ProductId = newproduct.ProductId,
                        ProductName = newproduct.ProductName,
                        ProductInfo = newproduct.ProductInfo,
                        BrandId = newproduct.BrandId,
                        CategoryId = newproduct.CategoryId,
                        ProductImage = new List<Image>(),
                    };
                    
                    try{
                        _context.Products.Add(product);
                        await _context.SaveChangesAsync();
                    }
                    catch(Exception ex){
                        return Problem(ex.Message);
                    }
                    return CreatedAtAction(nameof(GetProductByIdAsync), new {productId = newproduct.ProductId}, newproduct);
                }
                return Problem();
            }
            return BadRequest(ModelState);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductAsync(string productId, ProductResponse newproduct){
            if(!string.IsNullOrEmpty(productId)&& ModelState.IsValid){
                if(_context.Products == null){
                    return Problem();
                }
                var currentproduct = await _context.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
                if(currentproduct != null){
                    currentproduct.ProductId = newproduct.ProductId;
                    currentproduct.ProductName = newproduct.ProductName;
                    currentproduct.ProductInfo = newproduct.ProductInfo;
                    currentproduct.BrandId = newproduct.BrandId;
                    currentproduct.CategoryId = newproduct.CategoryId;
                    
                    try{
                        _context.Products.Update(currentproduct);
                        await _context.SaveChangesAsync();
                    }
                    catch(Exception ex){
                        return Problem(ex.Message);
                    }
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        // DELETE api/<ProductController>/5
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductAsync(string productId){
            if(!string.IsNullOrEmpty(productId)){
                if(_context.Products == null){
                    return Problem();
                }
                var deleteproduct = await _context.Products.Where(p => p.ProductId == productId).Include(i => i.ProductImage).FirstOrDefaultAsync();
                if(deleteproduct != null){
                    
                    try{
                        _context.Products.Remove(deleteproduct);
                        await _context.SaveChangesAsync();
                    }
                    catch(Exception ex){
                        return Problem(ex.Message);
                    }
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest();
        }
        public async Task<bool> CheckProductExistAsync(string productId){
            var result = await _context.Products.AnyAsync(p => p.ProductId == productId);
            return result;
        }
    }
}
