using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.ModelRequest.ImportBillModel;
using TaskManager.Models.ModelResponse;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ENTITY;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportBillController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ImportBillController> _logger;
        public ImportBillController(ApplicationDbContext context, ILogger<ImportBillController> logger){
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<ImportBillIndexRequest>>> GetAllImportBill(){
            if(_context.ImportBills == null){
                return Problem();
            }
            var import = await _context.ImportBills.ToListAsync();
            var result = import.Select(i => new ImportBillIndexRequest{
                ImportBillId = i.ImportBillId,
                CreateAt = i.CreateAt
            }).ToList();
            return Ok(result);
        }
        [HttpGet("{importBillId}")]
        public async Task<ActionResult<ImportBillDetailRequest>> GetImportBillById(string importBillId){
            if(!string.IsNullOrEmpty(importBillId)){
                if(_context.ImportBills == null){
                    return Problem();
                }
                var import = await _context.ImportBills.Where(i => i.ImportBillId == importBillId).Include(i => i.ListProduct).FirstOrDefaultAsync();
                if(import != null){
                    var result = new ImportBill{
                        ImportBillId = import.ImportBillId,
                        WarehouseId = import.WarehouseId,
                        SupplierId = import.SupplierId,
                        CreateAt = import.CreateAt,
                        ListProduct = import.ListProduct
                    };
                    return Ok(result);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<ActionResult<ImportBillResponse>> CreateImportBill(ImportBillResponse newimportBill){
            if(ModelState.IsValid){
                if(_context.ImportBills == null){
                    return Problem();
                }
                var import = new ImportBill{
                    ImportBillId = newimportBill.ImportBillId,
                    WarehouseId = newimportBill.WarehouseId,
                    SupplierId = newimportBill.SupplierId,
                    ListProduct = new List<ProductImport>(), 
                };
                try{
                    _context.ImportBills.Add(import);
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex){
                    return Problem(ex.Message);
                }
                return CreatedAtAction(nameof(GetImportBillById), new {importBillId = newimportBill.ImportBillId}, newimportBill);
            }
            return BadRequest();
        }
    }
}
