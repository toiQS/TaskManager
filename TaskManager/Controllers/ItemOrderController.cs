using Data;
using ENTITY;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ItemOrderModel;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ItemOrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ItemOrderController> _logger;
        public ItemOrderController(ApplicationDbContext context, ILogger<ItemOrderController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ICollection<ItemOrderIndexRequest>>> GetItems(string orderId)
        {
            if(_context.ItemOrders == null)
            {
                return Problem("không thể truy cập vào dữ liệu");
            }
            var item = await _context.ItemOrders.ToListAsync();
            var result = item.Select(i => new ItemOrderIndexRequest
            {
                ItemOrderId = i.ItemOrderId,
                OrderId = i.OrderId,
                ProductId = i.ProductId,
            }).ToList();
            return Ok(result);
        }
        [HttpGet("{itemOrderId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ItemOrderDetailRequest>> GetItemOrderByItemOrderId(long itemOrderId)
        {
            if(itemOrderId > 0)
            {
                if(_context.ItemOrders == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var item = await _context.ItemOrders.Where(x => x.ItemOrderId == itemOrderId).FirstOrDefaultAsync();
                if(item != null)
                {
                    var result = new ItemOrderDetailRequest
                    {
                        ItemOrderId = item.ItemOrderId,
                        OrderId = item.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        SellPrice = item.SellPrice,

                    };
                    return Ok(result);
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPost]
        public async Task<IActionResult> AddItemToOrder(ItemOrderResponse newItem)
        {
            if (ModelState.IsValid)
            {
                if (_context.ItemOrders == null || _context.ProductWarehouse == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var GetPrice = await _context.ProductWarehouse.Where(x => x.ProductId == newItem.ProductId).FirstOrDefaultAsync();
                if(GetPrice == null)
                {
                    return NotFound("không tìm thấy dữ liệu");
                }
                var item = new ItemOrder
                {
                    OrderId = newItem.OrderId,
                    ProductId = newItem.ProductId,
                    Quantity = newItem.Quantity,
                    SellPrice = GetPrice.ImportPriceOfEachProduct
                };
                try
                {
                    var ItemInWarehouse = await _context.ProductWarehouse.Where(x => x.ProductId == newItem.ProductId).FirstOrDefaultAsync();
                    if(ItemInWarehouse != null)
                    {
                        if (ItemInWarehouse.Quantity > newItem.Quantity)
                        {
                            ItemInWarehouse.Quantity -= newItem.Quantity;
                            try
                            {
                                _context.ProductWarehouse.Update(ItemInWarehouse);
                                await _context.SaveChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                return Problem(ex.Message);
                            }
                        }
                        else return Problem("số lượng không đủ");
                    }
                    _context.ItemOrders.Add(item);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
                return CreatedAtAction(nameof(GetItemOrderByItemOrderId), new { itemOrderId = item.OrderId }, item);
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPut("{itemOrderId}")]
        public async Task<IActionResult> ItemUpdate(long itemOrderId, ItemOrderResponse newitem)
        {
            if (ModelState.IsValid)
            {
                if (_context.ItemOrders == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var item = await _context.ItemOrders.Where(x => x.ItemOrderId == itemOrderId).FirstOrDefaultAsync();
                if (item != null)
                {
                    item.OrderId = newitem.OrderId;
                    item.ProductId = newitem.ProductId;
                    item.Quantity = newitem.Quantity;
                    try
                    {
                        _context.ItemOrders.Update(item);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return Problem(ex.Message);
                    }
                    return NoContent();
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpDelete("{itemOrderId}")]
        public async Task<IActionResult> ItemDelete(long itemOrderId)
        {
            if (itemOrderId > 0)
            {
                if (_context.ItemOrders == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var item = await _context.ItemOrders.Where(x => x.ItemOrderId == itemOrderId).FirstOrDefaultAsync();
                if (item != null)
                {
                    try
                    {
                        _context.ItemOrders.Remove(item);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return Problem(ex.Message);
                    }
                    return NoContent();
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
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
