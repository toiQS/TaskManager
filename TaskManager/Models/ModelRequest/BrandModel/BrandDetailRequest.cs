using ENTITY;
using TaskManager.Models.ModelRequest.ProductModel;

namespace TaskManager.Models.ModelRequest.BrandModel
{
    public class BrandDetailRequest
    {
        public string BrandId { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string BrandInfo { get; set; } = string.Empty;
        public ICollection<ProductIndexRequest> Products { get; set; } = new List<ProductIndexRequest>();
    }
}
