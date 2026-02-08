using System.Text.Json;
using System.Text.Json.Serialization;

namespace Offgrid.Framework.System.ComponentModel.DataAnnotations;

public class SafeJsonToEnumConverter<TEnum> : JsonConverter<TEnum?> where TEnum : struct, Enum
{
    private readonly string _errorMessage;

    public SafeJsonToEnumConverter() : this(null)
    {
    }

    public SafeJsonToEnumConverter(string? errorMessage = null)
    {
        _errorMessage = errorMessage ??
            $"Invalid value. Must be one of: {string.Join(", ", Enum.GetNames<TEnum>())}";
    }

    public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (Enum.TryParse<TEnum>(value, ignoreCase: true, out var result))
        {
            return result;
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString());
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
