namespace WMS.Domain;

public class Manufacturer
{
    public string? Name { get; set; }
    public List<Model> Models { get; set; } = new();
}
