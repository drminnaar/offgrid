using System.Text.Json;
using CloudNative.CloudEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Offgrid.Framework.RabbitMq;

public sealed class RabbitMqCloudEventPublisher : RabbitMqPublisherBase<CloudEvent>
{

    public RabbitMqCloudEventPublisher(
        ILogger<RabbitMqCloudEventPublisher> logger,
        IConnectionFactory connectionFactory,
        IOptions<RabbitMqClientOptions> options) : base(logger, connectionFactory, options)
    {
    }

    protected override BasicProperties GetBasicProperties(CloudEvent message) =>
        new()
        {
            ContentType = "application/json",
            Persistent = true,
            Type = typeof(CloudEvent).Name,
            Headers = new Dictionary<string, object?>
            {
                { "ce-id", message.Id! },
                { "ce-specversion", message.SpecVersion.VersionId },
                { "ce-type", message.Type! },
                { "ce-source", message.Source!.ToString()! },
                { "ce-subject", message.Subject! },
                { "ce-time", message.Time?.ToString("o")! },
                { "ce-datacontenttype", message.DataContentType! },
                { "correlationid", message["correlationid"]?.ToString() ?? string.Empty }
            }
        };

    override protected byte[] GetMessageBody(CloudEvent message) =>
        message.Data is byte[] dataBytes
            ? dataBytes
            : JsonSerializer.SerializeToUtf8Bytes(message.Data, JsonOptions);

    protected override string GetRoutingKey(CloudEvent message) => message.Type!;
}
