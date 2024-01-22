using System.Text.Json.Serialization;
using WMS.Domain;

namespace WMS.Application;

public class ModelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int YearOfIssue { get; set; }

    [JsonConverter(typeof(BodyTypeConverter))]
    public BodyTypes BodyType { get; set; }
    public Guid ManufacturerId { get; set; }
}
