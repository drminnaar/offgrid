namespace Offgrid.Framework.RabbitMq;

/// <summary>
/// Represents the settings for configuring a RabbitMQ queue, including its name, durability,
/// exclusivity, auto-delete behavior, and routing key. These settings are used when declaring
/// and binding queues to exchanges in RabbitMQ to control how messages are routed and persisted.
/// Proper configuration of these settings is crucial for ensuring that your messaging
/// infrastructure behaves as expected and can handle message delivery and durability
/// requirements effectively.
/// </summary>
/// <value></value>
public sealed record QueueSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QueueSettings"/> record with the specified
    /// queue name and default values for durable, exclusive, and auto-delete properties.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="routingKey">The routing key to use when binding the queue to an exchange.</param>
    public QueueSettings(string queueName, string routingKey = "")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(queueName, nameof(queueName));
        Name = queueName;
        RoutingKey = routingKey;
    }

    /// <summary>
    /// The name of the queue.
    /// </summary>
    /// <value>The name of the queue.</value>
    public string Name { get; init; } = default!;

    /// <summary>
    /// Whether the queue should survive broker restarts. Durable queues are persisted to disk,
    /// while non-durable queues exist only in memory and will be lost if the broker restarts.
    /// 
    /// Note: The default value is true to ensure that messages are not lost in case of broker
    /// restarts. Set this to false only if you want the queue to be temporary and can tolerate
    /// message loss in such cases, and ensure that your application can handle the potential
    /// loss of messages accordingly.
    /// </summary>
    /// <value>True if the queue is durable; otherwise, false.</value>
    public bool Durable { get; init; } = true;

    /// <summary>
    /// Whether the queue should be exclusive to the connection that declared it. Exclusive queues
    /// can only be used by the connection that declared them and are deleted when the connection
    /// closes.
    /// 
    /// Note: The default value is false to allow multiple consumers to access the queue. Set this
    /// to true only if you want the queue to be used by a single connection and automatically
    /// deleted when that connection closes, and ensure that your application can handle the
    /// potential loss of messages in such cases.
    /// </summary>
    /// <value>True if the queue is exclusive; otherwise, false.</value>
    public bool Exclusive { get; init; } = false;

    /// <summary>
    /// Whether the queue should be automatically deleted when no longer in use. Auto-delete
    /// queues are deleted when the last consumer unsubscribes.
    /// 
    /// Note: The default value is false to prevent accidental deletion of queues. Set this
    /// to true only if you want the queue to be automatically removed when it is no longer
    /// needed, and ensure that your application can handle the potential loss of messages
    /// in such cases.
    /// </summary>
    /// <value>True if the queue is auto-delete; otherwise, false.</value>
    public bool AutoDelete { get; init; } = false;

    /// <summary>
    /// The routing key to use when binding the queue to an exchange. The routing key is used by
    /// the exchange to determine how to route messages to the queue. It is important to set this
    /// correctly based on your application's messaging patterns and the type of exchange you are
    /// using (e.g., direct, topic, fanout) to ensure that messages are delivered to the intended
    /// queues.
    /// 
    /// Note: The routing key should be chosen carefully based on your application's requirements
    /// and the exchange type.
    ///   - For direct exchanges, it should match the routing key used by producers.
    ///   - For topic exchanges, it can include wildcards to allow for flexible routing.
    ///   - For fanout exchanges, the routing key is typically ignored.
    /// Make sure to configure the routing key in a way that aligns with your message routing
    /// strategy and ensures that messages are delivered to the correct queues.
    /// </summary> 
    /// <value>The routing key to use when binding the queue to an exchange.</value>
    public string RoutingKey { get; init; } = default!;
}
