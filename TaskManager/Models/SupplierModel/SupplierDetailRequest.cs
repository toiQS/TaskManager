using ENTITY;
using TaskManager.Models.ImportBillModel;

namespace TaskManager.Models.SupplierModel
{
    public class SupplierDetailRequest
    {
        public string? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierAddress { get; set; }
        public string SupplierEmail { get; set; } = string.Empty;
        public string SupplierPhone { get; set; } = string.Empty;
        public string TaxID { get; set; } = string.Empty;
        public ICollection<ImportBillIndexRequest> BillList { get; set; } = new List<ImportBillIndexRequest>();
    }
}
