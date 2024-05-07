namespace Domain;

public class Engine : IEntity
{
    public string Id { get; set; } = null!;
    public double Capacity { get; set; }
    public FuelType FuelType { get; set; }
}
