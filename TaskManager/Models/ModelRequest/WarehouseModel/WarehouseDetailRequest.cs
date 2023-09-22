using ENTITY;

namespace TaskManager.Models.ModelRequest.WarehouseModel
{
    public class WarehouseDetailRequest
    {
        public string WarehouseId { get; set; } = string.Empty;
        public string WarehouseName { get; set; } = string.Empty;
        public string WarehouseAddress { get; set; } = string.Empty;
        public ICollection<ProductWarehouse> WarehouseItems { get; set; } = new List<ProductWarehouse>();
        public ICollection<ImportBill> ImportBillItems { get; set; } = new List<ImportBill>();
    }
}
