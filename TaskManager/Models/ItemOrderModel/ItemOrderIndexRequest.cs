namespace TaskManager.Models.ItemOrderModel
{
    public class ItemOrderIndexRequest
    {
        public long ItemOrderId { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
    }
}
