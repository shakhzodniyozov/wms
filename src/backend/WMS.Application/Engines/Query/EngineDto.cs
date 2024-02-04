namespace WMS.Application;

public class EngineDto
{
    public Guid Id { get; set; }
    public double Capacity { get; set; }
    public string FuelType { get; set; } = null!;
}
