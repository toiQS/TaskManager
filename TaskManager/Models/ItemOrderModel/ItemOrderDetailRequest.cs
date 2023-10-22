namespace TaskManager.Models.ItemOrderModel
{
    public class ItemOrderDetailRequest
    {
        public long ItemOrderId { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double SellPrice { get; set; }
    }
}
