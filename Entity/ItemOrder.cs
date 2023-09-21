using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTITY
{
    public class ItemOrder
    {
        [Key]
        public long ItemOrderId { get; set; }
        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; } = string.Empty;
        public Product? Product { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double SellPrice { get; set; }
    }
}
