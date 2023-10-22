using ENTITY;
using TaskManager.Models.ItemOrderModel;

namespace TaskManager.Models.OrderModel
{
    public class OrderDetailRequest
    {
        public string OrderId { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public ICollection<ItemOrderIndexRequest> itemOrders { get; set; } = new List<ItemOrderIndexRequest>();
    }
}
