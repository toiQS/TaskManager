namespace TaskManager.Models.OrderModel
{
    public class OrderIndexRequest
    {
        public string OrderId { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
    }
}
