using ENTITY;
using TaskManager.Models.ModelRequest.ImageModel;

namespace TaskManager.Models.ModelRequest.ProductModel
{
    public class ProductDetailRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductInfo { get; set; } = string.Empty;
        public ICollection<ImageIndexRequest> ProductImage { get; set; } = new List<ImageIndexRequest>();
        public string CategoryId { get; set; } = string.Empty;
        public string BrandId { get; set; } = string.Empty;
    }
}
