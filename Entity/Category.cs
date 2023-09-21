using System.ComponentModel.DataAnnotations;

namespace ENTITY;
public class Category
{
    [Key]
    public string CategoryId { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryInfo { get; set; } = string.Empty;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}