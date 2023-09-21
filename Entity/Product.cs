using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTITY;
public class Product
{
    [Key]
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string ProductInfo { get; set; } = string.Empty;
    public ICollection<Image> ProductImage { get; set; } = new List<Image>();
    public string CategoryId { get; set; } = string.Empty;
    public string BrandId { get; set; } = string.Empty;
}