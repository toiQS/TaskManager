using Entity;

namespace TaskManager.Models.ModelRequest.ImportBillModel
{
    public class ImportBillDetailRequest
    {
        public string ImportBillId { get; set; } = string.Empty;
        public string WarehouseId { get; set; } = string.Empty;
        public string SupplierId { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public ICollection<ProductImport> ListProduct { get; set; } = new List<ProductImport>();
    }
}
