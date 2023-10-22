namespace TaskManager.Models.ProductImportModel
{
    public class ProductImportIndexRequest
    {
        public long ProductImportId { get; set; }
        public string ImportBillId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
    }
}
