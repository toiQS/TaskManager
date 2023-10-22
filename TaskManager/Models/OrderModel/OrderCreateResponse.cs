namespace TaskManager.Models.OrderModel
{
    public class OrderCreateResponse
    {
        public string OrderId { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
    }
}
