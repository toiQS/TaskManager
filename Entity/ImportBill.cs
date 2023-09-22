using Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTITY
{
    public class ImportBill
    {
        [Key]
        public string ImportBillId { get; set; } = string.Empty;
        public string WarehouseId { get; set; } = string.Empty;
        public string SupplierId { get; set; } = string.Empty;
        public DateTime CreateAt {  get; set; }
        public ICollection<ProductImport> ListProduct {  get; set; } = new List<ProductImport>();
    }
}
