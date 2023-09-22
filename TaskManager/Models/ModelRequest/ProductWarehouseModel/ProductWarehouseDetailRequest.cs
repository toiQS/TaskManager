namespace TaskManager.Models.ModelRequest.ProductWarehouseModel
{
    public class ProductWarehouseDetailRequest
    {
        public long ProductWarehouseId { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string WarehouseId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double ImportPriceOfEachProduct { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
