using Data;
using Entity;
using ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ModelRequest.ImportBillModel;
using TaskManager.Models.ModelResponse;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportBillController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ImportBillController> _logger;
        public ImportBillController(ApplicationDbContext context, ILogger<ImportBillController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<ImportBillIndexRequest>>> GetAllImportBill()
        {
            if (_context.ImportBills == null)
            {
                return Problem("không thể truy cập dữ liệu");
            }
            var import = await _context.ImportBills.ToListAsync();
            var result = import.Select(i => new ImportBillIndexRequest
            {
                ImportBillId = i.ImportBillId,
                CreateAt = i.CreateAt
            }).ToList();
            return Ok(result);
        }
        [HttpGet("{importBillId}")]
        public async Task<ActionResult<ImportBillDetailRequest>> GetImportBillById(string importBillId)
        {
            if (!string.IsNullOrEmpty(importBillId))
            {
                if (_context.ImportBills == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var import = await _context.ImportBills.Where(i => i.ImportBillId == importBillId).Include(i => i.ListProduct).FirstOrDefaultAsync();
                if (import != null)
                {
                    var result = new ImportBill
                    {
                        ImportBillId = import.ImportBillId,
                        WarehouseId = import.WarehouseId,
                        SupplierId = import.SupplierId,
                        CreateAt = import.CreateAt,
                        ListProduct = import.ListProduct
                    };
                    return Ok(result);
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPost]
        public async Task<ActionResult<ImportBillResponse>> CreateImportBill(ImportBillResponse newimportBill)
        {
            if (ModelState.IsValid)
            {
                if (_context.ImportBills == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                if(CheckImportBillExists(newimportBill.ImportBillId)){
                    return Problem("dữ liệu đã tồn tại");
                }
                var import = new ImportBill
                {
                    ImportBillId = newimportBill.ImportBillId,
                    WarehouseId = newimportBill.WarehouseId,
                    SupplierId = newimportBill.SupplierId,
                    ListProduct = new List<ProductImport>(),
                };
                try
                {
                    _context.ImportBills.Add(import);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Problem($"không thể cập nhật dữ liệu; {ex.Message}");
                }
                return CreatedAtAction(nameof(GetImportBillById), new { importBillId = newimportBill.ImportBillId }, newimportBill);
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPut("{importBillId}")]
        public async Task<ActionResult<ImportBillResponse>> UpdateImportBill(string importBillId, ImportBillResponse newimportBill)
        {
            if (!string.IsNullOrEmpty(importBillId) && ModelState.IsValid)
            {
                if (_context.ImportBills == null)
                {
                    return BadRequest("không thể truy cập dữ liệu");
                }
                var item = await _context.ImportBills.Where(i => i.ImportBillId == importBillId).Include(i => i.ListProduct).FirstOrDefaultAsync();
                if (item != null)
                {
                    item.SupplierId = newimportBill.SupplierId;
                    item.ImportBillId = newimportBill.ImportBillId;
                    item.WarehouseId = newimportBill.WarehouseId;
                    item.CreateAt = DateTime.Now;
                    try
                    {
                        _context.ImportBills.Update(item);
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
        [HttpDelete("{importBillId}")]
        public async Task<ActionResult<ImportBillResponse>> DeleteImportBill(string importBillId)
        {
            if (!string.IsNullOrEmpty(importBillId))
            {
                if (_context.ImportBills == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var deleteitem = await _context.ImportBills.Where(i => i.ImportBillId == importBillId).Include(i => i.ListProduct).FirstOrDefaultAsync();
                if (deleteitem != null)
                {
                    try
                    {
                        _context.ImportBills.Remove(deleteitem);
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
        public bool CheckImportBillExists(string importBillId){
            return _context.ImportBills.Any(e => e.ImportBillId == importBillId);
        }
    }
}
