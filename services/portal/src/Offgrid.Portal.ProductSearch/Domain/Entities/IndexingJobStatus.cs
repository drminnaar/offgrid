namespace Offgrid.Portal.ProductSearch.Domain.Entities;

public enum IndexingJobStatus
{
    Pending = 0,
    InProgress = 1,
    Completed = 2,
    FailedAndRetrying = 3,
    Deadlettered = 4
}
