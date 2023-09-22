namespace TaskManager.Models.ModelRequest.ProductWarehouseModel
{
    public class ProductWarehouseIndexRequest
    {
        public long ProductWarehouseId { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public DateTime UpdateAt { get; set; }
    }
}
