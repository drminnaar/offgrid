using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Offgrid.Framework.Messaging;
using Offgrid.Framework.RabbitMQ.Extensions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Offgrid.Framework.RabbitMq;

public abstract class RabbitMqCloudEventConsumer<TData> : RabbitMqConsumerClientBase<TData> where TData : class
{
    private readonly IEventHandler<TData> _eventHandler;

    public RabbitMqCloudEventConsumer(
        ILogger<RabbitMqCloudEventConsumer<TData>> logger,
        IConnectionFactory connectionFactory,
        IOptions<RabbitMqClientOptions> settings,
        IEventHandler<TData> eventHandler)
        : base(logger, connectionFactory, settings, eventHandler)
    {
        ArgumentNullException.ThrowIfNull(eventHandler, nameof(eventHandler));
        _eventHandler = eventHandler;
    }

    protected override Task HandleMessageReceivedAsync(BasicDeliverEventArgs eventArgs, CancellationToken cancellationToken)
    {
        var cloudEvent = eventArgs.ToCloudEvent<TData>();

        var data = cloudEvent.Data as TData
            ?? throw new InvalidOperationException($"Cloud event data is not of the expected type '{typeof(TData).Name}'.");

        return _eventHandler.HandleAsync(data, cancellationToken);
    }
}
