using Data;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ProductImportModel;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImportController : ControllerBase
    {
        private readonly ILogger<ProductImportController> _logger;
        private readonly ApplicationDbContext _context;
        public ProductImportController(ILogger<ProductImportController> logger, ApplicationDbContext context){
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<ProductImportIndexRequest>>> GetProductImportByImportId(string importId){
            if(_context.ProductImports == null){
                return Problem("không thể truy cập dữ liệu");
            }
            var item = await _context.ProductImports.Where(i => i.ImportBillId == importId).ToListAsync();
            var result = item.Select(i => new ProductImportIndexRequest{
                ProductId = i.ProductId,
                ProductImportId = i.ProductImportId,
                ImportBillId = i.ImportBillId
            });
            return Ok(result);
        }
        [HttpGet("{productImportId}")]
        public async Task<ActionResult<ProductImportDetailRequest>> GetProductImportById(long productImportId){
            if(productImportId > 0){
                if(_context.ProductImports == null){
                    return Problem("không thể truy cập dữ liệu");
                }
                var item = await _context.ProductImports.Where(i => i.ProductImportId == productImportId).FirstOrDefaultAsync();
                if(item != null){
                    var result = new ProductImportDetailRequest{
                        ProductId = item.ProductId,
                        ProductImportId = item.ProductImportId,
                        PriceOfEachProduct = item.PriceOfEachProduct,
                        Quantity = item.Quantity,
                        ImportBillId = item.ImportBillId
                    };
                    return Ok(result);
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductImport(ProductImportResponse newItem)
        {
            if (ModelState.IsValid)
            {
                if (_context.ProductImports == null || _context.ProductWarehouse == null)
                {
                    return Problem("Không thể truy cập vào cơ sở dữ liệu");
                }
                var item = new ProductImport
                {
                    ImportBillId = newItem.ImportBillId,
                    PriceOfEachProduct = newItem.PriceOfEachProduct,
                    ProductId = newItem.ProductId,
                    Quantity = newItem.Quantity,
                };
                try
                {
                    var productInWarehouse = await _context.ProductWarehouse
                        .Where(x => x.ProductId == newItem.ProductId)
                        .FirstOrDefaultAsync();
                    if (productInWarehouse != null)
                    {
                        productInWarehouse.ImportPriceOfEachProduct = newItem.PriceOfEachProduct;
                        productInWarehouse.Quantity += newItem.Quantity;

                        _context.ProductWarehouse.Update(productInWarehouse);
                    }
                    _context.ProductImports.Add(item);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Problem($"Lỗi: {ex.Message}");
                }
                return Ok("tạo thành công");
            }
            return BadRequest("Dữ liệu đầu vào không đúng");
        }

        [HttpPut("{productimportId}")]
        public async Task<IActionResult> UpdateProductImport(long productimportId, ProductImportResponse newitem){
            if(ModelState.IsValid && productimportId > 0){
                if(_context.ProductImports == null){
                    return Problem("không thể truy cập dữ liệu");
                }
                var item = await _context.ProductImports.Where(i => i.ProductImportId == productimportId).FirstOrDefaultAsync();
                if(item != null){
                    item.ProductId = newitem.ProductId;
                    item.ImportBillId = newitem.ImportBillId;
                    item.Quantity = newitem.Quantity;
                    item.PriceOfEachProduct = newitem.PriceOfEachProduct;
                    try{
                        _context.ProductImports.Update(item);
                        await _context.SaveChangesAsync();
                    }
                    catch(Exception ex){
                        return Problem("không thể cập nhật dữ liệu; " + ex.Message);    
                    }
                    return NoContent();
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpDelete("{productimportId}")]
        public async Task<IActionResult> DeleteProductImport(long productimportId){
            if(productimportId > 0){
                if(_context.ProductImports == null){
                    return Problem("không thể truy cập dữ liệu");
                }
                var item = await _context.ProductImports.Where(i => i.ProductImportId == productimportId).FirstOrDefaultAsync();
                if(item != null){
                    try{
                        _context.ProductImports.Remove(item);
                        await _context.SaveChangesAsync();
                    }
                    catch(Exception ex){
                        return Problem("không thể cập nhật dữ liệu; " + ex.Message);
                    }
                    return NoContent();
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        private bool CheckItemExits(long productimportId){
            return _context.ProductImports.Any(i => i.ProductImportId == productimportId);
        }
    }
}
