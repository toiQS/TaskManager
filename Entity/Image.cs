using System.ComponentModel.DataAnnotations;

namespace ENTITY
{
    public class Image
    {
        [Key]
        public string ImageId { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
    }
}
