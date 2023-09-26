using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ModelRequest.WarehouseModel;
using TaskManager.Models.ModelResponse;
using Entity;
using Data;
using System.Diagnostics.Contracts;
using ENTITY;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WarehouseController> _logger;
        public WarehouseController(ApplicationDbContext context, ILogger<WarehouseController> logger){
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<WarehouseIndexRequest>>> GetAllWarehouseAsync(){
            if(_context.Warehouse == null){
                return Problem();
            }
            var warehouse = await _context.Warehouse.ToListAsync();
            var result = warehouse.Select(w => new WarehouseIndexRequest{
                WarehouseId = w.WarehouseId,
                WarehouseName = w.WarehouseName
            }).ToList();
            return Ok(result);
        }
        [HttpGet("warehouseId")]
        public async Task<ActionResult<WarehouseDetailRequest>> GetWarehouseByIdAsync(string warehouseId){
            if(!string.IsNullOrEmpty(warehouseId)){
                if(_context.Warehouse == null){
                    return Problem();
                }
                var warehouse = await _context.Warehouse.Where(w => w.WarehouseId == warehouseId).Include(w => w.WarehouseItems).Include(w => w.ImportBillItems).FirstOrDefaultAsync();
                if(warehouse != null){
                    var result = new WarehouseDetailRequest{
                        WarehouseId = warehouse.WarehouseId,
                        WarehouseName = warehouse.WarehouseName,
                        WarehouseAddress = warehouse.WarehouseAddress,
                        WarehouseItems = warehouse.WarehouseItems,
                        ImportBillItems = warehouse.ImportBillItems
                    };
                    return Ok(result);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> CreateWarehouseAsync(WarehouseResponse newwarehouse){
            if(ModelState.IsValid){
                if(_context.Warehouse == null){
                    return Problem();
                }
                var warehouse = new Warehouse{
                    WarehouseId = newwarehouse.WarehouseId,
                    WarehouseName = newwarehouse.WarehouseName,
                    WarehouseAddress = newwarehouse.WarehouseAddress
                };
                
                try{
                    _context.Warehouse.Add(warehouse);
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex){
                    return Problem(ex.Message);
                }
                return CreatedAtAction(nameof(GetWarehouseByIdAsync), new {warehouseId = newwarehouse.WarehouseId}, newwarehouse);
            }
            return BadRequest();
        }
        [HttpPut("{warehouseId}")]
        public async Task<IActionResult> UpdateWarehouseAsync(string warehouseId, WarehouseResponse newwarehouse){
            if(!string.IsNullOrEmpty(warehouseId)&& ModelState.IsValid){
                if(_context.Warehouse == null){
                    return Problem();
                }
                var currentwarehouse = await _context.Warehouse.Where(w => w.WarehouseId == warehouseId).Include(w => w.WarehouseItems).Include(w => w.ImportBillItems).FirstOrDefaultAsync();
                if(currentwarehouse != null){
                    currentwarehouse.WarehouseName = newwarehouse.WarehouseName;
                    currentwarehouse.WarehouseAddress = newwarehouse.WarehouseAddress;
                    currentwarehouse.WarehouseId = newwarehouse.WarehouseId;
                    
                    try{
                        _context.Warehouse.Update(currentwarehouse);
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
        [HttpDelete("{warehouseId}")]
        public async Task<IActionResult> DeleteWarehouseAsync(string warehouseId){
            if(!string.IsNullOrEmpty(warehouseId)){
                if(_context.Warehouse == null){
                    return Problem();
                }
                var deletewarehouse = await _context.Warehouse.Where(w => w.WarehouseId == warehouseId).Include(w => w.WarehouseItems).Include(w => w.ImportBillItems).FirstOrDefaultAsync();
                if(deletewarehouse != null){
                   
                    try{
                        _context.Warehouse.Remove(deletewarehouse);
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
