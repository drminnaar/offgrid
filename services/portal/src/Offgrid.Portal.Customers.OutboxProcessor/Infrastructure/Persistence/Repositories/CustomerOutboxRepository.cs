using Microsoft.EntityFrameworkCore;
using Offgrid.Portal.Customers.OutboxProcessor.Domain.Entities;
using Offgrid.Portal.Customers.OutboxProcessor.Domain.Services;

namespace Offgrid.Portal.Customers.OutboxProcessor.Infrastructure.Persistence.Repositories;

public sealed class CustomerOutboxRepository : IOutbox
{
    private readonly IOutboxDbContext _context;
    private readonly TimeProvider _timeProvider;

    public CustomerOutboxRepository(IOutboxDbContext context, TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        ArgumentNullException.ThrowIfNull(timeProvider, nameof(timeProvider));
        _context = context;
        _timeProvider = timeProvider;
    }
    public async Task<IReadOnlyCollection<CustomerOutboxMessage>> GetPendingMessagesAsync(int batchSize, CancellationToken cancellationToken = default)
    {
        var now = _timeProvider.GetUtcNow();
        return await _context
            .CustomerOutboxMessages
            .TagWith("Get pending customer outbox messages")
            .Where(message => !message.IsDeadletter
                && message.ProcessedAt == null
                && (message.NextRetryAt == null || message.NextRetryAt <= now))
            .OrderBy(message => message.OccurredAt)
            .Take(batchSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        if (!_context.HasChanges())
        {
            return 0;
        }

        var stateEntriesWritten = await _context.SaveChangesAsync(cancellationToken);

        return stateEntriesWritten > 0
            ? stateEntriesWritten
            : throw new InvalidOperationException("Failed to save changes to the database. No state entries were written.");
    }
}
