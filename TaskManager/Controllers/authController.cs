using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly UserManager<ApplicationDbContext> _userManager;
        private readonly ILogger<authController> _logger;
        public authController(UserManager<ApplicationDbContext> userManager, ILogger<authController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Resign()
        {
            return Ok();
        }
    }
}
