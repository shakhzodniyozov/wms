namespace WMS.Domain;

public class Category : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Category? Parent { get; set; }
    public Guid? ParentId { get; set; }
    public List<Category> SubCategories { get; set; } = new();
    public List<Product> Products { get; set; } = new();
}
