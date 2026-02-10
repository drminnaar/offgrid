using Offgrid.Customers.OutboxWorker.Domain.Entities;
using Offgrid.Customers.OutboxWorker.Domain.Services;
using Offgrid.Customers.OutboxWorker.Infrastructure.Messaging;

namespace Offgrid.Customers.OutboxWorker.Application.Services;

public interface ICustomerOutboxService
{
    Task ProcessPendingMessagesAsync(int batchSize, CancellationToken cancellationToken = default);
}

public class CustomerOutboxService : ICustomerOutboxService
{
    private readonly IOutbox _outbox;
    private readonly IEventPublisher _publisher;
    private readonly ICloudEventFactory _cloudEventFactory;
    private readonly TimeProvider _timeProvider;
    private readonly ILogger<CustomerOutboxService> _logger;

    public CustomerOutboxService(
        IOutbox outbox,
        IEventPublisher publisher,
        ICloudEventFactory cloudEventFactory,
        TimeProvider timeProvider,
        ILogger<CustomerOutboxService> logger)
    {
        ArgumentNullException.ThrowIfNull(outbox, nameof(outbox));
        ArgumentNullException.ThrowIfNull(publisher, nameof(publisher));
        ArgumentNullException.ThrowIfNull(cloudEventFactory, nameof(cloudEventFactory));
        ArgumentNullException.ThrowIfNull(timeProvider, nameof(timeProvider));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        _outbox = outbox;
        _publisher = publisher;
        _cloudEventFactory = cloudEventFactory;
        _timeProvider = timeProvider;
        _logger = logger;
    }

    public async Task ProcessPendingMessagesAsync(int batchSize, CancellationToken cancellationToken = default)
    {
        var messages = await _outbox.GetPendingMessagesAsync(batchSize, cancellationToken);

        if (messages.Count == 0)
        {
            return;
        }

        foreach (var message in messages)
        {
            await ProcessPendingMessageAsync(message, cancellationToken);
        }
        await _outbox.CommitAsync(cancellationToken);
    }

    /// <summary>
    /// Processes a single pending outbox message by attempting to publish it.
    /// If the message is successfully published, it is marked as processed.
    /// If an error occurs during publishing, the message is marked as failed with the error
    /// details and retry information. If the message exceeds the maximum retry attempts,
    /// it is marked as dead lettered, indicating that it has failed permanently and should
    /// not be retried.
    /// 
    /// Note: After a message is successfully published, it is marked as processed and the
    /// change is committed to the outbox. If an error occurs during committing the processed
    /// status, the message will not be marked as processed and will be retried in the next
    /// processing cycle. This ensures that messages are not lost due to transient errors 
    /// during commit. But it also means that in the case of a commit failure, the message
    /// will be published multiple times until the commit succeeds. Depending on the idempotency
    /// of the event handling, this may or may not be acceptable. If idempotency is required,
    /// additional logic may be needed to track published messages and prevent duplicates in the
    /// case of commit failures. In this implementation it is required to use idempotent consumers
    /// to handle potential duplicate events in the case of commit failures.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task ProcessPendingMessageAsync(CustomerOutboxMessage message, CancellationToken cancellationToken)
    {
        if (!message.IsPending(_timeProvider.GetUtcNow()))
        {
            return;
        }

        try
        {
            var cloudEvent = _cloudEventFactory.CreateCloudEvent(message.EventType, message.Payload);
            await _publisher.PublishAsync(cloudEvent, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing outbox message {MessageId}. Marking as failed.", message.Id);
            MarkAsFailed(message, ex);
            return; // Exit the method to avoid marking the message as processed
        }

        message.MarkAsProcessed(_timeProvider.GetUtcNow());
    }

    private void MarkAsFailed(CustomerOutboxMessage message, Exception ex, int maxRetries = 5)
    {
        message.MarkAsFailed(ex.Message, _timeProvider.GetUtcNow(), CalculateExponentialBackoffDelay(message.RetryCount));

        if (message.RetryCount >= maxRetries)
        {
            _logger.LogError("Outbox message {Id} failed permanently after {Retries} attempts", message.Id, maxRetries);
            message.MarkAsDeadLettered(_timeProvider.GetUtcNow());
        }
        else
        {
            _logger.LogWarning("Outbox message {Id} failed with error: {Error}. It will be retried at {NextRetryAt}. Retry count: {RetryCount}", message.Id, ex.Message, message.NextRetryAt, message.RetryCount);
        }
    }

    private static TimeSpan CalculateExponentialBackoffDelay(int retryCount)
    {
        // Exponential backoff: 30s, 1min, 2min, 10min, etc.
        return TimeSpan.FromSeconds(Math.Pow(2, retryCount) * 30);
    }
}
