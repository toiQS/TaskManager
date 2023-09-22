namespace TaskManager.Models.ModelResponse
{
    public class SupplierResponse
    {
        public string? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierAddress { get; set; }
        public string SupplierEmail { get; set; } = string.Empty;
        public string SupplierPhone { get; set; } = string.Empty;
        public string TaxID { get; set; } = string.Empty;
    }
}
