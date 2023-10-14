namespace TaskManager.Models.ModelRequest.ImportBillModel
{
    public class ImportBillCreateResponse
    {
        public string ImportBillId { get; set; } = string.Empty;
        public string WarehouseId { get; set; } = string.Empty;
        public string SupplierId { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
    }
}
