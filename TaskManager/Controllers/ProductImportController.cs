using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entity;
using TaskManager.Models.ModelRequest.ProductImportModel;
using TaskManager.Models.ModelResponse;
using Castle.Components.DictionaryAdapter.Xml;
using Newtonsoft.Json.Linq;

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
        [HttpGet("{importId}")]
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
        [HttpGet("{productimportId}")]
        public async Task<ActionResult<ProductImportDetailRequest>> GetProductImportById(long productimportId){
            if(productimportId > 0){
                if(_context.ProductImports == null){
                    return Problem("không thể truy cập dữ liệu");
                }
                var item = await _context.ProductImports.Where(i => i.ProductImportId == productimportId).FirstOrDefaultAsync();
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
        public async Task<IActionResult> CreateProductImport(ProductImportResponse newitem){
            if(ModelState.IsValid){
                if(_context.ProductImports == null){
                    return Problem("không thể truy cập dữ liệu");
                }
                if(CheckItemExits(newitem.ProductImportId)){
                    return Problem("dữ liệu đã tồn tại");
                }
                var item = new ProductImport{
                    ProductId = newitem.ProductId,
                    ProductImportId = newitem.ProductImportId,
                    ImportBillId = newitem.ImportBillId,
                    Quantity = newitem.Quantity,
                    PriceOfEachProduct = newitem.PriceOfEachProduct
                };
                try{
                    var warehouseItem = await _context.ProductWarehouse.Where(w => w.ProductId == item.ProductId).FirstOrDefaultAsync();
                    if(warehouseItem != null){
                        warehouseItem.ProductId = item.ProductId;
                        warehouseItem.ImportPriceOfEachProduct = item.PriceOfEachProduct;
                        warehouseItem.Quantity += item.Quantity;
                        try{
                            _context.ProductWarehouse.Update(warehouseItem);
                            
                        }
                        catch(Exception ex){
                            return Problem("không thể cập nhật dữ liệu vào kho hàng; " + ex.Message);
                        }
                    }
                    else{
                        return Problem("không tìm thấy kho hàng");
                    }

                    _context.ProductImports.Add(item);
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex){
                    return Problem("không thể cập nhật dữ liệu; " + ex.Message);    
                }
                return CreatedAtAction(nameof(GetProductImportById), new { productimportId = newitem.ProductImportId }, newitem);
            }
            return BadRequest("dữ liệu đầu vào không đúng");
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
