using Data;
using ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ImportBillModel;
using TaskManager.Models.ProductWarehouseModel;
using TaskManager.Models.WarehouseModel;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WarehouseController> _logger;
        public WarehouseController(ApplicationDbContext context, ILogger<WarehouseController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<WarehouseIndexRequest>>> GetAllWarehouseAsync()
        {
            if (_context.Warehouse == null)
            {
                return Problem();
            }
            var warehouse = await _context.Warehouse.ToListAsync();
            var result = warehouse.Select(w => new WarehouseIndexRequest
            {
                WarehouseId = w.WarehouseId,
                WarehouseName = w.WarehouseName
            }).ToList();
            return Ok(result);
        }
        [HttpGet("warehouseId")]
        public async Task<ActionResult<WarehouseDetailRequest>> GetWarehouseByIdAsync(string warehouseId)
        {
            if (!string.IsNullOrEmpty(warehouseId))
            {
                if (_context.Warehouse == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var warehouse = await _context.Warehouse.Where(w => w.WarehouseId == warehouseId).Include(w => w.WarehouseItems).Include(w => w.ImportBillItems).FirstOrDefaultAsync();
                if (warehouse != null)
                {
                    var result = new WarehouseDetailRequest
                    {
                        WarehouseId = warehouse.WarehouseId,
                        WarehouseName = warehouse.WarehouseName,
                        WarehouseAddress = warehouse.WarehouseAddress,
                        WarehouseItems = warehouse.WarehouseItems.Select(x => new ProductWarehouseIndexRequest
                        {
                            ProductId = x.ProductId,
                            ProductWarehouseId = x.ProductWarehouseId,
                            UpdateAt = x.UpdateAt,
                        }).ToList(),
                        ImportBillItems = warehouse.ImportBillItems.Select(x => new ImportBillIndexRequest
                        {
                            CreateAt = x.CreateAt,
                            ImportBillId = x.ImportBillId,
                            
                        }).ToList()
                    };
                    return Ok(result);
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPost]
        public async Task<IActionResult> CreateWarehouseAsync(WarehouseCreateResponse newwarehouse)
        {
            if (ModelState.IsValid)
            {
                if (_context.Warehouse == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                if(CheckWarehouseExists(newwarehouse.WarehouseId)){
                    return Problem("dữ liệu đã tồn tại");
                }
                var warehouse = new Warehouse
                {
                    WarehouseId = newwarehouse.WarehouseId,
                    WarehouseName = newwarehouse.WarehouseName,
                    WarehouseAddress = newwarehouse.WarehouseAddress
                };

                try
                {
                    _context.Warehouse.Add(warehouse);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Problem($"không thể cập nhật dữ liệu; {ex.Message}");
                }
                return CreatedAtAction(nameof(GetWarehouseByIdAsync), new { warehouseId = newwarehouse.WarehouseId }, newwarehouse);
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPut("{warehouseId}")]
        public async Task<IActionResult> UpdateWarehouseAsync(string warehouseId, WarehouseUpdateResponse newwarehouse)
        {
            if (!string.IsNullOrEmpty(warehouseId) && ModelState.IsValid)
            {
                if (_context.Warehouse == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var currentwarehouse = await _context.Warehouse.Where(w => w.WarehouseId == warehouseId).Include(w => w.WarehouseItems).Include(w => w.ImportBillItems).FirstOrDefaultAsync();
                if (currentwarehouse != null)
                {
                    currentwarehouse.WarehouseName = newwarehouse.WarehouseName;
                    currentwarehouse.WarehouseAddress = newwarehouse.WarehouseAddress;

                    try
                    {
                        _context.Warehouse.Update(currentwarehouse);
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
        [HttpDelete("{warehouseId}")]
        public async Task<IActionResult> DeleteWarehouseAsync(string warehouseId)
        {
            if (!string.IsNullOrEmpty(warehouseId))
            {
                if (_context.Warehouse == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var deletewarehouse = await _context.Warehouse.Where(w => w.WarehouseId == warehouseId).Include(w => w.WarehouseItems).Include(w => w.ImportBillItems).FirstOrDefaultAsync();
                if (deletewarehouse != null)
                {

                    try
                    {
                        _context.Warehouse.Remove(deletewarehouse);
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
        private bool CheckWarehouseExists(string warehouseId){
            return _context.Warehouse.Any(w => w.WarehouseId == warehouseId);
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
