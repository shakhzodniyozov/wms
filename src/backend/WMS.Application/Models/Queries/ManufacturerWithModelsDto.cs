namespace WMS.Application;

public class ManufacturerWithModelsDto
{
    public string Manufacturer { get; set; } = null!;
    public IEnumerable<ModelWithYears>? Models { get; set; }
}

public class ModelWithYears
{
    public string ModelName { get; set; } = null!;
    public string YearsOfIssue { get; set; } = null!;
}