namespace TaskManager.Models.ProductWarehouseModel
{
    public class ProductWarehouseIndexRequest
    {
        public long ProductWarehouseId { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public DateTime UpdateAt { get; set; }
    }
}
