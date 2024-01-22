namespace WMS.Application;

public class ManufacturersAndCategoriesDto
{
    public IEnumerable<ManufacturerDto> Manufacturers { get; set; } = null!;
    public IEnumerable<CategoryDto> Categories { get; set; } = null!;
}
