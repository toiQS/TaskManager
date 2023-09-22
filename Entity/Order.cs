using System.ComponentModel.DataAnnotations;

namespace ENTITY
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public ICollection<ItemOrder> ItemOrders { get; set; } = new List<ItemOrder>();
    }
}
