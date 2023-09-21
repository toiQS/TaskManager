using System.ComponentModel.DataAnnotations;

namespace ENTITY;

public class Supplier
{
    [Key]
    public string? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? SupplierAddress { get; set; }
    [EmailAddress]
    public string SupplierEmail { get; set; } = string.Empty;
    [Phone]
    public string SupplierPhone { get; set; } = string.Empty;
    public string TaxID { get; set; } = string.Empty;
    public ICollection<ImportBill> BillList {get; set;} = new List<ImportBill>();
}