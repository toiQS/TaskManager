using ENTITY;
using TaskManager.Models.ImportBillModel;
using TaskManager.Models.ProductWarehouseModel;

namespace TaskManager.Models.WarehouseModel
{
    public class WarehouseDetailRequest
    {
        public string WarehouseId { get; set; } = string.Empty;
        public string WarehouseName { get; set; } = string.Empty;
        public string WarehouseAddress { get; set; } = string.Empty;
        public ICollection<ProductWarehouseIndexRequest> WarehouseItems { get; set; } = new List<ProductWarehouseIndexRequest>();
        public ICollection<ImportBillIndexRequest> ImportBillItems { get; set; } = new List<ImportBillIndexRequest>();
    }
}
