using System.Globalization;
using System.Text;
using System.Text.Json;
using CloudNative.CloudEvents;
using Offgrid.Framework.System.Text;
using RabbitMQ.Client.Events;

namespace Offgrid.Framework.RabbitMQ.Extensions;

public static partial class RabbitMqExtensions
{
    public static CloudEvent ToCloudEvent<TData>(this BasicDeliverEventArgs eventArgs) where TData : class
    {
        ArgumentNullException.ThrowIfNull(eventArgs, nameof(eventArgs));

        var properties = eventArgs.BasicProperties;

        var cloudEvent = new CloudEvent
        {
            DataContentType = properties.ContentType
        };

        if (properties.Headers != null)
        {
            foreach (var header in properties.Headers)
            {
                cloudEvent.AddCloudEventAttribute(header);
            }
        }

        cloudEvent.AddCloudEventData<TData>(eventArgs);

        return cloudEvent;
    }

    private static void AddCloudEventAttribute(this CloudEvent cloudEvent, KeyValuePair<string, object?> header)
    {
        try
        {
            if (!header.Key.StartsWith("ce-", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var key = header.Key[3..].ToLowerInvariant();

            if (key == "specversion")
            {
                // Ignore specversion as cloud events SDK will prevent assignement to it and we
                // don't need it for processing.
                return;
            }

            var value = header.Value is byte[] bytes
                ? Encoding.UTF8.GetString(bytes)
                : header.Value?.ToString() ?? "";

            if (key == "source")
            {
                cloudEvent[key] = new Uri(value, UriKind.Absolute);
                return;
            }

            if (key == "time")
            {
                if (DateTimeOffset.TryParse(value, null, DateTimeStyles.RoundtripKind, out var time))
                {
                    cloudEvent[key] = time;
                }
                return;
            }

            cloudEvent[key] = value;
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException($"Failed to add cloud event attribute from header '{header.Key}' with value '{header.Value}'", exception);
        }
    }

    private static void AddCloudEventData<TData>(this CloudEvent cloudEvent, BasicDeliverEventArgs eventArgs) where TData : class
    {
        try
        {
            if (eventArgs.Body.Length == 0 || cloudEvent.DataContentType == null)
            {
                return;
            }

            var bodyJson = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var @event = JsonSerializer.Deserialize<TData>(bodyJson, JsonSerializationOptions.Messaging);
            cloudEvent.Data = @event;
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException($"Failed to deserialize cloud event data '{typeof(TData).Name}' from message body with content type '{cloudEvent.DataContentType}'", exception);
        }
    }
}
