using System.Text.Json;
using System.Text.Json.Serialization;
using WMS.Domain;

namespace WMS.Application;

public class BodyTypeConverter : JsonConverter<BodyTypes>
{
    public override BodyTypes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var enumAsString = reader.GetString();
        if (!string.IsNullOrEmpty(enumAsString))
            return Enum.Parse<BodyTypes>(enumAsString);
        throw new Exception("Detected undefined body type");
    }

    public override void Write(Utf8JsonWriter writer, BodyTypes value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
