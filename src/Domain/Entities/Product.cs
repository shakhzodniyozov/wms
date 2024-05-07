namespace Domain;

public class Product : IEntity
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Code { get; set; }
    public int Quantity { get; set; }
    public string EAN { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<string> Categories { get; set; } = [];
    public List<string> Engines { get; set; } = [];
    public List<Price> Prices { get; set; } = [];
}
