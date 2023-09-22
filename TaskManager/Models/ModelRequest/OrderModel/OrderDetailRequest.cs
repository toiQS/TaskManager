using ENTITY;

namespace TaskManager.Models.ModelRequest.OrderModelModel
{
    public class OrderDetailRequest
    {
        public string OrderId { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public ICollection<ItemOrder> itemOrders { get; set; } = new List<ItemOrder>();
    }
}
