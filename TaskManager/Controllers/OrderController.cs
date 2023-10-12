using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ModelRequest.OrderModel;
using TaskManager.Models.ModelRequest.OrderModelModel;

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
                        itemOrders = order.ItemOrders
                    };
                    return Ok(result);
                }
                return NotFound("Không tìm thấy dữ liệu");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
    }
}
