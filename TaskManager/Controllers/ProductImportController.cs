using Data;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImportController : ControllerBase
    {
        private readonly ILogger<ProductImportController> _logger;
        private readonly ApplicationDbContext _context;
        public ProductImportController(ILogger<ProductImportController> logger, ApplicationDbContext context){
            _context = context;
            _logger = logger;
        }
    }
}
