namespace Domain;

public class Model : IEntity
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateOnly YearOfIssue { get; set; }
    public string MakeId { get; set; } = null!;
    public BodyType BodyType { get; set; }
    public List<string> Products { get; set; } = [];
}