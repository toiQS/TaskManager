using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTITY
{
    public class ProductWarehouse
    {
        [Key]
        public long ProductWarehouseId { get; set; }
        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; } = string.Empty;
        public Product? Product { get; set; }
        public string WarehouseId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double ImportPriceOfEachProduct { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
