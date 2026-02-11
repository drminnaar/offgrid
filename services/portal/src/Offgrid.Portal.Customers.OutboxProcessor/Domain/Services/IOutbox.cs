using Offgrid.Portal.Customers.OutboxProcessor.Domain.Entities;

namespace Offgrid.Portal.Customers.OutboxProcessor.Domain.Services;

public interface IOutbox
{
    /// <summary>
    /// Retrieves a batch of pending customer outbox messages. A message is considered pending if
    /// it is not deadlettered, has not been processed, and its next retry time is either null or
    /// less than or equal to the specified time.
    /// The number of messages returned is limited by the specified batch size.
    /// 
    /// Note: All entities retrieved by this method have change tracking enabled.
    /// This allows modifications to the entities to be persisted when CommitAsync is called.
    /// </summary>
    /// <param name="batchSize">The maximum number of messages to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A read-only collection of pending customer outbox messages.</returns>
    Task<IReadOnlyCollection<CustomerOutboxMessage>> GetPendingMessagesAsync(int batchSize, CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits any changes made to the customer outbox messages in the backing store. This includes marking messages as processed or failed.
    /// If there are no changes to commit, it returns 0. If the commit operation is successful, it returns the number of successful commits.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The number of state entries written to the backing store.</returns>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
