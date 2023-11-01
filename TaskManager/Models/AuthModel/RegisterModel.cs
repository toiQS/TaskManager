using Microsoft.AspNetCore.Identity;

namespace TaskManager.Models.AuthModel
{
    public class RegisterModel : IdentityUser
    {
        public string Id {  get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

    }
}
