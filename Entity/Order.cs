using System.ComponentModel.DataAnnotations;

namespace ENTITY
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; } = string.Empty;
        public ICollection<ItemOrder> ItemOrders { get; set; } = new List<ItemOrder>();
    }
}
