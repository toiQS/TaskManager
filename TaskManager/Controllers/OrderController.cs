using Data;
using ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ItemOrderModel;
using TaskManager.Models.OrderModel;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderController> _logger;
        public OrderController(ApplicationDbContext context, ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<OrderIndexRequest>>> GetOrders()
        {
           if(_context.Orders == null)
            {
                return Problem("không thể truy cập dữ liệu");
            }
            var order = await _context.Orders.ToListAsync();
            var result = order.Select(r => new OrderIndexRequest
            {
                OrderId = r.OrderId,
                CreateAt = DateTime.Now,
            });
            return Ok(result);
        }
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDetailRequest>> GetOrderByOrderId(string orderId)
        {
            if(!string.IsNullOrEmpty(orderId))
            {
                if (_context.Orders == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var order = await _context.Orders.Where(x => x.OrderId == orderId).Include(x => x.ItemOrders).FirstOrDefaultAsync();
                if (order != null) {
                    var result = new OrderDetailRequest
                    {
                        OrderId = order.OrderId,
                        CreateAt = order.CreateAt,
                        itemOrders = order.ItemOrders.Select(x => new ItemOrderIndexRequest
                        {
                            ItemOrderId = x.ItemOrderId,
                            OrderId = x.OrderId,
                            ProductId = x.ProductId,
                        } ).ToList()
                    };
                    return Ok(result);
                }
                return NotFound("Không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateResponse newOrder)
        {
            if (ModelState.IsValid)
            {
                if(_context.Orders == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var order = new Order
                {
                    OrderId = newOrder.OrderId,
                    CreateAt = newOrder.CreateAt,
                    ItemOrders = new List<ItemOrder>()
                };
                try
                {

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Problem($"Error: {ex.Message}");
                } 
                return CreatedAtAction(nameof(GetOrderByOrderId), new {orderId = order.OrderId}, order);
            }
            return BadRequest("dữ liệu dầu vào không đúng");
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            if(!string.IsNullOrEmpty(orderId))
            {
                if(_context.ItemOrders == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var order = await _context.Orders.Where(x => x.OrderId  == orderId).Include(x => x.ItemOrders).FirstOrDefaultAsync();
                if(order != null)
                {
                    try
                    {
                        _context.Orders.Remove(order);
                        await _context.SaveChangesAsync();
                    }
                    catch(Exception ex)
                    {
                        return Problem(ex.Message);
                    }
                }
                return NotFound("không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
    }
}
