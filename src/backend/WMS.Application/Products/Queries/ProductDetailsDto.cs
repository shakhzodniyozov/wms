namespace WMS.Application;

public class ProductDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Code { get; set; }
    public decimal Price { get; set; }
    public string EAN { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public Guid? ManufacturerId { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public bool ForAllManufacturers { get; set; }
    public ModelWithYearsOfIssueDto[] Models { get; set; } = null!;
    public EngineDto[]? Engines { get; set; }
}
