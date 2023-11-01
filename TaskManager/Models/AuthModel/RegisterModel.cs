using Microsoft.AspNetCore.Identity;

namespace TaskManager.Models.AuthModel
{
    public class RegisterModel 
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHard { get; set; } = string.Empty;
    }
}
