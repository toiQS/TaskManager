namespace TaskManager.Models.SupplierModel
{
    public class SupplierUpdateResponse
    {
        public string? SupplierName { get; set; }
        public string? SupplierAddress { get; set; }
        public string SupplierEmail { get; set; } = string.Empty;
        public string SupplierPhone { get; set; } = string.Empty;
        public string TaxID { get; set; } = string.Empty;
    }
}
