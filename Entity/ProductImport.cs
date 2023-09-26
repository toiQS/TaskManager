using ENTITY;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class ProductImport
    {
        [Key]
        public long ProductImportId { get; set; }
        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; } = string.Empty;
        public Product? Product { get; set; }
        public string ImportBillId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double PriceOfEachProduct { get; set; }
    }
}
