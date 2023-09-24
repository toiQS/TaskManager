using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ModelRequest.SupplierModel;
using TaskManager.Models.ModelResponse;
using Entity;
using Data;
using ENTITY;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SupplierController> _logger;
        public SupplierController(ApplicationDbContext context, ILogger<SupplierController> logger){
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<SupplierIndexRequest>>> GetAllSupplierAsync(){
            if(_context.Suppliers == null){
                return Problem();
            }
            var supplier = await _context.Suppliers.ToListAsync();
            var result = supplier.Select(s => new SupplierIndexRequest{
                SupplierId = s.SupplierId,
                SupplierName = s.SupplierName
            }).ToList();
            return Ok(result);
        }
        [HttpGet("{supplierId}")]
        public async Task<ActionResult<SupplierDetailRequest>> GetSupplierByIdAsync(string supplierId){
            if(!string.IsNullOrEmpty(supplierId)){
                if(_context.Suppliers == null){
                    return Problem();
                }
                var supplier = await _context.Suppliers.Where(s => s.SupplierId == supplierId).Include(s => s.BillList).FirstOrDefaultAsync();
                if(supplier != null){
                    var result = new SupplierDetailRequest{
                        SupplierId = supplier.SupplierId,
                        SupplierAddress = supplier.SupplierAddress,
                        SupplierEmail = supplier.SupplierEmail,
                        SupplierName = supplier.SupplierName,
                        SupplierPhone = supplier.SupplierPhone,
                        TaxID = supplier.TaxID,
                        BillList = supplier.BillList
                    };
                    return Ok(result);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<ActionResult<SupplierResponse>> CreateSupplierAsync(SupplierResponse newsupplier){
            if(ModelState.IsValid){
                if(_context.Suppliers == null){
                    return Problem();
                }
                var supplier = new Supplier{
                    SupplierAddress = newsupplier.SupplierAddress,
                    SupplierEmail = newsupplier.SupplierEmail,
                    SupplierId = newsupplier.SupplierId,
                    SupplierName = newsupplier.SupplierName,
                    TaxID = newsupplier.TaxID,
                    BillList = new List<ImportBill>()
                };
                _context.Suppliers.Add(supplier);
                try{
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex){
                    return Problem(ex.Message);
                }
                return CreatedAtAction(nameof(GetSupplierByIdAsync), new {supplierId = newsupplier.SupplierId}, newsupplier);
            }
            return BadRequest();
        }
        [HttpPut("{supplierId}")]
        public async Task<ActionResult<SupplierResponse>> UpdateSupplierAsync(string supplierId, SupplierResponse newsupplier){
            if(!string.IsNullOrEmpty(supplierId)&&ModelState.IsValid){
                if(_context.Suppliers == null){
                    return Problem();
                }
                var supplier = await _context.Suppliers.Where(s => s.SupplierId == supplierId).Include(s => s.BillList).FirstOrDefaultAsync();
                if(supplier != null){
                    supplier.SupplierAddress = newsupplier.SupplierAddress;
                    supplier.SupplierEmail = newsupplier.SupplierEmail;
                    supplier.SupplierId = newsupplier.SupplierId;
                    supplier.SupplierName = newsupplier.SupplierName;
                    supplier.TaxID = newsupplier.TaxID;
                    try{
                        _context.Suppliers.Update(supplier);
                        await _context.SaveChangesAsync();
                    }
                    catch(Exception ex){
                        return Problem(ex.Message);
                    }
                    return CreatedAtAction(nameof(GetSupplierByIdAsync), new {supplierId = newsupplier.SupplierId}, newsupplier);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpDelete("{supplierId}")]
        public async Task<IActionResult> DeleteSupplierAsync(string supplierId){
            if(!string.IsNullOrEmpty(supplierId)){
                if(_context.Suppliers == null){
                    return Problem();
                }
                var deletesupplier = await _context.Suppliers.Where(s => s.SupplierId == supplierId).Include(s => s.BillList).FirstOrDefaultAsync();
                if(deletesupplier != null){
                    try{
                        _context.Suppliers.Remove(deletesupplier);
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
    }
}
