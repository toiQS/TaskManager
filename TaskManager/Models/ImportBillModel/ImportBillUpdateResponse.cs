namespace TaskManager.Models.ImportBillModel
{
    public class ImportBillUpdateResponse
    {
        public string WarehouseId { get; set; } = string.Empty;
        public string SupplierId { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
    }
}
