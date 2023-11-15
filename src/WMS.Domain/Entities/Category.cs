namespace WMS.Domain;

public class Category
{
    public string? Name { get; set; }
    public Category? Parent { get; set; }
    public int? ParentId { get; set; }
    public List<Category> SubCategories { get; set; } = new();
    public List<Product> Products { get; set; } = new();
}
