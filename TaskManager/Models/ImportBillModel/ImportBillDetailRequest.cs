using Entity;
using TaskManager.Models.ProductImportModel;

namespace TaskManager.Models.ImportBillModel
{
    public class ImportBillDetailRequest
    {
        public string ImportBillId { get; set; } = string.Empty;
        public string WarehouseId { get; set; } = string.Empty;
        public string SupplierId { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public ICollection<ProductImportIndexRequest> ListProduct { get; set; } = new List<ProductImportIndexRequest>();
    }
}
