using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.UserModel
{
    public class UserIndexRequest
    {
        
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
