namespace WMS.Domain;

public class Manufacturer : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<Model> Models { get; set; } = new();
}
