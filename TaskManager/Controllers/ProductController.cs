using Data;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TaskManager.Models.ModelRequest;
using TaskManager.Models.ModelResponse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<ICollection<ProductIndexRequest>>> GetProducts()
        {
            if(_context.Products == null)
            {
                return Problem();
            }
            var products = await _context.Products.ToListAsync();
            if(products.Any())
            {
                var result = products.Select(p => new ProductIndexRequest
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                }).ToList();
                return Ok(result);
            }
            return NotFound();
        }

        // GET api/<ProductController>/5
        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> About(long productId)
        {
            if(_context.Products != null)
            {
                if(productId > 0)
                {
                    var product = await _context.Products
                        .Where(p => p.ProductId == productId)
                        .Include(p => p.ProductImage)
                        .FirstOrDefaultAsync();
                    if(product != null)
                    {
                        return Ok(product);
                    }
                    return BadRequest();
                }
                return BadRequest();
            }
            return Problem();
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(ProductResponse newProduct)
        {
            if (ModelState.IsValid)
            {
                if (_context.Products == null)
                    return Problem();
                var product = new Product
                { 
                    ProductName = newProduct.ProductName,
                    ProductCategory = newProduct.ProductCategory,
                    ProductDescription = newProduct.ProductDescription,
                    ProductType = newProduct.ProductType,
                    ProductImage = new List<Image>()
                };
                _context.Products.Add(product);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Problem(ex.ToString());
                }
                return CreatedAtAction(nameof(GetProducts),product);
            }
            return BadRequest(ModelState);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{productId}")]
        public async Task<IActionResult> EditProductByProductId(long productId, ProductResponse newproduct)
        {
            if(_context.Products == null)
                return Problem();
            if(ModelState.IsValid && productId > 0)
            {
                var product = await _context.Products.Where(x => x.ProductId  == productId).FirstOrDefaultAsync();
                if (product != null)
                {
                    product.ProductName = newproduct.ProductName;
                    product.ProductDescription = newproduct.ProductDescription;
                    product.ProductType = newproduct.ProductType;
                    _context.Products.Update(product);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch(Exception ex)
                    {
                        return Problem(ex.ToString());
                    }
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductByProductId(long productId)
        {
            if(_context.Products == null)
                return Problem();
            if(productId > 0)
            {
                var product = await _context.Products
                    .Where(x => x.ProductId == productId)
                    .Include(p => p.ProductImage)
                    .FirstOrDefaultAsync();
                if (product != null)
                {
                    _context.Products.Remove(product);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch(Exception ex)
                    {
                        return Problem(ex.ToString());
                    }
                    return NoContent() ;
                }
                return NotFound();
            }
            return BadRequest(string.Empty);
        }
    }
}
