using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.ModelRequest.ProductWarehouseModel;
using TaskManager.Models.ModelResponse;
namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductWarehouseController> _logger;
        public ProductWarehouseController(ApplicationDbContext context, ILogger<ProductWarehouseController> logger){
            _context = context;
            _logger =logger ;
        }
    }
}
