using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.AuthModel;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthController> _logger;
        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserRequest UserModel)
        {
            if(UserModel.UserName != null && UserModel.PasswordHash != null)
            {
                if(_userManager.Users == null)
                {
                    return Problem("không thể truy cập dữ liệu");
                }
                var result = await _signInManager.PasswordSignInAsync(UserModel.UserName, UserModel.PasswordHash , false, lockoutOnFailure: false); 
                if(result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(UserModel.UserName);
                    var userInfo = new { UserName = user.UserName, Email = user.Email }; // Thay bằng thông tin bạn muốn trả về
                    return Ok(userInfo);
                }
                else if (result.RequiresTwoFactor)
                {
                    return BadRequest("Yêu cầu xác thực hai bước");
                }
                else if (result.IsLockedOut)
                {
                    return BadRequest("Tài khoản đã bị khóa");
                }

                return Unauthorized("Đăng nhập không thành công");
            }
            return BadRequest("dữ liệu đầu vào không đúng");
        }
        [HttpPost("register")]
        public async  Task<IActionResult> Register(IdentityUser newUser)
        {
            return Ok(newUser);
            
        }
    }
}
