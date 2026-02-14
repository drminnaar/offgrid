using System.Text.Json;
using System.Text.Json.Serialization;

namespace Offgrid.Framework.System.Text;

/// <summary>
/// Provides default JSON serialization options for the application. This class contains static
/// properties that define common settings for JSON serialization, such as property naming policies
/// and how to handle null values. These options can be used throughout the application to ensure
/// consistent JSON serialization behavior, especially in scenarios like messaging where specific
/// formatting may be required.
/// </summary>
public static class JsonSerializationOptions
{
    /// <summary>
    /// Default options for pretty-printed JSON serialization, which includes settings such as
    /// camel case naming policy, indented formatting, and ignoring null values during
    /// serialization. This can be used for scenarios where human-readable JSON output is desired,
    /// such as for logging or debugging purposes.
    /// </summary>
    /// <remarks>
    /// NOTE: This is used for pretty-printed JSON serialization across the application. It can be
    /// overridden by specific options when needed, such as for messaging or web scenarios that
    /// require different settings.
    /// <example>
    /// <code>
    /// var options = JsonSerializationOptions.Pretty;
    /// var json = JsonSerializer.Serialize(myObject, options);
    /// </code>
    /// </example>
    /// <returns>JsonSerializerOptions</returns>
    public static readonly JsonSerializerOptions Pretty = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Default options for message/event JSON serialization across the application. This includes
    /// settings such as camel case naming policy and ignoring null values during serialization.
    /// </summary>
    /// <remarks>
    /// NOTE: This is used as the default options for message/event JSON serialization across the
    /// application. It can be overridden by specific options when needed, such as for messaging or
    /// other scenarios that require different settings.
    /// </remarks>
    /// <example>
    /// <code>
    /// var options = JsonSerializationOptions.Messaging;
    /// var json = JsonSerializer.Serialize(myObject, options);
    /// </code>
    /// </example>
    public static readonly JsonSerializerOptions Messaging = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Default options for web-related JSON serialization, such as for API responses. This includes
    /// settings such as camel case naming policy and ignoring null values during serialization.
    /// </summary>
    /// <remarks>
    /// NOTE: This is used as the default options for web-related JSON serialization across the
    /// application. It can be overridden by specific options when needed, such as for messaging or
    /// other scenarios that require different settings.
    /// </remarks>
    /// <example>
    /// <code>
    /// var options = JsonSerializationOptions.Web;
    /// var json = JsonSerializer.Serialize(myObject, options);
    /// </code>
    /// </example>
    public static readonly JsonSerializerOptions Web = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}
