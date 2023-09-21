using System.ComponentModel.DataAnnotations;

namespace ENTITY;
public class Brand
{
    [Key]
    public string BrandId { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string BrandInfo { get; set; } = string.Empty;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}