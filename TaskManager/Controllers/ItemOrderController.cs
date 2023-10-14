using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ModelRequest.ItemOrderModel;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("{itemOrderId")]
        public async Task<ActionResult<ItemOrderDetailRequest>> GetItemOrder(long itemOrderId)
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
        
    }
}
