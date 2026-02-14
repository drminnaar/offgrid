using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Offgrid.Framework.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Offgrid.Framework.RabbitMq;

public abstract class RabbitMqConsumerClientBase<TMessage> : RabbitMqClientBase where TMessage : class
{
    private readonly IEventHandler<TMessage> _messageHandler;
    private const int MaxRetryCount = 5;
    private const double BaseDelaySeconds = 1;
    private CancellationToken _consumeCancellationToken = CancellationToken.None;

    public RabbitMqConsumerClientBase(
        ILogger<RabbitMqConsumerClientBase<TMessage>> logger,
        IConnectionFactory connectionFactory,
        IOptions<RabbitMqClientOptions> settings,
        IEventHandler<TMessage> messageHandler)
        : base(logger, connectionFactory, settings)
    {
        ArgumentNullException.ThrowIfNull(messageHandler, nameof(messageHandler));
        _messageHandler = messageHandler;
    }

    protected abstract string QueueName { get; }
    protected abstract string RoutingKey { get; }

    public async Task ConsumeAsync(CancellationToken cancellationToken)
    {
        _consumeCancellationToken = cancellationToken;
        await EnsureConnectedWithQueueAsync(new QueueSettings(QueueName, RoutingKey), cancellationToken);

        var consumer = new AsyncEventingBasicConsumer(Channel!);
        consumer.ReceivedAsync += OnMessageReceivedAsync;

        await Channel!.BasicConsumeAsync(
            QueueName,
            false,
            consumer,
            cancellationToken: cancellationToken);
    }

    protected abstract Task HandleMessageReceivedAsync(BasicDeliverEventArgs eventArgs, CancellationToken cancellationToken);

    private async Task OnMessageReceivedAsync(object sender, BasicDeliverEventArgs eventArgs)
    {
        for (var attempt = 1; attempt <= MaxRetryCount; attempt++)
        {
            try
            {
                await HandleMessageReceivedAsync(eventArgs, _consumeCancellationToken);
                await Channel!.BasicAckAsync(eventArgs.DeliveryTag, false);
                return;
            }
            catch (Exception ex) when (attempt < MaxRetryCount)
            {
                Logger.LogWarning(ex, "Failed to process message. Attempt {Attempt} of {MaxAttempts}", attempt, MaxRetryCount);
                var delay = CalculateExponentialBackoffDelay(attempt);
                await Task.Delay(delay, _consumeCancellationToken);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to process message after {MaxAttempts} attempts", MaxRetryCount);
                await Channel!.BasicNackAsync(eventArgs.DeliveryTag, false, false);
                return;
            }
        }
    }

    /// <summary>
    /// Calculates the delay for the next retry attempt using exponential backoff. The delay 
    /// increases exponentially with each attempt, starting from a base delay. This helps to
    /// reduce the load on the system and allows time for transient issues to resolve before
    /// retrying.
    /// </summary>
    /// <remarks>
    /// The delay is calculated as: BaseDelaySeconds * 2^(attempt - 1). For example, with a
    /// base delay of 1 second, the delays for attempts 1 to 5 would be: 1s, 2s, 4s, 8s, and 16s
    /// respectively. If the maximum number of retry attempts is reached without success, the
    /// message will be rejected and not requeued, preventing infinite retry loops and allowing
    /// for manual intervention if necessary.
    /// </remarks>
    /// <param name="attempt">The current retry attempt number.</param>
    /// <returns>A <see cref="TimeSpan"/> representing the delay before the next retry.</returns>
    private static TimeSpan CalculateExponentialBackoffDelay(int attempt)
    {
        return TimeSpan.FromSeconds(Math.Pow(2, attempt - 1) * BaseDelaySeconds);
    }
}
