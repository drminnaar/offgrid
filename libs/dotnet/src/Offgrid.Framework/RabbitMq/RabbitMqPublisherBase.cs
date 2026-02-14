using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Offgrid.Framework.System.Text;
using RabbitMQ.Client;

namespace Offgrid.Framework.RabbitMq;

public abstract class RabbitMqPublisherBase<TMessage> : RabbitMqClientBase where TMessage : class
{
    private readonly JsonSerializerOptions _jsonOptions = JsonSerializationOptions.Messaging;

    protected JsonSerializerOptions JsonOptions => _jsonOptions;

    protected RabbitMqPublisherBase(
        ILogger<RabbitMqPublisherBase<TMessage>> logger,
        IConnectionFactory connectionFactory,
        IOptions<RabbitMqClientOptions> options) : base(logger, connectionFactory, options)
    {
    }

    public async Task PublishAsync(TMessage message, CancellationToken cancelToken = default)
    {
        await EnsureConnectedAsync(cancelToken);

        // The EnsureConnectedAsync method guarantees that the Channel is not null and is open,
        // so we can safely use the null-forgiving operator here. However, we will still check
        // for null and throw an exception if it is, to satisfy the compiler and ensure safety.
        if (Channel == null)
        {
            throw new InvalidOperationException("RabbitMQ channel is not available.");
        }

        await Channel.BasicPublishAsync(
            exchange: Options.ExchangeName,
            routingKey: GetRoutingKey(message),
            mandatory: false,
            basicProperties: GetBasicProperties(message),
            body: GetMessageBody(message),
            cancellationToken: cancelToken);
    }

    protected virtual byte[] GetMessageBody(TMessage message)
    {
        var json = JsonSerializer.Serialize(message, JsonOptions);
        var body = Encoding.UTF8.GetBytes(json);
        return body;
    }

    protected abstract string GetRoutingKey(TMessage message);

    protected abstract BasicProperties GetBasicProperties(TMessage message);
}
