using Data;
using ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ModelRequest.ProductWarehouseModel;
namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductWarehouseController> _logger;
        public ProductWarehouseController(ApplicationDbContext context, ILogger<ProductWarehouseController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet("{warehouseId}")]
        public async Task<ActionResult<ICollection<ProductWarehouseIndexRequest>>> GetProductWarehouseByWarehouseId(string warehouseId)
        {
            if (_context.ProductWarehouse == null)
            {
                return Problem("không thể truy cập dữ liệu");
            }
            var item = await _context.ProductWarehouse.ToListAsync();
            var result = item.Select(i => new ProductWarehouseIndexRequest
            {
                ProductId = i.ProductId,
                ProductWarehouseId = i.ProductWarehouseId,
                UpdateAt = i.UpdateAt
            }).ToList();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddProductWarehouse(ProductWarehouseDetailRequest productWarehouse)
        {
            if (ModelState.IsValid)
            {
                if (_context.ProductWarehouse == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                if(CheckItemExits(productWarehouse.ProductWarehouseId)){
                    return Problem("dữ liệu đã tồn tại");
                }
                var item = new ProductWarehouse
                {
                    ProductId = productWarehouse.ProductId,
                    WarehouseId = productWarehouse.WarehouseId,
                    Quantity = productWarehouse.Quantity,
                    ImportPriceOfEachProduct = productWarehouse.ImportPriceOfEachProduct,
                    UpdateAt = DateTime.Now
                };
                try
                {
                    _context.ProductWarehouse.Add(item);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Problem($"không thể cập nhật dữ liệu; {ex.Message}");
                }
                return CreatedAtAction(nameof(GetProductWarehouseByWarehouseId), new { warehouseId = productWarehouse.WarehouseId }, productWarehouse);
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpGet("{productwarehouseId}")]
        public async Task<ActionResult<ProductWarehouseDetailRequest>> GetProductWarehouseById(long productwarehouseId)
        {
            if (productwarehouseId > 0)
            {
                if (_context.ProductWarehouse == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var item = await _context.ProductWarehouse.Where(i => i.ProductWarehouseId == productwarehouseId).FirstOrDefaultAsync();
                if (item != null)
                {
                    var result = new ProductWarehouseDetailRequest
                    {
                        ProductWarehouseId = item.ProductWarehouseId,
                        WarehouseId = item.WarehouseId,
                        ProductId = item.ProductId,
                        ImportPriceOfEachProduct = item.ImportPriceOfEachProduct,
                        UpdateAt = item.UpdateAt
                    };
                    return Ok(result);
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPut("{productwarehouseId}")]
        public async Task<IActionResult> UpdateProductWarehouse(long productwarehouseId, ProductWarehouseDetailRequest productWarehouse)
        {
            if (productwarehouseId > 0 && ModelState.IsValid)
            {
                if (_context.ProductWarehouse == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var item = await _context.ProductWarehouse.Where(i => i.ProductWarehouseId == productwarehouseId).FirstOrDefaultAsync();
                if (item != null)
                {
                    item.ProductId = productWarehouse.ProductId;
                    item.WarehouseId = productWarehouse.WarehouseId;
                    item.Quantity = productWarehouse.Quantity;
                    item.ImportPriceOfEachProduct = productWarehouse.ImportPriceOfEachProduct;
                    item.UpdateAt = DateTime.Now;
                    try
                    {
                        _context.ProductWarehouse.Update(item);
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
        [HttpDelete("{productwarehouseId}")]
        public async Task<IActionResult> DeleteProductWarehouse(long productwarehouseId)
        {
            if (productwarehouseId > 0)
            {
                if (_context.ProductWarehouse == null)
                {
                    return BadRequest("không thể truy cập dữ liệu");
                }
                var deleteproductwarehouse = await _context.ProductWarehouse.Where(i => i.ProductWarehouseId == productwarehouseId).FirstOrDefaultAsync();
                if (deleteproductwarehouse != null)
                {
                    try
                    {
                        _context.ProductWarehouse.Remove(deleteproductwarehouse);
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
        private bool CheckItemExits(long productwarehouseId){
            return _context.ProductWarehouse.Any(e => e.ProductWarehouseId == productwarehouseId);
        }
    }
}
