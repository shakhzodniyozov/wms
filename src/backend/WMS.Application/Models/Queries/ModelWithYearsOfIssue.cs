namespace WMS.Application;

public class ModelWithYearsOfIssueDto
{
    public string Model { get; set; } = null!;
    public List<int> YearsOfIssue { get; set; } = new();
}
