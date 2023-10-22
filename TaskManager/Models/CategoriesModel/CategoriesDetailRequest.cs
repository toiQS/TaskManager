using ENTITY;
using TaskManager.Models.ProductModel;

namespace TaskManager.Models.CategoriesModel
{
    public class CategoriesDetailRequest
    {
        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryInfo { get; set; } = string.Empty;
        public ICollection<ProductIndexRequest> Products { get; set; } = new List<ProductIndexRequest>();
    }
}
