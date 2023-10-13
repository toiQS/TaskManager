using Entity;
using TaskManager.Models.ModelRequest.ProductImportModel;

namespace TaskManager.Models.ModelRequest.ImportBillModel
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
